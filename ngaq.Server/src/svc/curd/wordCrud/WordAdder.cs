using ngaq.Core.model;
using ngaq.Core.svc.crud;
using ngaq.Server.db.crud;
using ngaq.model.consts;
using ngaq.Core.model.wordIF;
using tools.IF;
using ngaq.Server.Db.Crud.IF;
using Microsoft.EntityFrameworkCore.Storage;
using ngaq.Core.model.consts;
using model.consts;
using ngaq.Core.svc;

namespace ngaq.Server.svc.crud.wordCrud;


public class WordAdder:
	I_TxAdderAsync<IList<I_FullWordKV>, zero>
	, IDisposable
	,I_SetTx_DbCtx
{

	public WordAdder() {

	}

	~WordAdder(){
		Dispose();
	}
	public void Dispose() {
		_kvAdder?.Dispose();
	}



	protected KVAdder _kvAdder = new(Opt.inst().tblName_WordKV);
	protected IDbContextTransaction? _tx;

	protected DiffWord _wordDiff = new ();


	public Task<zero> Begin() {
		return _kvAdder.Begin();
	}

	public Task<byte> Commit() {
		return _kvAdder.Commit();
	}

	protected i64 getUnixTimeMillis(){
		return DateTimeOffset.Now.ToUnixTimeMilliseconds();
	}

	/// <summary>
	/// 建立一個初始的學習紀錄、"add"
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	protected I_LearnKV mkLearnKV_add(i64 id){
		var learn = new WordKV(){
			bl = BlPrefix.join(BlPrefix.Learn, "")
		};
		learn.setVStr(LearnEnum.add.ToString());
		learn.setKI64(id);
		learn.kDesc = KDesc.fKey.ToString();
		learn.ct = getUnixTimeMillis();
		learn.ut = getUnixTimeMillis();
		return (I_LearnKV)learn;
	}

/// <summary>
/// 錄id芝詞芝初添者(曩未嘗被添者)
/// </summary>
	public List<i64> initAddedWordIds{get; protected set;} = new();
/// <summary>
/// 錄id芝詞ʹ屬性芝既存ʹ詞ˋ新得者
/// </summary>
	public List<i64> newlyAddedPropIds{get; protected set;} = new();



	/**
	 * 用于 從txt詞表中取(無Learnˉ屬性 之 諸JoinedWordᵘ)後再添厥入庫
	 * 成功則自動添一Learn(belong=add)
	 */
	public async Task<zero> TxAddAsync(IList<I_FullWordKV> words) {
		var wordSeeker = new WordSeeker();

		var initAddedWordIds = new List<i64>();//錄id芝詞芝初添者(曩未嘗被添者)
		var newlyAddedPropIds = new List<i64>();//錄id芝詞ʹ屬性芝既存ʹ詞ˋ新得者
		foreach(var wordToAdd in words){
			var existedWord = await wordSeeker.SeekJoinedWordKVByTextEtBl(
				wordToAdd.textWord.text_()
				, wordToAdd.textWord.bl
			);
			if(existedWord == null){ //表中原無此詞則直ᵈ添
				await _kvAdder.TxAddAsync(wordToAdd.textWord);
				var lastId = await _kvAdder.GetLastId() ?? throw new Exception("cannot get last ID");
				var learn = mkLearnKV_add(lastId);
				await _kvAdder.TxAddAsync(learn);
				initAddedWordIds.Add(lastId);
			}//~if(existedWord == null)表中原無此詞
			else{//表中既有此詞>
				var propToAdd = _wordDiff.diffProperty(wordToAdd, existedWord);
				var oldWordId = existedWord.textWord.id;
				var hasAddedProp = false;
				foreach(var neoProp in propToAdd){
					hasAddedProp = true;
					neoProp.setKI64(oldWordId);
					neoProp.kDesc = KDesc.fKey.ToString();
					await _kvAdder.TxAddAsync(neoProp);
					var ua = await _kvAdder.GetLastId();
					if(ua != null){
						newlyAddedPropIds.Add((long)ua);
					}else{
						throw new Exception("cannot get last ID");
					}
				}//~foreach(var neoProp in propToAdd)
				if(hasAddedProp){
					var learn = mkLearnKV_add(oldWordId);
					await _kvAdder.TxAddAsync(learn);
				}
			}//~if(existedWord == null)表中既有此詞
		}//~foreach(var wordToAdd in words)
		this.initAddedWordIds = initAddedWordIds;
		this.newlyAddedPropIds = newlyAddedPropIds;
		return 0;
	}

	public async Task<zero> SetTx(IDbContextTransaction transaction) {
		_tx = transaction;
		await _kvAdder.SetTx(transaction);
		return 0;
	}


}
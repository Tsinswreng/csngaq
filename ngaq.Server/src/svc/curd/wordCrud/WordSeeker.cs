//using System.Data.Entity;
using db;
using Microsoft.EntityFrameworkCore;
using model.consts;
using ngaq.Core.model;
using ngaq.Core.model.wordIF;
using ngaq.Core.Svc.Crud.WordCrud.IF;
using ngaq.model.consts;

namespace ngaq.Server.Svc.Crud.WordCrud;


public class WordSeeker:
	IDisposable
	,I_SeekFullWordKVByIdAsy
{
	public WordSeeker() {

	}

	~WordSeeker(){
		Dispose();
	}
	public void Dispose() {
		dbCtx?.Dispose();
	}

	public NgaqDbCtx dbCtx{get;set;} = new();

	public async Task< I_FullWordKv? > SeekFullWordKVByIdAsy(i64 id){
		// var all = await dbCtx.WordKV.ToListAsync();//+
		// for(u64 i = 0; i < (u64)all.Count; i++){
		// 	var e = all[(int)i];
		// 	G.log(e.id);
		// 	if(e.id == id){
		// 		G.logJson(e);
		// 		break;
		// 	}
		// }

		var textWords = await dbCtx.WordKV.Where(
			e=>e.id == id
			//&& (e.bl??"").StartsWith(BlPrefix.TextWord)
		).ToListAsync();

		if(textWords.Count == 0){
			return null;
		}
		if(textWords.Count > 1){
			throw new Exception("id: "+id+"\nMultiple text words found for the same id.");
		}
		var textWord = textWords[0];

		IList<I_LearnKv> learns = await dbCtx.WordKV.Where(e=>
			e.kI64 == textWord.id
			&& (e.bl??"").StartsWith(BlPrefix.Learn)
			&& e.kDesc == KDesc.fKey.ToString()
		).Cast<I_LearnKv>()
		.ToListAsync();

		IList<I_PropertyKv> propertys = await dbCtx.WordKV.Where(e=>
			e.kI64 == textWord.id
			&& (e.bl??"").StartsWith(BlPrefix.Property)
			&& e.kDesc == KDesc.fKey.ToString()
		).Cast<I_PropertyKv>()
		.ToListAsync();

		var ans = new FullWord(){
			textWord = (I_TextWordKV)textWord
			,learns = (IList<I_LearnKv>)learns
			,propertys = (IList<I_PropertyKv>)propertys
		};
		return ans;
	}

	/// <summary>
	///
	/// </summary>
	/// <param name="text"></param>
	/// <param name="bl"> 不含前綴 </param>
	/// <returns></returns>
	public async Task< I_FullWordKv? > SeekJoinedWordKVByTextEtBl(str text, str bl){
		var fullBl = BlPrefix.join(BlPrefix.TextWord, bl);
		var textWordIds = await dbCtx.WordKV.Where(e=>
			e.kStr == text
			&& e.bl == fullBl
		).Select(e=>e.id)
		.ToListAsync();
		if(textWordIds.Count == 0){
			return null;
		}
		if(textWordIds.Count > 1){
			throw new Exception(
				"text: "+text
				+"\nbl: "+bl
				+"\nMultiple text words found for the same id."
			);
		}
		var textWordId = textWordIds[0];
		var ans = await SeekFullWordKVByIdAsy(textWordId);
		return ans;
	}
}

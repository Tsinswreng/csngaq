using ngaq.Server.db.ngaq4;
using ngaq.Core.svc.ngaq4;
using ngaq.Server.db.crud;
using ngaq.Core.model.ngaq4;
using ngaq.Core.model;
namespace ngaq.Server.scripts;

public class Ngaq4Migrate{
	/*
	using ngaq.Server.scripts;
	new Ngaq4Migrate().run();
	 */
	public async Task<zero> Run(){
		var dbConnStr = "Data Source=E:/_code/ngaq/db/userDb/user-1.sqlite";
		var ngaq4Db = new GetNgaq4Words(dbConnStr);
		var converter = new Ngaq4ModToWordKV();
		// foreach(var word in ngaq4Db.GetTextWords()){
		// 	G.logJson(word);
		// }
		var joinedWord = ngaq4Db.GetAllJoinedWords();
		using(var kvAdder = new KVAdder(nameof(WordKv))){
			await kvAdder.Begin();

			for(var i = 0; i < joinedWord.Count; i++){
				var word = joinedWord[i];
				var textWord = converter.convertTextWord(word.textWord);
				var id = await kvAdder.TxAddAsync(textWord);
				if(id==null){throw new Exception("id is null");}
				var learns = word.learns.Select(
					e=>{e.wid = (long)id ; var ans = converter.convertLearn(e);return ans;}
				).ToList();
				var propertys = word.propertys.Select(
					e=>{e.wid = (long)id ; var ans = converter.convertProperty(e);return ans;}
				).ToList();
				for(var j = 0; j < learns.Count; j++){
					await kvAdder.TxAddAsync(learns[j]);
				}

				for(var j = 0; j < propertys.Count; j++){
					await kvAdder.TxAddAsync(propertys[j]);
				}
			}

			await kvAdder.Commit();
		}
		return 0;
	}
}
using ngaq.Server.db.ngaq4;
using ngaq.Core.svc.ngaq4;
using ngaq.Server.svc.crud;
using ngaq.Core.model.ngaq4;
using ngaq.Core.model;
namespace ngaq.Server.scripts;

public class Ngaq4Migrate{
	/*
	using ngaq.Server.scripts;
	new Ngaq4Migrate().run();
	 */
	public async Task<unit> Run(){
		var dbConnStr = "Data Source=E:/_code/ngaq/db/userDb/user-1.sqlite";
		var ngaq4Db = new GetNgaq4Words(dbConnStr);
		var converter = new Ngaq4ModToWordKV();
		// foreach(var word in ngaq4Db.GetTextWords()){
		// 	G.logJson(word);
		// }
		var TextWord5 = ngaq4Db.GetTextWords().Select(e=>converter.convertTextWord(e));
		var Learn5 = ngaq4Db.GetLearns().Select(e=>converter.convertLearn(e));
		var Property5 = ngaq4Db.GetLearns().Select(e=>converter.convertLearn(e));
		using(var kvAdder = new KVAdder(nameof(WordKV))){
			await kvAdder.Begin();
			// for(var i = 0; i < 10000; i++){
			// 	G.log(i);
			// }
			for(var i = 0; i < TextWord5.Count(); i++){
				G.log("a"+i);
				await kvAdder.TxAddAsync(TextWord5.ElementAt(i));
			}
			for(var i = 0; i < Learn5.Count(); i++){
				G.log("b"+i);
				await kvAdder.TxAddAsync(Learn5.ElementAt(i));
			}
			for(var i = 0; i < Property5.Count(); i++){
				G.log("c"+i);
				await kvAdder.TxAddAsync(Property5.ElementAt(i));
			}
			await kvAdder.Commit();
		}
		return 0;
	}
}
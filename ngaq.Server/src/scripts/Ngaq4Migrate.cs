using ngaq.Server.db.ngaq4;
namespace ngaq.Server.scripts;

public class Ngaq4Migrate{
	/*
	using ngaq.Server.scripts;
	new Ngaq4Migrate().run();
	 */
	public unit run(){
		var dbConnStr = "Data Source=E:/_code/ngaq/db/userDb/user-1.sqlite";
		var ngaq4Db = new GetNgaq4Words(dbConnStr);

		foreach(var word in ngaq4Db.GetTextWords()){
			G.logJson(word);
		}

		foreach(var word in ngaq4Db.GetPropertys()){
			G.logJson(word);
		}

		foreach(var word in ngaq4Db.GetLearns()){
			G.logJson(word);
		}
		return 0;
	}
}
/*
sqlite select * from textWord 返回對象作爲csharp TextWord對象
 */

using System.Data.SQLite;
using ngaq.Core.model;
using ngaq.Core.model.ngaq4;
using Dapper;
namespace ngaq.Server.db.ngaq4;




public class GetNgaq4Words {
	public GetNgaq4Words(str connStr) {
		_connStr = connStr;
		_conn = new SQLiteConnection(connStr);
		if(_conn==null){
			throw new Exception("Connection string is null");
		}
	}

	~GetNgaq4Words(){
		Dispose();
	}
	public void Dispose() {
		if (_conn!= null) {
			_conn.Close();
			_conn.Dispose();
		}
	}
	protected str _connStr;
	protected SQLiteConnection _conn;

	protected str _geneSql(str tbl){
		return $"SELECT * FROM {tbl}";
	}

	public JoinedWord4 GetJoinedWord(TextWord4 textWord){
		var id = textWord.id;
		var learns = GetLearnsByWid(id);
		var propertys = GetPropertysByWid(id);
		return new JoinedWord4{
			textWord = textWord,
			learns = learns,
			propertys = propertys
		};
	}

	public IList<JoinedWord4> GetAllJoinedWords(){
		var textWords = GetTextWords();
		var result = new List<JoinedWord4>();
		foreach(var textWord in textWords){
			var joinedWord = GetJoinedWord(textWord);
			result.Add(joinedWord);
		}
		return result;
	}


	public IList<Learn4> GetLearnsByWid(i64 wid){
		var sql = "SELECT * FROM learn WHERE wid = @wid";
		return _conn.Query<Learn4>(sql, new { wid = wid }).ToList();
	}

	public IList<Property4> GetPropertysByWid(i64 wid){
		var sql = "SELECT * FROM property WHERE wid = @wid";
		return _conn.Query<Property4>(sql, new { wid = wid }).ToList();
	}

	public IList<TextWord4> GetTextWords() {
		var connection = _conn;
		return connection.Query<TextWord4>("SELECT * FROM textWord").ToList();
	}
	[Obsolete]
	public IList<Property4> GetPropertys(){
		var sql = _geneSql("property");
		return _conn.Query<Property4>(sql).ToList();
	}
	[Obsolete]
	public IList<Learn4> GetLearns(){
		var sql = _geneSql("learn");
		return _conn.Query<Learn4>(sql).ToList();
	}


}


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

	public JoinedWord GetJoinedWord(TextWord textWord){
		var id = textWord.id;
		var learns = GetLearnsByWid(id);
		var propertys = GetPropertysByWid(id);
		return new JoinedWord{
			TextWord = textWord,
			Learns = learns,
			Propertys = propertys
		};
	}

	public IList<JoinedWord> GetAllJoinedWords(){
		var textWords = GetTextWords();
		var result = new List<JoinedWord>();
		foreach(var textWord in textWords){
			var joinedWord = GetJoinedWord(textWord);
			result.Add(joinedWord);
		}
		return result;
	}


	public IList<Learn> GetLearnsByWid(i64 wid){
		var sql = "SELECT * FROM learn WHERE wid = @wid";
		return _conn.Query<Learn>(sql, new { wid = wid }).ToList();
	}

	public IList<Property> GetPropertysByWid(i64 wid){
		var sql = "SELECT * FROM property WHERE wid = @wid";
		return _conn.Query<Property>(sql, new { wid = wid }).ToList();
	}

	public IList<TextWord> GetTextWords() {
		var connection = _conn;
		return connection.Query<TextWord>("SELECT * FROM textWord").ToList();
	}
	[Obsolete]
	public IList<Property> GetPropertys(){
		var sql = _geneSql("property");
		return _conn.Query<Property>(sql).ToList();
	}
	[Obsolete]
	public IList<Learn> GetLearns(){
		var sql = _geneSql("learn");
		return _conn.Query<Learn>(sql).ToList();
	}


}


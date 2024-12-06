/*
sqlite select * from textWord 返回對象作爲csharp TextWord對象
 */

using System.Data.SQLite;


public class GetNgaq4Words {
	public static List<TextWord> GetTextWords(string connectionString) {
		using (var connection = new SQLiteConnection(connectionString)) {
			// 使用 Dapper 的 Query 方法将结果映射到 TextWord 对象列表
			return connection.Query<TextWord>("SELECT * FROM textWord").ToList();
		}
	}
}
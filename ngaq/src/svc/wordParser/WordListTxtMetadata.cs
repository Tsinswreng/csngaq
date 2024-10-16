namespace ngaq.svc.wordParser;

public class WordListTxtMetadata{
	/// <summary>
	/// 語言
	/// </summary>
	public str? belong{get;set;}
	/// <summary>
	/// 單詞分隔符
	/// </summary>
	public str? delimiter{get;set;}

	public static WordListTxtMetadata Parse(str txt){
		var ans = std.Text.Json.JsonSerializer.Deserialize<WordListTxtMetadata>(txt);
		if(ans?.belong == null || ans?.delimiter == null){
			throw new std.Exception("ans?.belong == null || ans?.delimiter == null");
		}
		return ans;
	}
}
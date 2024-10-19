using System.Text;
using ngaq.svc.wordParser;
namespace test.parser;

public class TestWordParser{

	public static async Task _Main(){
		var path = "E:/_code/ngaq/srcWordList/jap/japanese.ngaq";
		var reader = new NextCharReader(path);
		var parser = new WordParser(reader);
		parser.encoding = Encoding.UTF8;
		var ans = await parser.Parse();
		G.logJson(ans);
	}
}
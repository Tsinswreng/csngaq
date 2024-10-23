/*0123456789*/
using ngaq.svc.wordParser;
namespace test.parser;


public class TestNextCharReader{
	public static async Task _Main(){
		var path = "E:/_code/csngaq/test/src/parser/TestNextCharReader.cs";
		var reader = new NextCharReader(path);
		for(;;){
			var c = reader.getNextByte();
			if(c == -1) break;
			G.logNoLn(c+" ");
		}
	}
}
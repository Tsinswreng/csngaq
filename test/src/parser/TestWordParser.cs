using System.Text;
using ngaq.svc.wordParser;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
namespace test.parser;

public class TestWordParser{

	public static async Task _Main(){
 		//BenchmarkRunner.Run<TestWordParser>();
		await new TestWordParser().Method();
	}

	[Benchmark]
	public async Task Method(){
		var path = "E:/_code/ngaq/srcWordList/jap/japanese.ngaq";
		//var path = "E:/_code/csngaq/test/assets/eng.ngaq";
		//var path = "E:/_code/ngaq/srcWordList/eng/english.ngaq";
		var reader = new NextCharReader(path);
		var parser = new WordParser(reader);
		parser.encoding = Encoding.UTF8;
		Stopwatch sw = new Stopwatch();
		sw.Start();
		//50001行、 1,043,388 字节
		var ans = await parser.Parse();
		sw.Stop();
		G.log("Elapsed time: " + sw.ElapsedMilliseconds + " ms");
		G.logJson(ans);
		// var concated = WordParser.concatBack(ans);
		// var str = string.Join("", concated);
		//G.log(str);
	}
}
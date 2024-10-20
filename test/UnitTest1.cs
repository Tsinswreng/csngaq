#define DEBUG
namespace test;

/* 
cd /e/_code/csngaq/test && dotnet test --filter "FullyQualifiedName~test.UnitTest1.Test1" --logger "console;verbosity=detailed"
 */
public class UnitTest1{
	[Fact]
	public async Task Test1(){
		System.Console.OutputEncoding = System.Text.Encoding.UTF8;
		await test.parser.TestWordParser._Main();
		System.Console.WriteLine("________________");
	}
}


// interface I1{
// 	int age{get;set;}
// }

// struct S1:I1{
// 	public int age{get;set;}
// }


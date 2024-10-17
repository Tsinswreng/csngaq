namespace test;


/* 
cd /e/_code/csngaq/test && dotnet test --filter "FullyQualifiedName~test.UnitTest1.Test1" --logger "console;verbosity=detailed"
 */
public class UnitTest1{
	[Fact]
	public void Test1(){
		G.log("123------------");
	}
}
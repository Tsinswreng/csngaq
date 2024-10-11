using System;
using System.IO;
/** =Global functions */
public static class G {

	public const str main = "ngaq";
	public const str test = "test";

	public static bool refEq(object? o1, object? o2){
		return object.ReferenceEquals(o1, o2);
	}

	/** 
	 *  Compare two LITERAL strings for O(1) by ref,
	 *  only for literal, DO NOT use for `new String()`
	 */
	public static bool lstrEq(str? s1, str? s2){
		return refEq(s1, s2);
	}

	/// <summary>
	/// notNull
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="v"></param>
	/// <param name="errMsg"></param>
	/// <returns></returns>
	/// <exception cref="NullReferenceException"></exception>
	public static T nn<T>(T? v, str errMsg=""){
		if(v == null){
			throw new NullReferenceException(errMsg);
		}
		return v;
	}

	public static str internStr(ref str s){
		s = string.Intern(s);
		return s;
	}

// 	public static str getCsprojDir(){
// 		string currentDirectory = Directory.GetCurrentDirectory();
// #if DEBUG
// 		string projectDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\")); // 向上两级目录
// 		return projectDirectory;
// #else
// 		return currentDirectory;
// #endif
// 	}

	/// <summary>
	/// get base dir of project  E:/_code/rime-tools
	/// the same as the dir of .gitignored file
	/// posix style path, not ends with "/"
	/// </summary>
	/// <returns></returns>
	public static str getBaseDir(){
		//dotnet run -> E:\_code\rime-tools\main\bin\Debug\net8.0\
		//dotnet test -> E:\_code\rime-tools\test\bin\Debug\net8.0\
		string domainDir = AppDomain.CurrentDomain.BaseDirectory;
		string baseDir = Path.GetFullPath(Path.Combine(domainDir, @"../../../../"));
		if(baseDir.EndsWith("/")){
			baseDir = baseDir.Substring(0, baseDir.Length-1);
		}
		return baseDir.Replace("\\", "/");
	}

	public static str log(){
		#if DEBUG
		System.Console.WriteLine();
		#endif
		return "";
	}

	public static str log<T>(T s){
		#if DEBUG
		System.Console.WriteLine(s);
		#endif
		return s?.ToString()??"";
	}


}
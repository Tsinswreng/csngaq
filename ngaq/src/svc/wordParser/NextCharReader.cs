/* 
c# 異步流式讀文件、每次讀一部分。
讀內容的時候異步。
不用指定字符編碼、直接返回數字。
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using word = byte;
using Chunk = System.ArraySegment<byte>; // 其Count是數組片段之長、非內部數組之長。

namespace ngaq.svc.wordParser;

public class NextCharReader: I_GetNextByte, IDisposable{
	
	public str path{get; set;}

	[Obsolete]
	public Encoding? encoding{get; set;} = null;

	public FileStream fs{get; set;}

	protected byte[] _buffer = new byte[1];

	protected i32 _byteRead = -1;

	public i32 bufferSize{get; set;} = 0x100000;

	public i32 pos{get; set;} = 0;
	//public Chunk nextChunk{get; set;} = default;
	public i64 byteSize{get; set;}
	public Chunk curChunk{get; set;} = default;
	public i32 chunkPos{get; set;} = 0;//下次將讀取的位置

	//public word[] buffer{get; set;}


	public NextCharReader(str path){
		this.path = path;
		//buffer = new word[bufferSize];
		fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		byteSize = fs.Length;
	}


	public async Task< Chunk > ReadNextChunk(){
		byte[] buffer = new byte[bufferSize];
		i32 bytesRead = await fs.ReadAsync(buffer, 0, bufferSize);
		//i32 bytesRead = fs.Read(buffer, 0, bufferSize);
		if(bytesRead <= 0){
			return default; //Array=null, Count=0, Offset=0
		}
		//  return buffer.Take(bytesRead).ToArray(); // 只返回已读取的数据
		return new Chunk(buffer, 0, bytesRead);
	}


	//example
	// public static async IAsyncEnumerable<byte[]> ReadFileAsync
	// (
	// 	string filePath
	// 	, int bufferSize
	// 	, CancellationToken cancellationToken = default
	// ){
	// 	using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true);

	// 	byte[] buffer = new byte[bufferSize];
	// 	int bytesRead;

	// 	while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0){
	// 		// 创建一个新数组，并将读取到的数据复制到其中
	// 		var data = new byte[bytesRead];
	// 		Array.Copy(buffer, data, bytesRead);
	// 		yield return data;
	// 	}
	// }
	public bool isEnd{get; set;} = false;
	public bool hasNext(){
		return pos < byteSize;
	}

	public async Task<word> GetNextByte(){
		if(chunkPos >= curChunk.Count){
			curChunk = await ReadNextChunk();
			chunkPos = 0;
			if(chunkPos >= curChunk.Count){
				//return -1; // EOF
				isEnd = true;
				return 0;
			}
		}
		var ans = curChunk[chunkPos];
		chunkPos++;
		pos++;
		return ans;
	}

	public void Dispose(){
		fs.Dispose();
	}

	~NextCharReader(){
		Dispose();
	}
}


/* 

c# 如何異步以utf8逐碼點讀文件?
比如我的文件是:   一1a𠂇😍
讀取時則依次返回 [一, 1, a, 𠂇, 😍]
注意: 以下字符的unicode編碼爲:
𠂇:0x20087
😍:0x1f60d
超過了0xffff。要把他們當成整個字符讀取、不要拆開。
 */

/* 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public static class Utf8Reader
{
    public static async Task<IEnumerable<string>> ReadUtf8CodePointsAsync(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true); // useAsync: true for asynchronous I/O
        using var reader = new StreamReader(stream, Encoding.UTF8, true, 4096); // detectEncodingFromByteOrderMarks: true

        var codePoints = new List<string>();
        char[] buffer = new char[4096]; // Adjust buffer size as needed
        int charsRead;

        while ((charsRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            for (int i = 0; i < charsRead; i++)
            {
                // Handle surrogate pairs for code points > U+FFFF
                if (char.IsHighSurrogate(buffer[i]) && i + 1 < charsRead && char.IsLowSurrogate(buffer[i + 1]))
                {
                    string codePoint = new string(buffer, i, 2);
                    codePoints.Add(codePoint);
                    i++; // Skip the low surrogate
                }
                else
                {
                    codePoints.Add(buffer[i].ToString());
                }
            }
        }

        return codePoints;
    }
}


// Example usage:
public async Task ExampleAsync()
{
    string filePath = "your_file.txt"; // Replace with your file path
    try
    {
        IEnumerable<string> codePoints = await Utf8Reader.ReadUtf8CodePointsAsync(filePath);
        foreach (string codePoint in codePoints)
        {
            Console.WriteLine(codePoint); // Output each code point
            Console.WriteLine($"Code point in hex: 0x{Convert.ToInt32(codePoint[0]).ToString("X4")}"); //Output code point in hex
        }
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"File not found: {filePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
 */


/* 

Result* getResult(args){
	//do sth.
	Result* result = new Result();
	result->value = xxx;
	return result;
}

int getResult(Result* result, args){
	//do sth.
	result->value = xxx;
	return 0;
}

 */

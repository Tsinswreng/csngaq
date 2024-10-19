using System;
using System.IO;
using System.Text;

namespace ngaq.svc.wordParser;

public class NextCharReader: I_GetNextChar_str, IDisposable{
	
	public str path{get; set;}
	public Encoding encoding{get; set;} = Encoding.UTF8;

	public FileStream fs{get; set;}

	protected byte[] _buffer = new byte[1];

	protected i32 _byteRead = -1;

	public NextCharReader(str path, Encoding encoding){
		this.path = path;
		this.encoding = encoding;

		fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
	}

	public NextCharReader(str path):this(path, Encoding.UTF8){
		
	}

	public async Task<str?> GetNextChar(){
		
		while( (_byteRead = await fs.ReadAsync(_buffer, 0, 1)) > 0 ){
			char c = encoding.GetChars(_buffer)[0];
			return c.ToString();
		}
		return null;
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
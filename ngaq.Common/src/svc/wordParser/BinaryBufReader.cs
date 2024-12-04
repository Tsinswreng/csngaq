/* 
1234567890
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class AsyncFileReader{
	public static async IAsyncEnumerable<byte[]> ReadFileAsync(string filePath, int bufferSize, CancellationToken cancellationToken = default){
		using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true);

		byte[] buffer = new byte[bufferSize];
		int bytesRead;

		while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0){
			// 创建一个新数组，并将读取到的数据复制到其中
			var data = new byte[bytesRead];
			Array.Copy(buffer, data, bytesRead);
			yield return data;
		}
	}



	public static async Task _Main(){
		string filePath = "E:/_code/csngaq/ngaq/src/svc/wordParser/BinaryBufReader.cs"; // 请替换为你的文件路径
		int bufferSize = 4096; // 缓冲区大小，可以根据需要调整

		try{
			await foreach (var buffer in ReadFileAsync(filePath, bufferSize)){
				G.logNoLn($"读取到 {buffer.Length} 字节数据{"\n"}");
				foreach (var b in buffer){
					G.logNoLn(b+" ");
				}
				//  在此处处理读取到的字节数组 buffer
			}
		}
		catch (FileNotFoundException){
			Console.WriteLine($"文件未找到: {filePath}");
		}
		catch (OperationCanceledException){
			Console.WriteLine("操作已取消");
		}
		catch (Exception ex){
			Console.WriteLine($"发生错误: {ex.Message}");
		}
	}
}
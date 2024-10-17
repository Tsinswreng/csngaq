using System;
using System.IO;
using System.Text;

namespace ngaq.svc.wordParser;

public class NextCharReader: I_GetNextChar, IDisposable{
	
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


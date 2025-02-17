using ngaq.Core.model;

namespace ngaq.Server;

public class Opt{

	protected static Opt _inst {get; set;}
	public static Opt inst(){
		if(_inst == null){
			_inst = new Opt();
		}
		return _inst;
	}
	public str tblName_WordKV {get; set;} = nameof(WordKv);
}

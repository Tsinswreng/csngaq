namespace ngaq.Core.Model.sample;


public class TextWordSample{

	public static TextWordSample? inst = null;
	public static TextWordSample getInst(){
		if(inst == null){
			inst = new TextWordSample();
		}
		return inst;
	}

	public I_TextWordKV sample{get;set;}

	public TextWordSample(){
		sample = null!;
		_init();
	}

	protected zero _init(){
 		sample= new WordKv(){
			id = 1
			//,bl = "english"
			,ct = 1707866373693
			,ut = 1737866373693
		};
		sample.text_("audacious");
		sample.lang_("english");
		return 0;
	}
}

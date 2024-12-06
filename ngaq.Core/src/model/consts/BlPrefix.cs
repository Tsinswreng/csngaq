namespace ngaq.model.consts;

public class BlPrefix{
	public const str delimiter = ":";
	public static str parse(str prefix, str name){
		return prefix + delimiter + name;
	}

	public const str TextWord = "TextWord";
	public const str Learn = "Learn";
	public const str Property = "Property";
}

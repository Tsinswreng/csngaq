namespace ngaq.model.consts;

public class BlPrefix{
	public const str delimiter = ":";
	public static str join(str prefix, str name){
		return prefix + delimiter + name;
	}

	public static (str, str) split(str full){
		var idx = full.IndexOf(delimiter);
		if(idx < 0){
			throw new ArgumentException("Invalid prefix format: " + full);
		}
		return (full.Substring(0, idx), full.Substring(idx + 1));
	}

	public const str TextWord = "TextWord";
	public const str Learn = "Learn";
	public const str Property = "Property";
}

namespace ngaq.model.consts;

public class BlPrefix{
	public const str delimiter = ":";
	public static str parse(str prefix, str name){
		return prefix + delimiter + name;
	}
}

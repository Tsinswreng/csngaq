using ngaq.Core.model;
using ngaq.Core.model.wordIF;
using tools;

namespace ngaq.Core.Svc;

public class DiffWord{
	/**
	 * 以ut潙準取差集
	 * w1有洏w2無 者
	 * @param w1 待加者
	 * @param w2 已有者
	 * @returns 未加過之prop
	 */
	public List<I_PropertyKv> diffProperty(
		I_FullWordKv w1
		,I_FullWordKv w2
	){
		if(
			w1.textWord.kStr != w2.textWord.kStr
			|| w1.textWord.bl != w2.textWord.bl
		){
			var errMsg = $"w1 id: {w1.textWord.id}, w2 id: {w2.textWord.id}\n"
				+"w1 and w2 are not the same word.";
			throw new ArgumentException(errMsg);
		}
		var diff = Tools.diffListIntoMap(
			w1.propertys
			,w2.propertys
			,(e)=>e.ut
		);
		List<I_PropertyKv> ans = [];
		foreach(var kvp in diff){
			ans.AddRange(kvp.Value);
		}
		return ans;
	}
}
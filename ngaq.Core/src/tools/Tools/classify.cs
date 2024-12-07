namespace tools;

public partial class Tools{
/**
 *
 * @param arr
 * @param keyOfMap 返ᵗ值ˋ作Map之鍵
 * @returns
 */
	public static Dictionary<Key, List<Ele>> classify<Ele, Key>(
		IList<Ele> arr
		,Func<Ele, Key>  keyOfMap
	)where Key:notnull
	{
		var ans = new Dictionary<Key, List<Ele>>();
		foreach(var e in arr){
			var key = keyOfMap(e);
			if(!ans.ContainsKey(key)){
				//ans[key] = new List<Ele>(){e};
				ans[key] = [e];
			}else{
				ans[key].Add(e);
			}
		}
		return ans;
		// return arr.GroupBy(keyOfMap)
		// 	.ToDictionary(g => g.Key, g => g.ToList());
	}
}
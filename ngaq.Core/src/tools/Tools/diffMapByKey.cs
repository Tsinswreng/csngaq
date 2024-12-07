namespace tools;

public partial class Tools {
/**
 * 取差集 支持 Map 和 Set(暫不支持)
 * 比较两个 Map，并返回一个新的 Map，该 Map 包含在第一个 Map 中但不在第二个 Map 中的键值对。
 * @param map1
 * @param map2
 * @returns
 */
	public static Dictionary<K, V> diffMapByKey<K, V>(
		Dictionary<K, V> map1
		, Dictionary<K, V> map2
	)where K:notnull
	{

		/*
	const ans = new Map<K, V>();
	map1.forEach((value, key) => {
		if (!map2.has(key)) {
			ans.set(key, value);
		}
	});
	return ans;
		 */
		return map1
			.Where(kvp => !map2.ContainsKey(kvp.Key))
			.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
	}

	// public static HashSet<K> DiffMapByKey<K, V>(HashSet<K> set1, HashSet<K> set2) {
	// 	return new HashSet<K>(set1.Except(set2));
	// }

	// public static Dictionary<K, V> DiffMapByKey<K, V>(Dictionary<K, V> map1, HashSet<K> set2) {
	// 	return map1.Where(kvp => !set2.Contains(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
	// }

	// public static HashSet<K> DiffMapByKey<K, V>(HashSet<K> set1, Dictionary<K, V> map2) {
	// 	return new HashSet<K>(set1.Where(key => !map2.ContainsKey(key)));
	// }


	// // Overload to handle null inputs gracefully.  Consider your desired behavior for nulls.
	// public static Dictionary<K, V>? DiffMapByKey<K, V>(Dictionary<K, V>? map1, Dictionary<K, V>? map2) {
	// 	if (map1 == null || map2 == null) return null; // Or throw an exception, or return an empty dictionary.
	// 	return map1.Where(kvp => !map2.ContainsKey(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
	// }

	// public static HashSet<K>? DiffMapByKey<K, V>(HashSet<K>? set1, HashSet<K>? set2) {
	// 	if (set1 == null || set2 == null) return null; // Or throw an exception, or return an empty HashSet.
	// 	return new HashSet<K>(set1.Except(set2));
	// }

}
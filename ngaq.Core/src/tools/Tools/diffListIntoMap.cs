namespace tools;

public partial class Tools{

/*
arr1=[
	{text:'a', num:1}
	,{text:'b', num:2}
	,{text:'c', num:3}
]
arr2=[
	{text:'a', num:1}
	,{text:'b', num:2}
	,{text:'d', num:4}
]
fn(arr1, arr2, (e)=>e.num) -> Map{3=>{text:'c', num:3}}
*/
	public static Dictionary<Fld, IList<ArrEle>> diffListIntoMap<ArrEle, Fld>(
		IList<ArrEle> arr1
		,IList<ArrEle> arr2
		,Func<ArrEle, Fld> fn
	)where Fld:notnull
	{
		var map1 = classify(arr1, fn);
		var map2 = classify(arr2, fn);
		var ans = diffMapByKey(map1, map2);
		return ans;
	}
}
namespace Shr;


static class IListExt{
	public static T pop<T>(this IList<T> z){
		if(z.Count == 0){throw new InvalidOperationException("List is empty");}
		var last = z[z.Count-1];
		z.RemoveAt(z.Count-1);
		return last;
	}
}

public static class Tools{
	/// <summary>
	/// [[1], [2,3], [4,5]]// -> [[1,2,4],[1,2,5],[1,3,4],[1,3,5]]
	/// [[1,2],[3],[4,5],[6]] // -> 1346,1356,2346,2356
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="arr"></param>
	/// <returns></returns>
	public static IList<IList<T>> cartesianProduct<T>(IList<IList<T>> arr){
		if(arr.Count == 0){return [];}
		var pos = new List<i32>(arr.Count);
		for(var i = 0; i < arr.Count; i++){
			var u = arr[i];
			if(u.Count == 0){return [];}
			pos.Add(0);
		}
		var stack = new List<T>();
		IList<IList<T>> ans = [];
		var cnt = 0;
		var ipp = false;
		for(var i = 0;;cnt++){
			stack.Add(
				arr[i][pos[i]]
			);
			ipp = true;
			if(stack.Count == arr.Count){
				ans.Add(new List<T>(stack));
				if(stack.Count == 0){return ans;}
				stack.pop();
				pos[i]++;
				ipp=false;
			}
			if(pos[i] == arr[i].Count){
				for(;i>=0;){
					if( pos[i]+1 >= arr[i].Count ){
						pos[i] = 0;
						i--;
						if(stack.Count == 0){return ans;}
						stack.pop();
					}else{
						pos[i] += 1;
						break;
					}
					ipp=false;
				}
			}
			if(ipp){i++;}
		}//~outer for
		//return ans;
	}
}
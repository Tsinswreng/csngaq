using System.Collections.Generic;
namespace tools;

public interface I_CyclicArray<T>{
	i32 capacity {get; set;}
	i32 size {get; set;}
	bool isEmpty();
	bool isFull();
	i32 clear();
	i32 addBack(T item);
	T? addBackF(T item);
	T? rmBack();
	T backGet(i32 pos);
	i32 backSet(i32 pos, T item);

	i32 addFront(T item);
	T? addFrontF(T item);
	T? rmFront();
	T frontGet(i32 pos);
	i32 frontSet(i32 pos, T item);

	i32 capacityAdd(i32 adder);
	List<T> toList();
}
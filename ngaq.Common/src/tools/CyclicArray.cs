using System.Collections.Generic;
using System;
using System.Collections;
namespace tools;

public class CyclicArray<T>
	: I_CyclicArray<T>
	//, ICollection<T>
	//, IEnumerable<T>
{

	public CyclicArray(i32 capacity_){
		var z = this;
		if(capacity_ <= 0){
			throw new ArgumentOutOfRangeException("capacity must be positive");
		}
		z.capacity = capacity_;
		z._data = new T[capacity_];
	}
	public int capacity { get ; set ; } = 0;
	public int size { get; set ; } = 0;

	protected T[] _data { get ; set ; } = [];

	protected i32 _frontI = 0;
	protected i32 _backI = 0;

	public static i32 posAdd(i32 index, i32 capacity, i32 num){
		return (index + num) % capacity;
	}

	public static i32 posSub(i32 index, i32 capacity, i32 num){
		// AI 所作: return (index - num + capacity) % capacity;
		num = num % capacity;
		index -= num;
		if (index < 0){
			index += capacity;
		}
		return index;
	}

	public i32 addBack(T item){
		var z = this;
		if(z.isFull()){
			return -1;
		}
		if(z.isEmpty()){
			z._data[z._backI] = item;
		}else{
			z._backI = posAdd(z._backI, z.capacity, 1);
			z._data[z._backI] = item;
		}
		z.size++;
		return 0;
	}


	public T? addBackF(T item){
		var z = this;
		if(z.addBack(item) == 0){
			return default; // 无法将 null 转换为类型参数“T”，因为它可能是不可为 null 的值类型。请考虑改用“default(T)”。
		}
		var front = z.rmFront();
		z.addBack(item);
		return front;
	}

	public int addFront(T item){
		var z = this;
		if(z.isFull()){
			return -1;
		}
		if(z.isEmpty()){
			z._data[z._frontI] = item;
		}else{
			z._frontI = posSub(z._frontI, z.capacity, 1);
			z._data[z._frontI] = item;
		}
		z.size++;
		return 0;
	}

	public T? addFrontF(T item){
		var z = this;
		if(z.addFront(item) == 0){
			return default; // 无法将 null 转换为类型参数“T”，因为它可能是不可为 null 的值类型。请考虑改用“default(T)”。
		}
		var back = z.rmBack();
		z.addFront(item);
		return back;
	}

	public T backGet(i32 pos){
		var z = this;
		var index = posSub(z._backI, z.capacity, pos);
		return z._data[index];
	}

	public int backSet(i32 pos, T item){
		var z = this;
		var index = posSub(z._backI, z.capacity, pos);
		z._data[index] = item;
		return 0;
	}

	public int capacityAdd(int adder){
		if(adder < 0){
			throw new ArgumentOutOfRangeException("adder must be positive");
		}
		var neoData = new T[capacity + adder];
		for(i32 i = 0; i < size; i++){
			neoData[i] = frontGet(i);
		}
		capacity += adder;
		_frontI = 0;
		_backI = size - 1;
		_data = neoData;
		return 0;
	}

	public int clear(){
		Array.Clear(_data);
		_frontI = 0;
		_backI = 0;
		size = 0;
		return 0;
	}

	public T frontGet(i32 pos){
		return _data[posAdd(_frontI, capacity, pos)];
	}

	public int frontSet(i32 pos, T item){
		_data[posAdd(_frontI, capacity, pos)] = item;
		return 0;
	}

	public bool isEmpty(){
		return size == 0;
	}

	public bool isFull(){
		return size == capacity;
	}

	public T? rmBack(){
		if(isEmpty()){
			return default;
		}
		var back = backGet(0);
		_backI = posSub(_backI, capacity, 1);
		size--;
		return back;
	}

	public T? rmFront(){
		if(isEmpty()){
			return default;
		}
		var front = frontGet(0);
		_frontI = posAdd(_frontI, capacity, 1);
		size--;
		return front;
	}

	public List<T> toList(){
		var list = new List<T>(size);
		for(i32 i = 0; i < size; i++){
			list.Add(frontGet(i));
		}
		return list;
	}

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自分用ディクショナリー
[System.Serializable]
public class MyDictionary<T, J>
{
	[System.Serializable]
	class DictionaryMateriala<A, B>
	{
		public A key;
		public B value;
	}

	[SerializeField]
	List<DictionaryMateriala<T, J>> data = new List<DictionaryMateriala<T, J>>();

	//public J this[T key]
	//{
	//	get
	//	{
	//		foreach (var d in data)
	//		{
	//			if (d.key == key)
	//			{
	//				return d.value;
	//			}
	//		}
	//		return null;
	//	}
	//}
}
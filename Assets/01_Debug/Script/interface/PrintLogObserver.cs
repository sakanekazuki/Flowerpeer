using System;
using UnityEngine;

public class PrintLogObserver<T> : IObserver<T>
{
	public void OnCompleted()
	{
		Debug.Log("Completed");
	}

	public void OnError(Exception error)
	{
		Debug.LogError(error);
	}

	public void OnNext(T value)
	{
		Debug.Log(value);
	}
}

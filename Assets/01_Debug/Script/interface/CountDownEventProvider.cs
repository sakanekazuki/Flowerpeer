using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CountDownEventProvider : MonoBehaviour
{
	[SerializeField]
	int count;

	Subject<int> subject;

	public IObservable<int> countdownobservable => subject;

	private void Awake()
	{
		subject = new Subject<int>();

		StartCoroutine(ECount());
	}

	IEnumerator ECount()
	{
		var current = count;
		while (current > 0)
		{
			subject.OnNext(current);
			current--;
			yield return new WaitForSeconds(1.0f);
		}

		subject.OnNext(0);
		subject.OnCompleted();
	}

	private void OnDestroy()
	{
		subject.Dispose();
	}
}

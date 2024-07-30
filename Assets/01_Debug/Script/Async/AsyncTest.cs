using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncTest : MonoBehaviour
{
	private void Awake()
	{
		TestAsync().Forget();
	}

	public async UniTaskVoid TestAsync()
	{
		int time = 0;
		while (time < 10)
		{
			Debug.Log(time);
			await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
			time++;
		}
	}

	IEnumerator IETest()
	{
		int n = 0;
		while (n < 10)
		{
			n++;
			Debug.Log(n);
			yield return new WaitForSeconds(1.0f);
		}
	}
}
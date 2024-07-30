using System;
using UnityEngine;
using UniRx;

public class TestObserve : MonoBehaviour
{
    [SerializeField]
    CountDownEventProvider _countDownEventProvider;

    PrintLogObserver<int> _printLogObserver;

    IDisposable _disposable;

	private void Start()
	{
		//observe = new TestObserver<int>();

		//disposable = subject.countdownobservable.Subscribe(observe);

		_disposable = _countDownEventProvider.
			countdownobservable.
			Subscribe(
				x => Debug.Log(x),
				ex => Debug.LogError(ex),
				() => Debug.Log("Completed"));
	}

	private void OnDestroy()
	{
		_disposable?.Dispose();
	}
}

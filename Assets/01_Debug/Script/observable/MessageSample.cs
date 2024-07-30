using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MessageSample : MonoBehaviour
{
	[SerializeField]
	float _countTimeSeconds = 30f;

	readonly AsyncSubject<Unit> _onTimeUpAsyncSubject = new AsyncSubject<Unit>();

	public IObservable<Unit> OnTimeUpAsyncSubject => _onTimeUpAsyncSubject;

	IDisposable _disposable;

	private void Start()
	{
		_disposable = Observable
			.Timer(TimeSpan.FromSeconds(_countTimeSeconds))
			.Subscribe(_ =>
			{
				_onTimeUpAsyncSubject.OnNext(Unit.Default);
				_onTimeUpAsyncSubject.OnCompleted();
			});
	}

	private void OnDestroy()
	{
		_disposable?.Dispose();

		_onTimeUpAsyncSubject.Dispose();
	}
}

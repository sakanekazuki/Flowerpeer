using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// フェードイン・フェードアウト管理クラス
public class FadeIO : Singleton<FadeIO>
{
	// フェードをするためのImage
	Image fadeImg;

	// true = フェード中
	public bool IsFading
	{
		get;
		private set;
	}

	// フェードスピード
	[SerializeField]
	float fadeSpeed = 0.08f;

	private void Awake()
	{
		// 子オブジェクトからImageクラスを取得
		fadeImg = GetComponentInChildren<Image>();
	}

	/// <summary>
	/// フェードイン
	/// </summary>
	public async UniTask<bool> FadeIn()
	{
		CancellationToken token = this.GetCancellationTokenOnDestroy();
		// フェード状態にする
		IsFading = true;
		fadeImg.raycastTarget = true;
		fadeImg.color = Color.black;

		// 色がなくなるまでループ
		while (fadeImg.color.a > 0.1f)
		{
			// アルファを抜く
			fadeImg.color -= new Color(0, 0, 0, fadeSpeed);
			// 1フレーム待つ
			await UniTask.DelayFrame(1, cancellationToken: token);
		}

		// フェード終了
		IsFading = false;
		fadeImg.raycastTarget = false;

		return true;
	}

	/// <summary>
	/// フェードアウト
	/// </summary>
	public async UniTask<bool> FadeOut()
	{
		CancellationToken token = this.GetCancellationTokenOnDestroy();
		fadeImg.raycastTarget = true;
		// フェード状態にする
		IsFading = true;
		fadeImg.color = new Color(0, 0, 0, 0);

		// 色がなくなるまでループ
		while (fadeImg.color.a < 0.9f)
		{
			// アルファを抜く
			fadeImg.color += new Color(0, 0, 0, fadeSpeed);
			// 1フレーム待つ
			await UniTask.DelayFrame(1, cancellationToken: token);
		}

		// フェード終了
		IsFading = false;
		fadeImg.raycastTarget = false;

		return true;
	}
}
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// ディゾルブシェーダーでのフェード
public class DissolveFadeIO : Singleton<DissolveFadeIO>
{
	// フェードするイメージ
	Image fadeImg;
	// フェードの値を変更するマテリアル
	Material fadeMaterial;

	// true = フェード中
	public bool IsFading
	{
		get;
		private set;
	}

	// フェードスピード
	[SerializeField]
	float fadeSpeed = 0.08f;

	// フェードの値
	float fadeValue = 0;

	private void Awake()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;

		// 子オブジェクトからImageクラスを取得
		fadeImg = GetComponentInChildren<Image>();
		fadeMaterial = fadeImg.material;
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
		fadeValue = 0;

		// 色がなくなるまでループ
		while (fadeValue < 1)
		{
			fadeValue += fadeSpeed;
			// アルファを抜く
			fadeMaterial.SetFloat("_DizolbValue", fadeValue);
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
		fadeValue = 1;

		// 色がなくなるまでループ
		while (fadeValue > 0)
		{
			fadeValue -= fadeSpeed;
			// アルファを抜く
			fadeMaterial.SetFloat("_DizolbValue", fadeValue);

			// 1フレーム待つ
			await UniTask.DelayFrame(1, cancellationToken: token);
		}

		// フェード終了
		IsFading = false;
		fadeImg.raycastTarget = false;

		return true;
	}
}
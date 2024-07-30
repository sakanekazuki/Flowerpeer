using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// タイトルキャンバス
public class TitleCanvas : MonoBehaviour, ITitleCanvas
{
	//// クリック時に生成
	//[SerializeField]
	//GameObject clickEffect;

	// タイトルロゴ
	[SerializeField]
	GameObject logoObject;

	// ストーリーを表示するまでの時間
	[SerializeField]
	float storyDisplayTime = 5;

	// ストーリー
	[SerializeField]
	GameObject storyText;

	// ボタンを無視させるボタン
	[SerializeField]
	GameObject buttonIgnoreImage;

	CancellationTokenSource tokenSource;
	CancellationToken token;

	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	private void OnEnable()
	{
		storyText.SetActive(false);
		buttonIgnoreImage.SetActive(false);
		logoObject.SetActive(true);
		tokenSource = new CancellationTokenSource();
		token = tokenSource.Token;
		StoryTextDisplay(token).Forget();
	}

	private void OnDisable()
	{
		tokenSource.Cancel();
	}

	async UniTaskVoid StoryTextDisplay(CancellationToken token)
	{
		await UniTask.Delay(System.TimeSpan.FromSeconds(storyDisplayTime), cancellationToken: token);
		logoObject.SetActive(false);
		storyText.SetActive(true);
	}

	/// <summary>
	/// ステージ選択画面に移動
	/// </summary>
	public void ToStageSelect()
	{
		buttonIgnoreImage.SetActive(true);
		GM_Title.Instance.GetComponent<IGM_Title>().ToStageSelect();
	}

	/// <summary>
	/// オプション画面に移動
	/// </summary>
	public void ToOption()
	{
		buttonIgnoreImage.SetActive(true);
		GM_Title.Instance.GetComponent<IGM_Title>().ToOption();
	}

	/// <summary>
	/// ゲーム終了
	/// </summary>
	public void GameQuit()
	{
		buttonIgnoreImage.SetActive(true);
		LevelManager.GameQuit();
	}

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生する音</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	void ITitleCanvas.LogoDisplay()
	{
		logoObject.SetActive(true);
		storyText.SetActive(false);
		StoryTextDisplay(token).Forget();
	}
}
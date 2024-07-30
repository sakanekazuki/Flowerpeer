using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

// ステージ選択キャンバス
public class StageSelectCanvas : MonoBehaviour, IStageSelectCanvas
{
	// ボタンを選択出来ない状態にするオブジェクト
	[SerializeField]
	GameObject buttonIgnoreObject;

	// 説明するテキスト
	[SerializeField]
	Text stageNameText;

	// スコア表示テキスト
	[SerializeField]
	TextMeshProUGUI scoreText;

	// プレビュー
	[SerializeField]
	Image previewImage;

	// プレビュー
	[SerializeField]
	List<Sprite> previewSprites = new List<Sprite>();

	// 最初のスプライト
	Sprite defaultPreviewSprite;

	// ミッション進行具合を表示するオブジェクト
	[SerializeField]
	List<Image> missionProgresses = new List<Image>();

	// ミッションクリアしていない際のスプライト
	[SerializeField]
	Sprite notClearedSprite;

	// ミッションクリア時のスプライト
	[SerializeField]
	Sprite clearSprite;

	private void Awake()
	{
		buttonIgnoreObject.SetActive(false);
		defaultPreviewSprite = previewImage.sprite;
	}

	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	private void OnDisable()
	{
		stageNameText.text = "";
		previewImage.sprite = defaultPreviewSprite;
		scoreText.text = "";
	}

	/// <summary>
	/// ゲームに移動
	/// </summary>
	/// <param name="stageNumber">ステージの番号</param>
	public async void ToGame(int stageNumber)
	{
		buttonIgnoreObject.SetActive(true);
		GM_Main.stageNumber = stageNumber;
		GameInstance.gameState = GameInstance.GameState.MainGame;
		// シーンを読み込む
		LevelManager.OpenLevel("MainScene").Forget();
		// シーンを切り替えない
		LevelManager.canLevelMove = false;
		// フェードアウト
		FadeIO.Instance.FadeOut().Forget();
		// フェードとシーン読み込み終了待ち
		await UniTask.WaitUntil(() => { return !LevelManager.IsLevelLoading && !FadeIO.Instance.IsFading; });
		// シーン移動を可能にする
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// タイトルに移動
	/// </summary>
	public void ToTitle()
	{
		GM_Title.Instance.GetComponent<IGM_Title>().ToTitle();
	}

	/// <summary>
	/// オプションに移動
	/// </summary>
	public void Option()
	{
		GM_Title.Instance.GetComponent<IGM_Title>().ToOption();
	}

	/// <summary>
	/// 戻る
	/// </summary>
	public void Return()
	{
		GM_Title.Instance.GetComponent<IGM_Title>().Return();
	}

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生する音</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	void IStageSelectCanvas.SetLineMagnitude(float magnitude, bool isClear)
	{
		if (isClear)
		{
			scoreText.text = Mathf.Floor(magnitude).ToString() + "cm";
		}
		else
		{
			//scoreText.text = "○○○○cm";
			scoreText.text = "未クリア";
		}
	}

	void IStageSelectCanvas.SetPreviewImage(int number)
	{
		previewImage.sprite = previewSprites[number - 1];
		stageNameText.text = StageManager.Instance.StageNames[number - 1];
	}

	void IStageSelectCanvas.MissionProgression(int progress)
	{
		foreach (var e in missionProgresses)
		{
			e.sprite = notClearedSprite;
		}
		for (int i = 0; i < progress; ++i)
		{
			missionProgresses[i].sprite = clearSprite;
		}
	}
}
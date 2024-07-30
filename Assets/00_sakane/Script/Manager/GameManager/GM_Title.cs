using Cysharp.Threading.Tasks;
using UnityEngine;

// タイトルの管理クラス
public class GM_Title : GameManagerBase, IGM_Title
{
	// タイトルキャンバス
	[SerializeField]
	GameObject titleCanvasPrefab;
	GameObject titleCanvas;

	// ステージ選択キャンバス
	[SerializeField]
	GameObject stageSelectCanvasPrefab;
	GameObject stageSelectCanvas;

	// オプションキャンバス
	[SerializeField]
	GameObject optionCanvasPrefab;
	GameObject optionCanvas;

	// 前回の状態
	GameInstance.GameState beforeGameState = GameInstance.GameState.Title;

	protected override void Start()
	{
		Time.timeScale = 1.0f;
		base.Start();

		// 生成
		titleCanvas = Instantiate(titleCanvasPrefab);
		stageSelectCanvas = Instantiate(stageSelectCanvasPrefab);
		optionCanvas = Instantiate(optionCanvasPrefab);
		titleCanvas.SetActive(false);
		stageSelectCanvas.SetActive(false);
		optionCanvas.SetActive(false);
		beforeGameState = GameInstance.gameState;

		// 現在のキャンバスを表示
		switch (GameInstance.gameState)
		{
		case GameInstance.GameState.Title:
			titleCanvas.SetActive(true);
			break;
		case GameInstance.GameState.StageSelect:
			stageSelectCanvas.SetActive(true);
			break;
		case GameInstance.GameState.Option:
			optionCanvas.SetActive(true);
			break;
		}
		FadeIO.Instance.FadeIn().Forget();
	}

	/// <summary>
	/// ステージ選択画面に移動
	/// </summary>
	async void ToStageSelect()
	{
		await UniTask.WaitUntil(() => ClickEffectManager.Instance.ClickEffects.Count <= 0);
		await FadeIO.Instance.FadeOut();
		stageSelectCanvas.SetActive(true);
		titleCanvas.SetActive(false);
		optionCanvas.SetActive(false);
		beforeGameState = GameInstance.gameState;
		GameInstance.gameState = GameInstance.GameState.StageSelect;
		await FadeIO.Instance.FadeIn();
	}

	/// <summary>
	/// タイトル画面に移動
	/// </summary>
	async void ToTitle()
	{
		await UniTask.WaitUntil(() => ClickEffectManager.Instance.ClickEffects.Count <= 0);
		await FadeIO.Instance.FadeOut();
		titleCanvas.SetActive(true);
		stageSelectCanvas.SetActive(false);
		optionCanvas.SetActive(false);
		beforeGameState = GameInstance.gameState;
		GameInstance.gameState = GameInstance.GameState.Title;
		await FadeIO.Instance.FadeIn();
	}

	/// <summary>
	/// オプション画面に移動
	/// </summary>
	async void ToOption()
	{
		await UniTask.WaitUntil(() => ClickEffectManager.Instance.ClickEffects.Count <= 0);
		optionCanvas.SetActive(true);
		titleCanvas.SetActive(false);
		stageSelectCanvas.SetActive(false);
		beforeGameState = GameInstance.gameState;
		GameInstance.gameState = GameInstance.GameState.Option;
	}

	/// <summary>
	/// 戻る
	/// </summary>
	public void Return()
	{
		switch (beforeGameState)
		{
		case GameInstance.GameState.Title:
			ToTitle();
			break;
		case GameInstance.GameState.StageSelect:
			ToStageSelect();
			break;
		case GameInstance.GameState.Option:
			ToOption();
			break;
		}
	}

	void IGM_Title.ToStageSelect()
	{
		ToStageSelect();
	}

	void IGM_Title.ToTitle()
	{
		ToTitle();
	}

	void IGM_Title.ToOption()
	{
		ToOption();
	}

	void IGM_Title.Return()
	{
		Return();
	}
}
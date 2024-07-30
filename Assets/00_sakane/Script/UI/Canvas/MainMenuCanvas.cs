using TMPro;
using UnityEngine;
using UnityEngine.UI;

// メインのメニューキャンバス
public class MainMenuCanvas : MonoBehaviour,IMainMenuCanvas
{
// 次のステージ進むボタン
	[SerializeField]
	Button nextStageButton;

	// ステージ番号
	[SerializeField]
	TMP_Text stageNumberText;

	private void OnEnable()
	{
		stageNumberText.text = StageManager.NowStageNumber.ToString();
	}

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生する音</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	/// <summary>
	/// ステージ選択
	/// </summary>
	public void StageSelect()
	{
		GameInstance.gameState = GameInstance.GameState.StageSelect;
		LevelManager.OpenLevel("TitleScene").Forget();
	}

	/// <summary>
	/// 次のステージ
	/// </summary>
	public  void NextStage()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().NextStage();
	}

	/// <summary>
	/// オプション
	/// </summary>
	public void Option()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().Option();
	}

	/// <summary>
	/// 戻る
	/// </summary>
	public void Return()
	{
		gameObject.SetActive(false);
		GM_Main.Instance.GetComponent<IGM_Main>().ToGame();
		GM_Main.Instance.GetComponent<IGM_Main>().GameReStart();
	}

	void IMainMenuCanvas.SetNextStageButtonInteractable(bool intaractable)
	{
		nextStageButton.interactable = intaractable;
	}
}
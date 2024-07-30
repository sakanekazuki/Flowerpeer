using UnityEngine;
using UnityEngine.UI;

// ゲームオーバー時に出てくる選択肢
public class GameOverSelectCanvas : MonoBehaviour,IGameOverSelectCanvas
{
	// 次のステージに移動するボタン
	[SerializeField]
	Button nextStageButton;

	/// <summary>
	/// ステージ選択に戻る
	/// </summary>
	public void ToStageSelect()
	{
		GameInstance.gameState = GameInstance.GameState.StageSelect;
		LevelManager.OpenLevel("TitleScene").Forget();
	}

	/// <summary>
	/// 次のステージに進む
	/// </summary>
	public void NextStage()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().NextStage();
	}

	/// <summary>
	/// リトライ
	/// </summary>
	public void ReTry()
	{
		LevelManager.OpenLevel("MainScene").Forget();
	}

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生する音</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	void IGameOverSelectCanvas.SetNextStageInteractable(bool isInteractable)
	{
		nextStageButton.interactable = isInteractable;
	}
}
using Cysharp.Threading.Tasks;
using UnityEngine;

// ステージ選択に移動するボタン
public class ToStageSelectButton : MonoBehaviour
{
	/// <summary>
	/// ステージ選択に移動
	/// </summary>
	public void ToStageSelect()
	{
		// シーン切り替えをしないように設定
		LevelManager.canLevelMove = false;
		// シーン読み込み
		LevelManager.OpenLevel("TitleScene").Forget();
		// ゲームの状態をステージ選択にする
		GameInstance.gameState = GameInstance.GameState.StageSelect;
		// シーンを切り替え
		LevelManager.canLevelMove = true;
	}
}
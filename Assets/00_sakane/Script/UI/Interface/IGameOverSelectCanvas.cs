// ゲームオーバー時に表示するキャンバスのインターフェース
public interface IGameOverSelectCanvas
{
	/// <summary>
	/// 次のステージに移動するボタンの状態変更
	/// </summary>
	/// <param name="isInteractable">ボタンの状態</param>
	void SetNextStageInteractable(bool isInteractable);
}
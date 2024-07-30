// 次のステージに移動するかステージ選択に戻るかを選択するキャンバスのインターフェース
public interface INextStageORStageSelectCanvas
{
	/// <summary>
	/// 次のステージに進むボタンの状態を変更
	/// </summary>
	/// <param name="isInteractable">状態</param>
	void NextStageInInteractable(bool isInteractable);
}
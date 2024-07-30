// メインメニューのインターフェース
public interface IMainMenuCanvas
{
	/// <summary>
	///	次のステージにすすむボタンの状態設定
	/// </summary>
	/// <param name="interactable">ボタンの状態</param>
	void SetNextStageButtonInteractable(bool interactable);
}
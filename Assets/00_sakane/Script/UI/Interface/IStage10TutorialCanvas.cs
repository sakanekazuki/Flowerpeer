// ステージ10のチュートリアルを表示するキャンバスのインターフェース
public interface IStage10TutorialCanvas
{
	/// <summary>
	/// 次に進むボタン有効
	/// </summary>
	void NextButtonEnable();

	/// <summary>
	/// 次のチュートリアルに進むボタン無効
	/// </summary>
	void NextButtonDisable();

	/// <summary>
	/// 前に戻るボタン有効
	/// </summary>
	void PreviousButtonEnable();

	/// <summary>
	/// 前のチュートリアルに戻るボタン無効
	/// </summary>
	void PreviousButtonDisable();
}

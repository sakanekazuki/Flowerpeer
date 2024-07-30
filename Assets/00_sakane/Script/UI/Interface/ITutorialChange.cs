// チュートリアルを変更するインターフェース
public interface ITutorialChange
{
	/// <summary>
	/// チュートリアルの番号リセット
	/// </summary>
	void TutorialNumberReset();

	/// <summary>
	/// 次のチュートリアルに移動
	/// </summary>
	void Next();
	/// <summary>
	/// 前のチュートリアルに移動
	/// </summary>
	void Previous();
}

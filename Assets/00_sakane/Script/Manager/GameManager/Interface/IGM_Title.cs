// タイトルのインターフェース
public interface IGM_Title
{
	/// <summary>
	/// ステージ選択画面に移動
	/// </summary>
	void ToStageSelect();

	/// <summary>
	/// タイトル画面に移動
	/// </summary>
	void ToTitle();

	/// <summary>
	/// オプション画面に移動
	/// </summary>
	void ToOption();

	/// <summary>
	/// 戻る
	/// </summary>
	void Return();
}
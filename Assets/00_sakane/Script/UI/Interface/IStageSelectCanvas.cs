// ステージ選択キャンバスのインターフェース
public interface IStageSelectCanvas
{
	/// <summary>
	/// テキストに線を引いた量を入れる
	/// </summary>
	/// <param name="magnitude">線を引いた量</param>
	void SetLineMagnitude(float magnitude, bool isClear = true);

	/// <summary>
	/// プレビュー画像設定
	/// </summary>
	/// <param name="number">番号</param>
	void SetPreviewImage(int number);

	/// <summary>
	/// ミッション進行具合設定
	/// </summary>
	/// <param name="progress">進行具合</param>
	void MissionProgression(int progress);
}
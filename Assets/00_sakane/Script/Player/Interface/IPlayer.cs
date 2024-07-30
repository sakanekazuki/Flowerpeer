// プレイヤーのインターフェース
public interface IPlayer
{
	/// <summary>
	/// 操作不可能
	/// </summary>
	void NonControl();

	/// <summary>
	/// 操作可能
	/// </summary>
	void Control();

	/// <summary>
	/// 色変更可能状態設定
	/// </summary>
	/// <param name="canColorChange">true = 色変更可能</param>
	void SetCanColorChange(bool canColorChange);

	/// <summary>
	/// デフォルトのカーソルに戻す
	/// </summary>
	void DefaultCursor();

	/// <summary>
	/// 魔法のカーソルに戻す
	/// </summary>
	void MagicCursor();

	/// <summary>
	/// 線生成強制終了
	/// </summary>
	void LineCreateForcedTermination();
}
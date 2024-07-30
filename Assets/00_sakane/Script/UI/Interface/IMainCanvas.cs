// メインゲームキャンバスのインターフェース
public interface IMainCanvas
{
	/// <summary>
	/// ゲーム開始
	/// </summary>
	void SetStartMessageActive(bool isActive);

	/// <summary>
	/// メッセージ設定
	/// </summary>
	/// <param name="message">メッセージ</param>
	void SetMessageText(string message);

	/// <summary>
	/// スコア設定
	/// </summary>
	/// <param name="score">スコア</param>
	void SetScoreText(string score);

	/// <summary>
	/// キャラクターが取得した鍵
	/// </summary>
	/// <param name="number">取得した番号</param>
	/// <param name="keyType">鍵の種類</param>
	void CharacterGetKey(int number ,GameInstance.KeyType keyType);

	/// <summary>
	/// キャラクターが取得している鍵リセット
	/// </summary>
	void KeyReset();

	/// <summary>
	/// ボタンの状態を設定
	/// </summary>
	/// <param name="interactable">ボタンの状態</param>
	void SetButtonInteractable(bool interactable);
}
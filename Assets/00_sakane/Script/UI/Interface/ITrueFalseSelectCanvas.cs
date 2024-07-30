using Cysharp.Threading.Tasks;

// 選択肢を表示するキャンバスのインターフェース
public interface ITrueFalseSelectCanvas
{
	/// <summary>
	/// 選択するまで待つ
	/// </summary>
	/// <returns>選択の結果</returns>
	UniTask<bool> SelectWait();

	/// <summary>
	/// メッセージ設定
	/// </summary>
	/// <param name="message">設定するメッセージ</param>
	void SetMessage(string message);
}
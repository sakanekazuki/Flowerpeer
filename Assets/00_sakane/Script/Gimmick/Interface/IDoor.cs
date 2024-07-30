// ドアのインターフェース
public interface IDoor
{
	/// <summary>
	/// 開く
	/// </summary>
	/// <param name="keyValue">鍵の番号</param>
	/// <returns>true = 開く</returns>
	bool Open(int keyValue);
}
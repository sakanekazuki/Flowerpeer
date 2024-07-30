// アイテムのインターフェース
public interface IItemBase<T>
{
	/// <summary>
	/// 拾う
	/// </summary>
	void PickUp();

	/// <summary>
	/// 使用する
	/// </summary>
	/// <returns>使用した際の情報</returns>
	T Used();
}
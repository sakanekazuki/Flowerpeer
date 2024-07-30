using UnityEngine;

// アイテムのベースクラス
public abstract class ItemBase<T> : MonoBehaviour, IItemBase<T>
{
	// true = 一度使用するとなくなる
	[SerializeField]
	protected bool isOnce = true;

	/// <summary>
	/// アイテム取得
	/// </summary>
	virtual protected void PickUp()
	{

	}

	/// <summary>
	/// 使用する
	/// </summary>
	virtual protected T Used()
	{
		return default(T);
	}

	void IItemBase<T>.PickUp()
	{
		PickUp();
	}

	T IItemBase<T>.Used()
	{
		return Used();
	}
}
using UnityEngine;

// �A�C�e���̃x�[�X�N���X
public abstract class ItemBase<T> : MonoBehaviour, IItemBase<T>
{
	// true = ��x�g�p����ƂȂ��Ȃ�
	[SerializeField]
	protected bool isOnce = true;

	/// <summary>
	/// �A�C�e���擾
	/// </summary>
	virtual protected void PickUp()
	{

	}

	/// <summary>
	/// �g�p����
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
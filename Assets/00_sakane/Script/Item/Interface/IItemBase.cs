// �A�C�e���̃C���^�[�t�F�[�X
public interface IItemBase<T>
{
	/// <summary>
	/// �E��
	/// </summary>
	void PickUp();

	/// <summary>
	/// �g�p����
	/// </summary>
	/// <returns>�g�p�����ۂ̏��</returns>
	T Used();
}
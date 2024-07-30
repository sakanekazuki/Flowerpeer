// ���C���Q�[���L�����o�X�̃C���^�[�t�F�[�X
public interface IMainCanvas
{
	/// <summary>
	/// �Q�[���J�n
	/// </summary>
	void SetStartMessageActive(bool isActive);

	/// <summary>
	/// ���b�Z�[�W�ݒ�
	/// </summary>
	/// <param name="message">���b�Z�[�W</param>
	void SetMessageText(string message);

	/// <summary>
	/// �X�R�A�ݒ�
	/// </summary>
	/// <param name="score">�X�R�A</param>
	void SetScoreText(string score);

	/// <summary>
	/// �L�����N�^�[���擾������
	/// </summary>
	/// <param name="number">�擾�����ԍ�</param>
	/// <param name="keyType">���̎��</param>
	void CharacterGetKey(int number ,GameInstance.KeyType keyType);

	/// <summary>
	/// �L�����N�^�[���擾���Ă��錮���Z�b�g
	/// </summary>
	void KeyReset();

	/// <summary>
	/// �{�^���̏�Ԃ�ݒ�
	/// </summary>
	/// <param name="interactable">�{�^���̏��</param>
	void SetButtonInteractable(bool interactable);
}
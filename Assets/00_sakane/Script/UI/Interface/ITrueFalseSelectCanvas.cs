using Cysharp.Threading.Tasks;

// �I������\������L�����o�X�̃C���^�[�t�F�[�X
public interface ITrueFalseSelectCanvas
{
	/// <summary>
	/// �I������܂ő҂�
	/// </summary>
	/// <returns>�I���̌���</returns>
	UniTask<bool> SelectWait();

	/// <summary>
	/// ���b�Z�[�W�ݒ�
	/// </summary>
	/// <param name="message">�ݒ肷�郁�b�Z�[�W</param>
	void SetMessage(string message);
}
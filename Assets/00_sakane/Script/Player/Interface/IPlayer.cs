// �v���C���[�̃C���^�[�t�F�[�X
public interface IPlayer
{
	/// <summary>
	/// ����s�\
	/// </summary>
	void NonControl();

	/// <summary>
	/// ����\
	/// </summary>
	void Control();

	/// <summary>
	/// �F�ύX�\��Ԑݒ�
	/// </summary>
	/// <param name="canColorChange">true = �F�ύX�\</param>
	void SetCanColorChange(bool canColorChange);

	/// <summary>
	/// �f�t�H���g�̃J�[�\���ɖ߂�
	/// </summary>
	void DefaultCursor();

	/// <summary>
	/// ���@�̃J�[�\���ɖ߂�
	/// </summary>
	void MagicCursor();

	/// <summary>
	/// �����������I��
	/// </summary>
	void LineCreateForcedTermination();
}
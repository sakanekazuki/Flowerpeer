// �X�e�[�W�I���L�����o�X�̃C���^�[�t�F�[�X
public interface IStageSelectCanvas
{
	/// <summary>
	/// �e�L�X�g�ɐ����������ʂ�����
	/// </summary>
	/// <param name="magnitude">������������</param>
	void SetLineMagnitude(float magnitude, bool isClear = true);

	/// <summary>
	/// �v���r���[�摜�ݒ�
	/// </summary>
	/// <param name="number">�ԍ�</param>
	void SetPreviewImage(int number);

	/// <summary>
	/// �~�b�V�����i�s��ݒ�
	/// </summary>
	/// <param name="progress">�i�s�</param>
	void MissionProgression(int progress);
}
// �L�����N�^�[�̃C���^�[�t�F�[�X
using UnityEngine;

public interface ICharacter
{
	/// <summary>
	/// �F�ύX
	/// </summary>
	/// <param name="status">�ύX����F�̃X�e�[�^�X</param>
	void ColorChange(CharacterStatus status);

	/// <summary>
	/// �����E��
	/// </summary>
	/// <param name="keyId">�E��������ID</param>
	int PickUpTheKey(int keyId);

	/// <summary>
	/// �L�����N�^�[�ړ��J�n
	/// </summary>
	void Start();

	/// <summary>
	/// �L�����N�^�[��~
	/// </summary>
	void Stop();

	/// <summary>
	/// �L�����N�^�[�̑傫���擾
	/// </summary>
	/// <returns>�L�����N�^�[�̑傫��</returns>
	Vector2 GetSize();
}
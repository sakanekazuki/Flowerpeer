using System.Collections.Generic;
using UnityEngine;

// �Q�[���}�l�[�W���[�̃C���^�[�t�F�[�X
public interface IGM_Main
{
	/// <summary>
	/// ������
	/// </summary>
	void Init();

	/// <summary>
	/// �I��
	/// </summary>
	void Final();

	/// <summary>
	/// �S�[��
	/// </summary>
	/// <param name="goalCharacter">�S�[�������L�����N�^�[�I�u�W�F�N�g</param>
	void CharacterGoal(GameObject goalCharacter);

	/// <summary>
	/// �Q�[���I�[�o�[
	/// </summary>
	void GameOver();

	/// <summary>
	/// ���̃X�e�[�W�Ɉړ�
	/// </summary>
	void NextStage();

	/// <summary>
	/// �I�v�V�������
	/// </summary>
	void Option();

	/// <summary>
	/// ���C���Q�[���ɖ߂�
	/// </summary>
	void ToGame();

	/// <summary>
	/// ���C�����j���[
	/// </summary>
	void MainMenu();

	/// <summary>
	/// �Q�[���N���A
	/// </summary>
	void GameClear();

	/// <summary>
	/// �C���X�^���X�������L�����N�^�[�擾
	/// </summary>
	/// <returns>�C���X�^���X�������L�����N�^�[</returns>
	List<GameObject> GetInstanceCharacterObjects();

	/// <summary>
	/// ���̒����ݒ�
	/// </summary>
	/// <param name="lineMagnitude">�ύX��̐��̒���</param>
	void SetLineMagnitude(float lineMagnitude);

	/// <summary>
	/// �Q�[����~
	/// </summary>
	void GameStop();

	/// <summary>
	/// �Q�[�����X�^�[�g
	/// </summary>
	void GameReStart();

	/// <summary>
	/// �Q�[��������
	/// </summary>
	void FastForward();

	/// <summary>
	/// �������Ԏ擾
	/// </summary>
	/// <returns>��������</returns>
	bool GetIsFastForward();

	/// <summary>
	/// �L�����N�^�[�������擾
	/// </summary>
	/// <param name="number">��</param>
	/// <param name="keyType">���</param>
	void CharacterGetKey(int number, GameInstance.KeyType keyType);
}
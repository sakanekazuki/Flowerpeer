using System.Collections.Generic;

// �Z�[�u�f�[�^
[System.Serializable]
public class SaveData
{
	// ����(�}�X�^�[,BGM,SE)
	public float MasterVolume = -20;
	public float BGMVolume = -20;
	public float SEVolume = -20;

	// �`���[�g���A���̕\��
	public bool isStage1TutorialFirst = false;
	public bool isStage10TutorialFirst = false;

	// �X�e�[�W�̏��
	public List<bool> isClear = new List<bool>();
	public List<bool> isOpening = new List<bool>();
	public List<float> minLineMagnitude = new List<float>();

	// �~�b�V�����̏��
	public List<GameInstance.MissionState> missionStates = new List<GameInstance.MissionState>();

	public int stillOpeingValue = 0;
}
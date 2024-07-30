using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W�Ǘ��N���X
public class StageManager : ManagerBase<StageManager>
{
	// ���݂̃X�e�[�W�ԍ�
	static int nowStageNumber = 1;
	public static int NowStageNumber
	{
		get => nowStageNumber;
	}

	// �X�e�[�W
	Object stageObject = null;

	// true = �X�e�[�W�ǂݍ��ݒ�
	bool isLoadingStage = false;
	public bool IsLoadingStage
	{
		get => isLoadingStage;
	}

	// BGM
	[SerializeField]
	AudioClip bgmClip1;
	[SerializeField]
	AudioClip bgmClip2;
	[SerializeField]
	AudioClip bgmClip3;
	[SerializeField]
	AudioClip bgmClip4;

	// BGM��炷
	AudioSource bgmSource;

	// �~�b�V�����̃N���A����
	[SerializeField]
	SO_MissionConditions missionConditions;
	public SO_MissionConditions MissionConditions
	{
		get => missionConditions;
	}

	// �X�e�[�W��
	[SerializeField]
	List<string> stageNames = new List<string>();
	public List<string> StageNames
	{
		get => stageNames;
	}

	private void Start()
	{
		bgmSource = Camera.main.GetComponent<AudioSource>();
	}

	/// <summary>
	/// �X�e�[�W�N���A
	/// </summary>
	public void StageClear()
	{
		bgmSource.Stop();

		if (nowStageNumber < 30)
		{
			SaveManager.data.isOpening[nowStageNumber] = true;
		}
		//// �Q�[���N���A
		//if(nowStageNumber == stageValue)
		//{
		//	GM_Main.Instance.GetComponent<IGM_Main>().GameClear();
		//}
		SaveManager.data.isClear[nowStageNumber - 1] = true;
		LineManager.Instance.MagnitudeSave();
		for (int i = 3; i > 1; --i)
		{
			if ((int)SaveManager.data.missionStates[nowStageNumber - 1] > (i - 1))
			{
				return;
			}
			if (Mathf.Floor(SaveManager.data.minLineMagnitude[nowStageNumber - 1]) <= missionConditions.missionConditions[nowStageNumber - 1].ClearConditions[i - 1])
			{
				SaveManager.data.missionStates[nowStageNumber - 1] = (GameInstance.MissionState)System.Enum.ToObject(typeof(GameInstance.MissionState), i);
				return;
			}
		}
		SaveManager.data.missionStates[nowStageNumber - 1] = GameInstance.MissionState.NotCleared;
	}

	/// <summary>
	/// �X�e�[�W�̃~�b�V�����N���A�����擾
	/// </summary>
	/// <param name="stageNumber">�擾����X�e�[�W�̔ԍ�</param>
	/// <returns>�N���A����</returns>
	public List<float> GetMissionClearCondition(int stageNumber)
	{
		return missionConditions.missionConditions[stageNumber].ClearConditions;
	}

	/// <summary>
	/// ���̃X�e�[�W�Ɉړ�
	/// </summary>
	public async UniTask<bool> NextStage()
	{
		// �ǂݍ��ݏ�Ԃɂ���
		isLoadingStage = true;
		var result = await StageLoadAsync(++nowStageNumber);
		return result;
	}

	/// <summary>
	/// �񓯊��ŃX�e�[�W����
	/// </summary>
	/// <param name="stageNumber">�X�e�[�W�ԍ�</param>
	/// <returns></returns>
	public async UniTask<bool> StageLoadAsync(int stageNumber)
	{
		GM_Main.isPlaying = false;
		if (stageObject != null)
		{
			// �쐬���Ă����X�e�[�W���폜
			Destroy(stageObject);
		}
		// �ǂݍ��ݏ�Ԃɂ���
		isLoadingStage = true;
		// �X�e�[�W�ǂݍ���
		var loadObject = await Resources.LoadAsync("Stage/stage" + stageNumber.ToString());

		SaveManager.data.isOpening[stageNumber - 1] = true;

		if (loadObject == null)
		{
			return false;
		}
		// �X�e�[�W����
		stageObject = Instantiate(loadObject);
		// ���݂̃X�e�[�W�ԍ�
		nowStageNumber = stageNumber;
		// �X�e�[�W�ǂݍ��ݏI����Ԃɂ���
		isLoadingStage = false;

		// BGM�𗬂������Ȃ��ꍇ�͎擾
		await UniTask.WaitUntil(() =>
		{
			if (bgmSource == null)
			{
				bgmSource = Camera.main.GetComponent<AudioSource>();
			}
			return bgmSource != null;
		});
		// BGM�Đ�
		if (stageNumber <= 10)
		{
			bgmSource.clip = bgmClip1;
		}
		else if (stageNumber <= 20)
		{
			bgmSource.clip = bgmClip2;
		}
		else if (stageNumber <= 25)
		{
			bgmSource.clip = bgmClip3;
		}
		else
		{
			bgmSource.clip = bgmClip4;
		}
		bgmSource.Play();

		return true;
	}
}
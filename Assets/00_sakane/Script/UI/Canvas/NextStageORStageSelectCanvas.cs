using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ���̃X�e�[�W�Ɉړ����邩�X�e�[�W�I���ɖ߂邩��I������L�����o�X
public class NextStageORStageSelectCanvas : MonoBehaviour, INextStageORStageSelectCanvas
{
	// ���U���g��\������e�L�X�g
	[SerializeField]
	TextMeshProUGUI resultText;

	// ���̃X�e�[�W�Ɉړ�����{�^��
	[SerializeField]
	Button nextStageButton;

	// �~�b�V�����̏�Ԃ�\������摜
	[SerializeField]
	List<Image> missionConditionImage = new List<Image>();
	[SerializeField]
	List<Sprite> missionConditionSprite = new List<Sprite>();

	// �~�b�V�����̃N���A������\������e�L�X�g
	[SerializeField]
	List<TMP_Text> missionClearConditionText = new List<TMP_Text>();

	private void OnEnable()
	{
		// �����_�ȉ��؂�̂Ă����l��\��
		resultText.text = Mathf.Floor(LineManager.Instance.AllLineMagnitude).ToString() + "cm";
		foreach (var e in missionConditionImage)
		{
			// �X�v���C�g������
			e.sprite = missionConditionSprite[0];
		}
		foreach (var e in StageManager.Instance.GetMissionClearCondition(StageManager.NowStageNumber - 1))
		{
			// �z��̗v�f�ԍ�
			var arrayNumber = StageManager.Instance.GetMissionClearCondition(StageManager.NowStageNumber - 1).IndexOf(e);
			// �N���A��������
			missionClearConditionText[arrayNumber/* - arrayNumber*/].text = e.ToString() + "cm";
		}
		GameInstance.MissionState missionState = GameInstance.MissionState.NotCleared;
		for (int i = 3; i > 0; --i)
		{
			if (Mathf.Floor(LineManager.Instance.AllLineMagnitude) <= StageManager.Instance.MissionConditions.missionConditions[StageManager.NowStageNumber - 1].ClearConditions[i - 1])
			{
				// ���̃X�R�A�Ń~�b�V�������ǂꂭ�炢�N���A���ꂽ�̂��擾	
				missionState = (GameInstance.MissionState)System.Enum.ToObject(typeof(GameInstance.MissionState), i);
				break;
			}
		}
		for (int i = 0; i < (int)missionState; ++i)
		{
			// �N���A�����X�v���C�g�ɕύX
			missionConditionImage[i].sprite = missionConditionSprite[1];
		}
		//for (int i = 0; i < (int)SaveManager.data.missionStates[StageManager.NowStageNumber - 1]; ++i)
		//{
		//	// �N���A�����X�v���C�g�ɕύX
		//	missionConditionImage[i].sprite = missionConditionSprite[1];
		//}
	}

	/// <summary>
	/// SE��炷
	/// </summary>
	/// <param name="clip">��</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	void INextStageORStageSelectCanvas.NextStageInInteractable(bool isInteractable)
	{
		nextStageButton.interactable = isInteractable;
	}
}
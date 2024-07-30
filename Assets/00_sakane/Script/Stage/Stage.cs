using UnityEngine;
using UnityEngine.UI;

// �X�e�[�W�N���X
public class Stage : MonoBehaviour
{
	// �X�e�[�W�ԍ�
	public int number = 0;

	// true = �N���A�������Ƃ̂���X�e�[�W
	bool isClear = false;

	// �N���A�������Ƃ�����X�e�[�W�̉��ɕ\�������摜
	[SerializeField]
	GameObject clearImg;

	// �~�b�V�����i�s�
	GameInstance.MissionState missionState;

	private void Awake()
	{
		// �N���A���������ׂ�
		isClear = SaveManager.data.isClear[number - 1];
		if (number == 1 || SaveManager.data.isOpening[number - 1])
		{
			GetComponentInChildren<Button>().interactable = true;
			SaveManager.data.isOpening[number - 1] = true;
		}
		else
		{
			GetComponentInChildren<Button>().interactable = false;
		}
		missionState = SaveManager.data.missionStates[number - 1];
	}

	private void Start()
	{
		// �N���A���Ă���ꍇ�͕\��
		if (isClear)
		{
			clearImg.SetActive(true);
		}
		else
		{
			clearImg.SetActive(false);
		}
	}

	/// <summary>
	/// �J�[�\���������Ă���ۂɌĂяo���֐�
	/// </summary>
	public void Hovered()
	{
		transform.root.gameObject.GetComponent<IStageSelectCanvas>().SetLineMagnitude(SaveManager.data.minLineMagnitude[number - 1], isClear);
		transform.root.gameObject.GetComponent<IStageSelectCanvas>().SetPreviewImage(number);
		transform.root.gameObject.GetComponent<IStageSelectCanvas>().MissionProgression((int)missionState);
	}
}
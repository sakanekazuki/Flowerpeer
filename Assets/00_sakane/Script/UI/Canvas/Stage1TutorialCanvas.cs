using UnityEngine;
using UnityEngine.UI;

// �X�e�[�W1�̃`���[�g���A��
public class Stage1TutorialCanvas : MonoBehaviour,IStage1TutorialCanvas
{
	// �`���[�g���A����\������C���[�W
	[SerializeField]
	GameObject tutorialImage;

	// �`���[�g���A�������ɐi�߂�{�^��
	[SerializeField]
	Button nextButton;

	// �`���[�g���A����O�ɖ߂��{�^��
	[SerializeField]
	Button previousButton;

	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
		//GetComponentInChildren<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
	}

	/// <summary>
	/// �X�e�[�W1�̃`���[�g���A����\��
	/// </summary>
	public void TutorialDisable()
	{
		TutorialManager.Instance.Stage1TutorialDisable();
		NextButtonEnable();
		PreviousButtonEnable();
	}

	/// <summary>
	/// ���̃`���[�g���A���Ɉړ�
	/// </summary>
	public void NextTutorial()
	{
		tutorialImage.GetComponent<ITutorialChange>().Next();
	}

	/// <summary>
	/// �O�̃`���[�g���A���Ɉړ�
	/// </summary>
	public void PreviousTutorial()
	{
		tutorialImage.GetComponent<ITutorialChange>().Previous();
	}

	/// <summary>
	/// SE�Đ�
	/// </summary>
	/// <param name="clip">�Đ����鉹</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	void NextButtonEnable()
	{
		nextButton.interactable = true;
	}

	void PreviousButtonEnable()
	{
		previousButton.interactable = true;
	}

	void IStage1TutorialCanvas.NextButtonEnable()
	{
		NextButtonEnable();
	}

	void IStage1TutorialCanvas.NextButtonDisable()
	{
		nextButton.interactable = false;
	}

	void IStage1TutorialCanvas.PreviousButtonEnable()
	{
		PreviousButtonEnable();
	}

	void IStage1TutorialCanvas.PreviousButtonDisable()
	{
		previousButton.interactable = false;
	}
}
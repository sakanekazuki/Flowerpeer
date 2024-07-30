using UnityEngine;
using UnityEngine.UI;

// �X�e�[�W10�̃`���[�g���A��
public class Stage10TutorialCanvas : MonoBehaviour, IStage10TutorialCanvas
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
	/// �X�e�[�W10�̃`���[�g���A����\��
	/// </summary>
	public void TutorialDisable()
	{
		TutorialManager.Instance.Stage10TutorialDisable();
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

	void IStage10TutorialCanvas.NextButtonEnable()
	{
		NextButtonEnable();
	}

	void IStage10TutorialCanvas.NextButtonDisable()
	{
		nextButton.interactable = false;
	}

	void IStage10TutorialCanvas.PreviousButtonEnable()
	{
		PreviousButtonEnable();
	}

	void IStage10TutorialCanvas.PreviousButtonDisable()
	{
		previousButton.interactable = false;
	}
}
using UnityEngine;

// �X�e�[�W1�ŕ\������`���[�g���A����\������摜
public class Stage1TutorialImage : MonoBehaviour,ITutorialChange
{
	// �A�j���[�^�[
	Animator animator;

	// �`���[�g���A����
	[SerializeField]
	int tutorialValue = 3;

	// ���݂̃`���[�g���A���̔ԍ�
	int nowTutorialNumber = 1;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		nowTutorialNumber = 1;
		transform.root.GetComponent<IStage1TutorialCanvas>().NextButtonEnable();
		transform.root.GetComponent<IStage1TutorialCanvas>().PreviousButtonDisable();
	}

	void ITutorialChange.TutorialNumberReset()
	{
		nowTutorialNumber = 1;
	}

	void ITutorialChange.Next()
	{
		++nowTutorialNumber;
		if (nowTutorialNumber == tutorialValue)
		{
			transform.root.GetComponent<IStage1TutorialCanvas>().NextButtonDisable();
		}
		transform.root.GetComponent<IStage1TutorialCanvas>().PreviousButtonEnable();
		animator.SetTrigger("Tutorial" + nowTutorialNumber.ToString());
	}

	void ITutorialChange.Previous()
	{
		--nowTutorialNumber;
		if (nowTutorialNumber == 1)
		{
			transform.root.GetComponent<IStage1TutorialCanvas>().PreviousButtonDisable();
		}
		transform.root.GetComponent<IStage1TutorialCanvas>().NextButtonEnable();
		animator.SetTrigger("Tutorial" + nowTutorialNumber.ToString());
	}
}

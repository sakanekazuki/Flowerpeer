using UnityEngine;

// �X�e�[�W1�ŕ\������`���[�g���A����\������摜
public class Stage10TutorialImage : MonoBehaviour,ITutorialChange
{
	// �A�j���[�^�[
	Animator animator;

	// �`���[�g���A����
	[SerializeField]
	int tutorialValue = 2;

	// ���݂̃`���[�g���A���̔ԍ�
	int nowTutorialNumber = 1;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		nowTutorialNumber = 1;
		transform.root.GetComponent<IStage10TutorialCanvas>().NextButtonEnable();
		transform.root.GetComponent<IStage10TutorialCanvas>().PreviousButtonDisable();
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
			transform.root.GetComponent<IStage10TutorialCanvas>().NextButtonDisable();
		}
		transform.root.GetComponent<IStage10TutorialCanvas>().PreviousButtonEnable();
		animator.SetTrigger("Tutorial" + nowTutorialNumber.ToString());
	}

	void ITutorialChange.Previous()
	{
		--nowTutorialNumber;
		if (nowTutorialNumber == 1)
		{
			transform.root.GetComponent<IStage10TutorialCanvas>().PreviousButtonDisable();
		}
		transform.root.GetComponent<IStage10TutorialCanvas>().NextButtonEnable();
		animator.SetTrigger("Tutorial" + nowTutorialNumber.ToString());
	}
}

using UnityEngine;

// ステージ1で表示するチュートリアルを表示する画像
public class Stage1TutorialImage : MonoBehaviour,ITutorialChange
{
	// アニメーター
	Animator animator;

	// チュートリアル数
	[SerializeField]
	int tutorialValue = 3;

	// 現在のチュートリアルの番号
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

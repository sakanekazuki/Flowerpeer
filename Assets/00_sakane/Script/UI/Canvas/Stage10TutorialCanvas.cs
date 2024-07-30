using UnityEngine;
using UnityEngine.UI;

// ステージ10のチュートリアル
public class Stage10TutorialCanvas : MonoBehaviour, IStage10TutorialCanvas
{
	// チュートリアルを表示するイメージ
	[SerializeField]
	GameObject tutorialImage;

	// チュートリアルを次に進めるボタン
	[SerializeField]
	Button nextButton;

	// チュートリアルを前に戻すボタン
	[SerializeField]
	Button previousButton;

	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
		//GetComponentInChildren<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
	}

	/// <summary>
	/// ステージ10のチュートリアル非表示
	/// </summary>
	public void TutorialDisable()
	{
		TutorialManager.Instance.Stage10TutorialDisable();
		NextButtonEnable();
		PreviousButtonEnable();
	}

	/// <summary>
	/// 次のチュートリアルに移動
	/// </summary>
	public void NextTutorial()
	{
		tutorialImage.GetComponent<ITutorialChange>().Next();
	}

	/// <summary>
	/// 前のチュートリアルに移動
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
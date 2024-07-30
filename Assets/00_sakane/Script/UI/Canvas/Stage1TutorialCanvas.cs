using UnityEngine;
using UnityEngine.UI;

// ステージ1のチュートリアル
public class Stage1TutorialCanvas : MonoBehaviour,IStage1TutorialCanvas
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
	/// ステージ1のチュートリアル非表示
	/// </summary>
	public void TutorialDisable()
	{
		TutorialManager.Instance.Stage1TutorialDisable();
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

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生する音</param>
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
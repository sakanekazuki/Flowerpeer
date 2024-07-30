using UnityEngine;

// チュートリアル管理クラス
public class TutorialManager : ManagerBase<TutorialManager>
{
	// true = チュートリアル表示中
	bool isEnable = false;
	public bool IsEnable
	{
		get => isEnable;
	}

	// ステージ1のチュートリアル画面
	[SerializeField]
	GameObject stage1TutorialObjectPrefab;
	GameObject stage1TutorialObject;

	// ステージ10のチュートリアル画面
	[SerializeField]
	GameObject stage10TutorialObjectPrefab;
	GameObject stage10TutorialObject;

	private void Awake()
	{
		stage1TutorialObject = Instantiate(stage1TutorialObjectPrefab);
		stage10TutorialObject = Instantiate(stage10TutorialObjectPrefab);

		stage1TutorialObject.SetActive(false);
		stage10TutorialObject.SetActive(false);
		isEnable = false;
	}

	/// <summary>
	/// ステージ1のチュートリアル非表示
	/// </summary>
	public void Stage1TutorialDisable()
	{
		stage1TutorialObject.SetActive(false);
		isEnable = false;
	}

	/// <summary>
	/// ステージ1のチュートリアル表示
	/// </summary>
	public void Stage1TutorialEnable()
	{
		stage1TutorialObject.SetActive(true);
		SaveManager.data.isStage1TutorialFirst = true;
		isEnable = true;
	}

	/// <summary>
	/// ステージ10のチュートリアル非表示
	/// </summary>
	public void Stage10TutorialDisable()
	{
		stage10TutorialObject.SetActive(false);
		isEnable = false;
	}

	/// <summary>
	/// ステージ10のチュートリアル表示
	/// </summary>
	public void Stage10TutorialEnable()
	{
		stage10TutorialObject.SetActive(true);
		SaveManager.data.isStage10TutorialFirst = true;
		isEnable = true;
	}
}
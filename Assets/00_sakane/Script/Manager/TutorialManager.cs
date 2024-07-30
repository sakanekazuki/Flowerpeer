using UnityEngine;

// �`���[�g���A���Ǘ��N���X
public class TutorialManager : ManagerBase<TutorialManager>
{
	// true = �`���[�g���A���\����
	bool isEnable = false;
	public bool IsEnable
	{
		get => isEnable;
	}

	// �X�e�[�W1�̃`���[�g���A�����
	[SerializeField]
	GameObject stage1TutorialObjectPrefab;
	GameObject stage1TutorialObject;

	// �X�e�[�W10�̃`���[�g���A�����
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
	/// �X�e�[�W1�̃`���[�g���A����\��
	/// </summary>
	public void Stage1TutorialDisable()
	{
		stage1TutorialObject.SetActive(false);
		isEnable = false;
	}

	/// <summary>
	/// �X�e�[�W1�̃`���[�g���A���\��
	/// </summary>
	public void Stage1TutorialEnable()
	{
		stage1TutorialObject.SetActive(true);
		SaveManager.data.isStage1TutorialFirst = true;
		isEnable = true;
	}

	/// <summary>
	/// �X�e�[�W10�̃`���[�g���A����\��
	/// </summary>
	public void Stage10TutorialDisable()
	{
		stage10TutorialObject.SetActive(false);
		isEnable = false;
	}

	/// <summary>
	/// �X�e�[�W10�̃`���[�g���A���\��
	/// </summary>
	public void Stage10TutorialEnable()
	{
		stage10TutorialObject.SetActive(true);
		SaveManager.data.isStage10TutorialFirst = true;
		isEnable = true;
	}
}
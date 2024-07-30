using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

// �X�e�[�W�I���L�����o�X
public class StageSelectCanvas : MonoBehaviour, IStageSelectCanvas
{
	// �{�^����I���o���Ȃ���Ԃɂ���I�u�W�F�N�g
	[SerializeField]
	GameObject buttonIgnoreObject;

	// ��������e�L�X�g
	[SerializeField]
	Text stageNameText;

	// �X�R�A�\���e�L�X�g
	[SerializeField]
	TextMeshProUGUI scoreText;

	// �v���r���[
	[SerializeField]
	Image previewImage;

	// �v���r���[
	[SerializeField]
	List<Sprite> previewSprites = new List<Sprite>();

	// �ŏ��̃X�v���C�g
	Sprite defaultPreviewSprite;

	// �~�b�V�����i�s���\������I�u�W�F�N�g
	[SerializeField]
	List<Image> missionProgresses = new List<Image>();

	// �~�b�V�����N���A���Ă��Ȃ��ۂ̃X�v���C�g
	[SerializeField]
	Sprite notClearedSprite;

	// �~�b�V�����N���A���̃X�v���C�g
	[SerializeField]
	Sprite clearSprite;

	private void Awake()
	{
		buttonIgnoreObject.SetActive(false);
		defaultPreviewSprite = previewImage.sprite;
	}

	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	private void OnDisable()
	{
		stageNameText.text = "";
		previewImage.sprite = defaultPreviewSprite;
		scoreText.text = "";
	}

	/// <summary>
	/// �Q�[���Ɉړ�
	/// </summary>
	/// <param name="stageNumber">�X�e�[�W�̔ԍ�</param>
	public async void ToGame(int stageNumber)
	{
		buttonIgnoreObject.SetActive(true);
		GM_Main.stageNumber = stageNumber;
		GameInstance.gameState = GameInstance.GameState.MainGame;
		// �V�[����ǂݍ���
		LevelManager.OpenLevel("MainScene").Forget();
		// �V�[����؂�ւ��Ȃ�
		LevelManager.canLevelMove = false;
		// �t�F�[�h�A�E�g
		FadeIO.Instance.FadeOut().Forget();
		// �t�F�[�h�ƃV�[���ǂݍ��ݏI���҂�
		await UniTask.WaitUntil(() => { return !LevelManager.IsLevelLoading && !FadeIO.Instance.IsFading; });
		// �V�[���ړ����\�ɂ���
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// �^�C�g���Ɉړ�
	/// </summary>
	public void ToTitle()
	{
		GM_Title.Instance.GetComponent<IGM_Title>().ToTitle();
	}

	/// <summary>
	/// �I�v�V�����Ɉړ�
	/// </summary>
	public void Option()
	{
		GM_Title.Instance.GetComponent<IGM_Title>().ToOption();
	}

	/// <summary>
	/// �߂�
	/// </summary>
	public void Return()
	{
		GM_Title.Instance.GetComponent<IGM_Title>().Return();
	}

	/// <summary>
	/// SE�Đ�
	/// </summary>
	/// <param name="clip">�Đ����鉹</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	void IStageSelectCanvas.SetLineMagnitude(float magnitude, bool isClear)
	{
		if (isClear)
		{
			scoreText.text = Mathf.Floor(magnitude).ToString() + "cm";
		}
		else
		{
			//scoreText.text = "��������cm";
			scoreText.text = "���N���A";
		}
	}

	void IStageSelectCanvas.SetPreviewImage(int number)
	{
		previewImage.sprite = previewSprites[number - 1];
		stageNameText.text = StageManager.Instance.StageNames[number - 1];
	}

	void IStageSelectCanvas.MissionProgression(int progress)
	{
		foreach (var e in missionProgresses)
		{
			e.sprite = notClearedSprite;
		}
		for (int i = 0; i < progress; ++i)
		{
			missionProgresses[i].sprite = clearSprite;
		}
	}
}
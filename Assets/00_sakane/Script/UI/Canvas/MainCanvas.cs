using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ���C���̃L�����o�X
public class MainCanvas : MonoBehaviour, IMainCanvas
{
	// ���b�Z�[�W
	[SerializeField]
	TextMeshProUGUI message;

	// ���������̒����̍��v
	[SerializeField]
	TextMeshProUGUI score;

	// �\������I����
	[SerializeField]
	//GameObject trueFalseSelectCanvasPrefab;
	GameObject trueFalseSelectCanvas;

	// �J�n���b�Z�[�W
	[SerializeField]
	GameObject startMessageObject;
	Image startMessageImage;
	bool isMessageDisplay = false;
	float time = 0;
	float flashValue = 0;
	[SerializeField]
	float flashSpeed;

	// �擾��������\������
	[SerializeField]
	List<Image> keyImageList = new List<Image>();

	// ���̃X�v���C�g
	[SerializeField]
	List<Sprite> keySprites = new List<Sprite>();

	// �{�^���𖳎�����摜
	[SerializeField]
	GameObject buttonIgnoreImage;

	private void Start()
	{
		//trueFalseSelectCanvas = Instantiate(trueFalseSelectCanvasPrefab);
		trueFalseSelectCanvas.SetActive(false);
		GetComponent<Canvas>().worldCamera = Camera.main;
		startMessageImage = startMessageObject.GetComponent<Image>();
		buttonIgnoreImage.SetActive(false);
	}

	private void Update()
	{
		if (isMessageDisplay)
		{
			time += flashSpeed;
			flashValue = Mathf.Sin(time);
			flashValue = (flashValue + 3.0f) / 4.0f;
			startMessageImage.material.SetFloat("_AlphaValue", flashValue);
		}
	}

	/// <summary>
	/// ���j���[�{�^���������ꂽ���ɌĂ΂��֐�
	/// </summary>
	public void ToMenu()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().MainMenu();
	}

	/// <summary>
	/// ���g���C�{�^���������ꂽ���ɌĂ΂��֐�
	/// </summary>
	public async void Retry()
	{
		// �I�����̕\��
		trueFalseSelectCanvas.SetActive(true);
		// ���b�Z�[�W�ݒ�
		trueFalseSelectCanvas.GetComponent<ITrueFalseSelectCanvas>().SetMessage("���g���C���܂����H");

		// �Q�[����~
		GM_Main.Instance.GetComponent<IGM_Main>().GameStop();

		// �{�^�����������܂ő҂�
		var result = await trueFalseSelectCanvas.GetComponent<ITrueFalseSelectCanvas>().SelectWait();

		// �͂���I�������ꍇ�͍ŏ�����
		if (result)
		{
			LevelManager.OpenLevel("MainScene").Forget();
			return;
		}

		// �I������\��
		trueFalseSelectCanvas.SetActive(false);

		// �Q�[���ĊJ
		GM_Main.Instance.GetComponent<IGM_Main>().GameReStart();
	}

	/// <summary>
	/// ������
	/// </summary>
	public void FastForward()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().FastForward();
	}

	/// <summary>
	/// �|�[�Y�{�^���������ꂽ���ɌĂ΂��֐�
	/// </summary>
	public void Pause()
	{
		GM_Main.Instance.GetComponent<IGM_Main>();
	}

	/// <summary>
	/// SE�Đ�
	/// </summary>
	/// <param name="clip">�Đ����鉹</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	/// <summary>
	/// UI�ɃJ�[�\�������킹���Ƃ�
	/// </summary>
	public void UIHovered()
	{
		Player.player.GetComponent<IPlayer>().DefaultCursor();
	}

	/// <summary>
	/// UI����J�[�\�����O�����Ƃ�
	/// </summary>
	public void UIUnHovered()
	{
		Player.player.GetComponent<IPlayer>().MagicCursor();
	}

	void IMainCanvas.SetStartMessageActive(bool isActive)
	{
		startMessageObject?.SetActive(isActive);
		isMessageDisplay = isActive;
	}

	void IMainCanvas.SetMessageText(string message)
	{
		//this.message.text = message;
	}

	void IMainCanvas.SetScoreText(string score)
	{
		this.score.text = score;
	}

	void IMainCanvas.CharacterGetKey(int number, GameInstance.KeyType keyType)
	{
		keyImageList[number - 1].color = Color.white;
		keyImageList[number - 1].sprite = keySprites[(int)keyType];
	}

	void IMainCanvas.KeyReset()
	{
		var keys = GameObject.FindObjectsByType<Key>(FindObjectsSortMode.None);
		// �����ɂ���
		foreach (var key in keyImageList)
		{
			key.color = Color.clear;
		}
		// �������鐔������������
		for (int i = 0; i < keys.Length; i++)
		{
			this.keyImageList[i].color = Color.black;
		}
	}

	void IMainCanvas.SetButtonInteractable(bool interactable)
	{
		buttonIgnoreImage.SetActive(!interactable);
	}
}
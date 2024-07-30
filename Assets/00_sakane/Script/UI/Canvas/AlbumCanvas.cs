using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �A���o����\������L�����o�X
public class AlbumCanvas : MonoBehaviour
{
	// �A���o����\������{�^��
	[SerializeField]
	List<Button> albumButtons = new List<Button>();

	// �A���o����\������C���[�W
	[SerializeField]
	Image albumImage;

	// �A���o���ɏo���G�t�F�N�g
	[SerializeField]
	Image albumEffect;

	float unScaledTime;

	private void Start()
	{
		//GetComponent<Canvas>().worldCamera = Camera.main;
		albumImage.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (albumEffect.IsActive())
		{
			albumEffect.material.SetFloat("_UnScaledTime", unScaledTime);
			unScaledTime += Time.unscaledDeltaTime;
		}
		else
		{
			unScaledTime = 0;
		}
	}

	private void OnEnable()
	{
		// �{�^���̏�ԏ�����
		foreach (var button in albumButtons)
		{
			button.interactable = false;
		}
		// ���邱�Ƃ̂ł���摜��\���A�{�^����L���ɂ���
		for (int i = 0; i < SaveManager.data.stillOpeingValue; ++i)
		{
			albumButtons[i].interactable = true;
			albumButtons[i].GetComponent<Image>().sprite = AlbumManager.Instance.GetStillSprite(i);
		}
	}

	/// <summary>
	/// �A���o����\��
	/// </summary>
	/// <param name="number">�A���o���̔ԍ�</param>
	public void AlbumDisplay(int number)
	{
		// �摜�ݒ�
		albumImage.sprite = AlbumManager.Instance.GetStillSprite(number);
		// �摜��\��
		albumImage.gameObject.SetActive(true);
		// �G�t�F�N�g�\��
		albumEffect.gameObject.SetActive(true);
		// �A���o���̃G�t�F�N�g����
		albumEffect.sprite = AlbumManager.Instance.GetStillEffect(number);
		// �}�e���A���ݒ�
		albumEffect.material = AlbumManager.Instance.GetStillMaterial(number);
	}

	/// <summary>
	/// �߂�{�^�����������Ƃ�
	/// </summary>
	public void Return()
	{
		gameObject.SetActive(false);
	}

	/// <summary>
	/// �A���o����\��
	/// </summary>
	public void AlbumDisable()
	{
		albumImage.gameObject.SetActive(false);
		albumEffect.gameObject.SetActive(false);
	}

	/// <summary>
	/// SE�Đ�
	/// </summary>
	/// <param name="clip">�Đ�����SE</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}
}

using UnityEngine;
using UnityEngine.UI;

// �I�v�V�����L�����o�X
public class OptionCanvas : MonoBehaviour
{
	// �}�X�^�[�{�����[���̃X���C�_�[
	[SerializeField]
	Slider masterSlider;
	// BGM�̃X���C�_�[
	[SerializeField]
	Slider bgmSlider;
	// SE�̃X���C�_�[
	[SerializeField]
	Slider seSlider;

	// �A���o��
	[SerializeField]
	GameObject albumCanvas;

	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
		albumCanvas.SetActive(false);

		var masDB = (SaveManager.data.MasterVolume + 80) / 80;
		var bgmDB = (SaveManager.data.BGMVolume + 80) / 80;
		var seDB = (SaveManager.data.SEVolume + 80) / 80;
		masterSlider.value = masDB * masDB * masDB * masDB;
		bgmSlider.value = bgmDB * bgmDB * bgmDB * bgmDB;
		seSlider.value = seDB * seDB * seDB * seDB;
	}

	/// <summary>
	/// �߂�
	/// </summary>
	public void Return()
	{
		switch (GameInstance.gameState)
		{
		case GameInstance.GameState.Title:
		case GameInstance.GameState.StageSelect:
		case GameInstance.GameState.Option:
			GM_Title.Instance.GetComponent<IGM_Title>().Return();
			break;
		case GameInstance.GameState.MainGame:
			GM_Main.Instance.GetComponent<IGM_Main>().MainMenu();
			break;
		}
	}

	/// <summary>
	/// �}�X�^�[�{�����[��
	/// </summary>
	/// <param name="volume">����</param>
	public void MasterSetting(float volume)
	{
		SoundManager.Instance.MasterVolumeChange(volume);
	}

	/// <summary>
	/// BGM�̐ݒ�
	/// </summary>
	/// <param name="volume">����</param>
	public void BGMSetting(float volume)
	{
		SoundManager.Instance.BGMVolumeChange(volume);
	}

	/// <summary>
	/// SE�̐ݒ�
	/// </summary>
	/// <param name="volume">����</param>
	public void SESetting(float volume)
	{
		SoundManager.Instance.SEVolumeChange(volume);
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
	/// �A���o���\��
	/// </summary>
	public void AlbumDisplay()
	{
		albumCanvas.SetActive(true);
	}

	/// <summary>
	/// �`���[�g���A��1�\��
	/// </summary>
	public void Tutorial1Display()
	{
		TutorialManager.Instance.Stage1TutorialEnable();
	}

	/// <summary>
	/// �`���[�g���A��10�\��
	/// </summary>
	public void Tutorial10Display()
	{
		TutorialManager.Instance.Stage10TutorialEnable();
	}
}
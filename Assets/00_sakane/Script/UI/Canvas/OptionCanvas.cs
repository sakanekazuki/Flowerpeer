using UnityEngine;
using UnityEngine.UI;

// オプションキャンバス
public class OptionCanvas : MonoBehaviour
{
	// マスターボリュームのスライダー
	[SerializeField]
	Slider masterSlider;
	// BGMのスライダー
	[SerializeField]
	Slider bgmSlider;
	// SEのスライダー
	[SerializeField]
	Slider seSlider;

	// アルバム
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
	/// 戻る
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
	/// マスターボリューム
	/// </summary>
	/// <param name="volume">音量</param>
	public void MasterSetting(float volume)
	{
		SoundManager.Instance.MasterVolumeChange(volume);
	}

	/// <summary>
	/// BGMの設定
	/// </summary>
	/// <param name="volume">音量</param>
	public void BGMSetting(float volume)
	{
		SoundManager.Instance.BGMVolumeChange(volume);
	}

	/// <summary>
	/// SEの設定
	/// </summary>
	/// <param name="volume">音量</param>
	public void SESetting(float volume)
	{
		SoundManager.Instance.SEVolumeChange(volume);
	}

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生する音</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	/// <summary>
	/// アルバム表示
	/// </summary>
	public void AlbumDisplay()
	{
		albumCanvas.SetActive(true);
	}

	/// <summary>
	/// チュートリアル1表示
	/// </summary>
	public void Tutorial1Display()
	{
		TutorialManager.Instance.Stage1TutorialEnable();
	}

	/// <summary>
	/// チュートリアル10表示
	/// </summary>
	public void Tutorial10Display()
	{
		TutorialManager.Instance.Stage10TutorialEnable();
	}
}
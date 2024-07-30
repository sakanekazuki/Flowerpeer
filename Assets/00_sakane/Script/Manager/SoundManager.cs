using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

// 音管理
public class SoundManager : ManagerBase<SoundManager>
{
	// 音量管理をしているオブジェクト
	[SerializeField]
	AudioMixer audioMixer;

	// 各種音量
	static float masterVolume = 0;
	public static float MaterVolume
	{
		get => masterVolume;
	}
	static float bgmVolume = 0;
	public static float BgmVolume
	{
		get => bgmVolume;
	}
	static float seVolume = 0;
	public static float SeVolume
	{
		get => seVolume;
	}

	// 音を鳴らしているオブジェクト
	List<GameObject> soundObjects = new List<GameObject>();
	public List<GameObject> SoundObjects
	{
		get => soundObjects;
	}

	private void Start()
	{
		//var masDB = (SaveManager.data.MasterVolume + 80) / 80;
		//var bgmDB = (SaveManager.data.BGMVolume + 80) / 80;
		//var seDB = (SaveManager.data.SEVolume + 80) / 80;
		//masterVolume = masDB * masDB * masDB;
		//bgmVolume = bgmDB * bgmDB * bgmDB;
		//seVolume = seDB * seDB * seDB;

		masterVolume = SaveManager.data.MasterVolume;
		bgmVolume = SaveManager.data.BGMVolume;
		seVolume = SaveManager.data.SEVolume;

		audioMixer.SetFloat("MasterVolume", masterVolume);
		audioMixer.SetFloat("BGMVolume", bgmVolume);
		audioMixer.SetFloat("SEVolume", seVolume);

		//MasterVolumeChange(masterVolume);
		//BGMVolumeChange(bgmVolume);
		//SEVolumeChange(seVolume);
	}

	public void MasterVolumeChange(float volume)
	{
		var db = Mathf.Sqrt(Mathf.Sqrt(volume)) * 80 - 80;
		masterVolume = db;
		SaveManager.data.MasterVolume = db;
		audioMixer.SetFloat("MasterVolume", masterVolume);
	}

	/// <summary>
	/// BGM変更
	/// </summary>
	/// <param name="volume">音量</param>
	public void BGMVolumeChange(float volume)
	{
		var db = Mathf.Sqrt(Mathf.Sqrt(volume)) * 80 - 80;
		bgmVolume = db;
		SaveManager.data.BGMVolume = db;
		audioMixer.SetFloat("BGMVolume", bgmVolume);
	}

	/// <summary>
	/// SE変更
	/// </summary>
	/// <param name="volume">音量</param>
	public void SEVolumeChange(float volume)
	{
		var db = Mathf.Sqrt(Mathf.Sqrt(volume)) * 80 - 80;
		seVolume = db;
		SaveManager.data.SEVolume = db;
		audioMixer.SetFloat("SEVolume", seVolume);
	}

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生する音</param>
	/// <returns></returns>
	public async UniTaskVoid SEPlay(AudioClip clip)
	{
		// トークン取得
		var token = this.GetCancellationTokenOnDestroy();

		// オブジェクト生成
		var se = new GameObject();
		// 音を鳴らしているオブジェクト追加
		SoundObjects.Add(se);
		// 音を鳴らすスクリプト追加
		var source = se.AddComponent<AudioSource>();
		// AudioMixerのグループ設定
		source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SE")[0];
		// 音を鳴らす
		source.PlayOneShot(clip);
		// 再生が終了するまで待つ
		await UniTask.WaitUntil(() => !source.isPlaying, cancellationToken: token);
		// 音を鳴らしているオブジェクト削除
		SoundObjects.Remove(se);
		// 再生したオブジェクトを削除
		Destroy(se);
	}
}
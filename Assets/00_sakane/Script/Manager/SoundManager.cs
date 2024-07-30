using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

// ���Ǘ�
public class SoundManager : ManagerBase<SoundManager>
{
	// ���ʊǗ������Ă���I�u�W�F�N�g
	[SerializeField]
	AudioMixer audioMixer;

	// �e�퉹��
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

	// ����炵�Ă���I�u�W�F�N�g
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
	/// BGM�ύX
	/// </summary>
	/// <param name="volume">����</param>
	public void BGMVolumeChange(float volume)
	{
		var db = Mathf.Sqrt(Mathf.Sqrt(volume)) * 80 - 80;
		bgmVolume = db;
		SaveManager.data.BGMVolume = db;
		audioMixer.SetFloat("BGMVolume", bgmVolume);
	}

	/// <summary>
	/// SE�ύX
	/// </summary>
	/// <param name="volume">����</param>
	public void SEVolumeChange(float volume)
	{
		var db = Mathf.Sqrt(Mathf.Sqrt(volume)) * 80 - 80;
		seVolume = db;
		SaveManager.data.SEVolume = db;
		audioMixer.SetFloat("SEVolume", seVolume);
	}

	/// <summary>
	/// SE�Đ�
	/// </summary>
	/// <param name="clip">�Đ����鉹</param>
	/// <returns></returns>
	public async UniTaskVoid SEPlay(AudioClip clip)
	{
		// �g�[�N���擾
		var token = this.GetCancellationTokenOnDestroy();

		// �I�u�W�F�N�g����
		var se = new GameObject();
		// ����炵�Ă���I�u�W�F�N�g�ǉ�
		SoundObjects.Add(se);
		// ����炷�X�N���v�g�ǉ�
		var source = se.AddComponent<AudioSource>();
		// AudioMixer�̃O���[�v�ݒ�
		source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SE")[0];
		// ����炷
		source.PlayOneShot(clip);
		// �Đ����I������܂ő҂�
		await UniTask.WaitUntil(() => !source.isPlaying, cancellationToken: token);
		// ����炵�Ă���I�u�W�F�N�g�폜
		SoundObjects.Remove(se);
		// �Đ������I�u�W�F�N�g���폜
		Destroy(se);
	}
}
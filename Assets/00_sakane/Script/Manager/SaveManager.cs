using System.IO;
using UnityEngine;

// �ۑ��N���X
public class SaveManager : ManagerBase<SaveManager>
{
	// �t�@�C���p�X
	static string filePath;
	// �Z�[�u�f�[�^
	public static SaveData data;

	//[SerializeField]
	//TextMeshProUGUI t;

	//private void Start()
	//{
	//	t.text = filePath;
	//}

	private void OnApplicationQuit()
	{
		Save();
	}

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		filePath = Application.persistentDataPath + "/" + "savedata.json";
		data = new SaveData();
		for (int i = 0; i < GameInstance.stageValue; ++i)
		{
			data.isClear.Add(false);
			data.isOpening.Add(false);
			data.minLineMagnitude.Add(9999);
			data.missionStates.Add(GameInstance.MissionState.NotCleared);
		}

		Load();
	}

	/// <summary>
	/// �ۑ�
	/// </summary>
	void Save()
	{
		data.MasterVolume = Mathf.Floor(data.MasterVolume);
		data.BGMVolume = Mathf.Floor(data.BGMVolume);
		data.SEVolume = Mathf.Floor(data.SEVolume);
		string json = JsonUtility.ToJson(data);
		StreamWriter streamWriter = new StreamWriter(filePath);
		streamWriter.Write(json); streamWriter.Flush();
		streamWriter.Close();
	}

	/// <summary>
	/// �ǂݍ���
	/// </summary>
	static void Load()
	{
		if (File.Exists(filePath))
		{
			StreamReader streamReader;
			streamReader = new StreamReader(filePath);
			string saveData = streamReader.ReadToEnd();
			streamReader.Close();
			data = JsonUtility.FromJson<SaveData>(saveData);
		}
		if (data.missionStates.Count <= 0)
		{
			for (int i = data.missionStates.Count; i < GameInstance.stageValue; ++i)
			{
				data.missionStates.Add(GameInstance.MissionState.NotCleared);
			}
		}
	}
}
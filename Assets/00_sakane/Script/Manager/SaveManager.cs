using System.IO;
using UnityEngine;

// 保存クラス
public class SaveManager : ManagerBase<SaveManager>
{
	// ファイルパス
	static string filePath;
	// セーブデータ
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
	/// 保存
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
	/// 読み込み
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
using System.Collections.Generic;

// セーブデータ
[System.Serializable]
public class SaveData
{
	// 音量(マスター,BGM,SE)
	public float MasterVolume = -20;
	public float BGMVolume = -20;
	public float SEVolume = -20;

	// チュートリアルの表示
	public bool isStage1TutorialFirst = false;
	public bool isStage10TutorialFirst = false;

	// ステージの状態
	public List<bool> isClear = new List<bool>();
	public List<bool> isOpening = new List<bool>();
	public List<float> minLineMagnitude = new List<float>();

	// ミッションの状態
	public List<GameInstance.MissionState> missionStates = new List<GameInstance.MissionState>();

	public int stillOpeingValue = 0;
}
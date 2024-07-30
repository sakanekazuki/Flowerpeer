using UnityEngine;

// ゲームインスタンス
public class GameInstance
{
	public enum GameState
	{
		Title,
		Option,
		StageSelect,
		MainGame,
	}

	public enum MissionState
	{
		NotCleared = 0,
		Onece      = 1,
		Twice      = 2,
		Thrice     = 3,
	}

	public enum KeyType
	{
		Banana = 0,
		Grape  = 1,
		Orange = 2,
		Berry  = 3,
	}

	// インスタンス
	static GameInstance instance;
	// インスタンス取得
	public static GameInstance Instance
	{
		get => instance;
	}

	// 合計ステージ数
	public static int stageValue = 30;

	public static GameState gameState = GameState.Title;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	private GameInstance()
	{

	}

	// ゲーム開始時に生成
	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		//Debug.Log(Application.persistentDataPath);
		instance = new GameInstance();

#if UNITY_EDITOR
		if (isDebug)
		{
			PlayerPrefs.DeleteAll();
		}
#endif
	}

#if UNITY_EDITOR
	// デバッグ機能
	public readonly static bool isDebug = true;
#endif
}
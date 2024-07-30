using UnityEngine;

// �Q�[���C���X�^���X
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

	// �C���X�^���X
	static GameInstance instance;
	// �C���X�^���X�擾
	public static GameInstance Instance
	{
		get => instance;
	}

	// ���v�X�e�[�W��
	public static int stageValue = 30;

	public static GameState gameState = GameState.Title;

	/// <summary>
	/// �R���X�g���N�^
	/// </summary>
	private GameInstance()
	{

	}

	// �Q�[���J�n���ɐ���
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
	// �f�o�b�O�@�\
	public readonly static bool isDebug = true;
#endif
}
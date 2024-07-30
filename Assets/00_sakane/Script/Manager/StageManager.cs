using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

// ステージ管理クラス
public class StageManager : ManagerBase<StageManager>
{
	// 現在のステージ番号
	static int nowStageNumber = 1;
	public static int NowStageNumber
	{
		get => nowStageNumber;
	}

	// ステージ
	Object stageObject = null;

	// true = ステージ読み込み中
	bool isLoadingStage = false;
	public bool IsLoadingStage
	{
		get => isLoadingStage;
	}

	// BGM
	[SerializeField]
	AudioClip bgmClip1;
	[SerializeField]
	AudioClip bgmClip2;
	[SerializeField]
	AudioClip bgmClip3;
	[SerializeField]
	AudioClip bgmClip4;

	// BGMを鳴らす
	AudioSource bgmSource;

	// ミッションのクリア条件
	[SerializeField]
	SO_MissionConditions missionConditions;
	public SO_MissionConditions MissionConditions
	{
		get => missionConditions;
	}

	// ステージ名
	[SerializeField]
	List<string> stageNames = new List<string>();
	public List<string> StageNames
	{
		get => stageNames;
	}

	private void Start()
	{
		bgmSource = Camera.main.GetComponent<AudioSource>();
	}

	/// <summary>
	/// ステージクリア
	/// </summary>
	public void StageClear()
	{
		bgmSource.Stop();

		if (nowStageNumber < 30)
		{
			SaveManager.data.isOpening[nowStageNumber] = true;
		}
		//// ゲームクリア
		//if(nowStageNumber == stageValue)
		//{
		//	GM_Main.Instance.GetComponent<IGM_Main>().GameClear();
		//}
		SaveManager.data.isClear[nowStageNumber - 1] = true;
		LineManager.Instance.MagnitudeSave();
		for (int i = 3; i > 1; --i)
		{
			if ((int)SaveManager.data.missionStates[nowStageNumber - 1] > (i - 1))
			{
				return;
			}
			if (Mathf.Floor(SaveManager.data.minLineMagnitude[nowStageNumber - 1]) <= missionConditions.missionConditions[nowStageNumber - 1].ClearConditions[i - 1])
			{
				SaveManager.data.missionStates[nowStageNumber - 1] = (GameInstance.MissionState)System.Enum.ToObject(typeof(GameInstance.MissionState), i);
				return;
			}
		}
		SaveManager.data.missionStates[nowStageNumber - 1] = GameInstance.MissionState.NotCleared;
	}

	/// <summary>
	/// ステージのミッションクリア条件取得
	/// </summary>
	/// <param name="stageNumber">取得するステージの番号</param>
	/// <returns>クリア条件</returns>
	public List<float> GetMissionClearCondition(int stageNumber)
	{
		return missionConditions.missionConditions[stageNumber].ClearConditions;
	}

	/// <summary>
	/// 次のステージに移動
	/// </summary>
	public async UniTask<bool> NextStage()
	{
		// 読み込み状態にする
		isLoadingStage = true;
		var result = await StageLoadAsync(++nowStageNumber);
		return result;
	}

	/// <summary>
	/// 非同期でステージ生成
	/// </summary>
	/// <param name="stageNumber">ステージ番号</param>
	/// <returns></returns>
	public async UniTask<bool> StageLoadAsync(int stageNumber)
	{
		GM_Main.isPlaying = false;
		if (stageObject != null)
		{
			// 作成していたステージを削除
			Destroy(stageObject);
		}
		// 読み込み状態にする
		isLoadingStage = true;
		// ステージ読み込み
		var loadObject = await Resources.LoadAsync("Stage/stage" + stageNumber.ToString());

		SaveManager.data.isOpening[stageNumber - 1] = true;

		if (loadObject == null)
		{
			return false;
		}
		// ステージ生成
		stageObject = Instantiate(loadObject);
		// 現在のステージ番号
		nowStageNumber = stageNumber;
		// ステージ読み込み終了状態にする
		isLoadingStage = false;

		// BGMを流す物がない場合は取得
		await UniTask.WaitUntil(() =>
		{
			if (bgmSource == null)
			{
				bgmSource = Camera.main.GetComponent<AudioSource>();
			}
			return bgmSource != null;
		});
		// BGM再生
		if (stageNumber <= 10)
		{
			bgmSource.clip = bgmClip1;
		}
		else if (stageNumber <= 20)
		{
			bgmSource.clip = bgmClip2;
		}
		else if (stageNumber <= 25)
		{
			bgmSource.clip = bgmClip3;
		}
		else
		{
			bgmSource.clip = bgmClip4;
		}
		bgmSource.Play();

		return true;
	}
}
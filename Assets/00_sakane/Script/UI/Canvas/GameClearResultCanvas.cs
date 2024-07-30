using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameClearResultCanvas : MonoBehaviour
{
	// リザルトを表示するテキスト
	[SerializeField]
	TextMeshProUGUI resultText;

	// ミッションの状態を表示する画像
	[SerializeField]
	List<Image> missionConditionImage = new List<Image>();
	[SerializeField]
	List<Sprite> missionConditionSprite = new List<Sprite>();

	// ミッションのクリア条件を表示するテキスト
	[SerializeField]
	List<TMP_Text> missionClearConditionText = new List<TMP_Text>();

	private void OnEnable()
	{
		// 小数点以下切り捨てした値を表示
		resultText.text = Mathf.Floor(LineManager.Instance.AllLineMagnitude).ToString() + "cm";
		foreach (var e in missionConditionImage)
		{
			// スプライト初期化
			e.sprite = missionConditionSprite[0];
		}
		foreach (var e in StageManager.Instance.GetMissionClearCondition(StageManager.NowStageNumber - 1))
		{
			// 配列の要素番号
			var arrayNumber = StageManager.Instance.GetMissionClearCondition(StageManager.NowStageNumber - 1).IndexOf(e);
			// クリア条件を代入
			missionClearConditionText[arrayNumber/* - arrayNumber*/].text = e.ToString() + "cm";
		}
		GameInstance.MissionState missionState = GameInstance.MissionState.NotCleared;
		for (int i = 3; i > 0; --i)
		{
			if (Mathf.Floor(LineManager.Instance.AllLineMagnitude) <= StageManager.Instance.MissionConditions.missionConditions[StageManager.NowStageNumber - 1].ClearConditions[i - 1])
			{
				// 今のスコアでミッションがどれくらいクリアされたのか取得	
				missionState = (GameInstance.MissionState)System.Enum.ToObject(typeof(GameInstance.MissionState), i);
				break;
			}
		}
		for (int i = 0; i < (int)missionState; ++i)
		{
			// クリアしたスプライトに変更
			missionConditionImage[i].sprite = missionConditionSprite[1];
		}
	}

	public void ToStill()
	{
		gameObject.SetActive(false);
	}
}

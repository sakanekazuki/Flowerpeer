using System.Collections.Generic;
using UnityEngine;

// 線管理クラス
public class LineManager : ManagerBase<LineManager>
{
	// 管理する線
	List<GameObject> lines = new List<GameObject>();
	// 線の長さ
	float blueLineMagnitude = 0;
	float redLineMagnitude = 0;
	float allLineMagnitude = 0;
	public float AllLineMagnitude
	{
		get => allLineMagnitude;
	}

	// 線を描いた長さを記録できる最大値
	[SerializeField]
	float maxMagnitude = 9999;

	/// <summary>
	/// 線の長さ保存
	/// </summary>
	public void MagnitudeSave()
	{
		var stageNumber = StageManager.NowStageNumber;

		// 線を引いた合計値を計算
		allLineMagnitude = blueLineMagnitude + redLineMagnitude;
		// 合計値が最大値を超えていた場合最大値にする
		if (allLineMagnitude > maxMagnitude)
		{
			allLineMagnitude = maxMagnitude;
		}
		// 今までで一番低い値だった場合保存
		if (SaveManager.data.minLineMagnitude[stageNumber - 1] > allLineMagnitude)
		{
			SaveManager.data.minLineMagnitude[stageNumber - 1] = allLineMagnitude;
		}
	}

	/// <summary>
	/// 管理する線追加
	/// </summary>
	/// <param name="line">管理する線</param>
	public void LineCreate(GameObject line)
	{
		lines.Add(line);

		// 青い線
		if (line.layer == 10)
		{
			blueLineMagnitude += line.transform.localScale.x;
		}
		else if (line.layer == 11)
		{
			redLineMagnitude += line.transform.localScale.x;
		}

		// 合計を計算
		var magnitude = blueLineMagnitude + redLineMagnitude;
		// 最大の場合最大値にする
		if (magnitude > maxMagnitude)
		{
			magnitude = maxMagnitude;
		}
		// 線の長さの表示(切り捨て)
		GM_Main.Instance.GetComponent<IGM_Main>().SetLineMagnitude(Mathf.Floor(magnitude));
	}

	/// <summary>
	/// 全ての線削除
	/// </summary>
	public void LineDeleteAll()
	{
		foreach (var line in lines)
		{
			Destroy(line);
		}
		// 描いた線を消す
		lines.Clear();
		// 線の長さリセット
		blueLineMagnitude = 0;
		redLineMagnitude = 0;
		GM_Main.Instance.GetComponent<IGM_Main>().SetLineMagnitude(0);
	}
}
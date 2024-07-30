using System;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// ステージ開始時
public class StageStartCanvas : MonoBehaviour,IStageStartCanvas
{
	// 表示する時間
	[SerializeField]
	float displayTime = 0.3f;

	// ステージ数
	[SerializeField]
	TMP_Text stageNumberText;
	// ステージ名
	[SerializeField]
	Text stageNameText;

	// フェード速度
	[SerializeField]
	float fadeSpeed;

	private void Awake()
	{
		// メインカメラ設定
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	/// <summary>
	/// フェード
	/// </summary>
	async UniTask<bool> StartMessageFade()
	{
		var token = this.GetCancellationTokenOnDestroy();

		//var stageNumberTargetPos = stageNumberText.transform.position;
		//var stageNumberStartPos = stageNumberText.transform.position;
		//var stageNameTargetPos = stageNameText.transform.position;
		//var stageNameStartPos = stageNameText.transform.position;

		//// 真ん中に来るまで待つ
		//await UniTask.WaitUntil(
		//() =>
		//{
		//	stageNumberText.transform.position = Vector3.Lerp(stageNumberText.transform.position, new Vector3(0, stageNumberTargetPos.y, stageNumberStartPos.z), fadeSpeed);
		//	stageNameText.transform.position = Vector3.Lerp(stageNameText.transform.position, new Vector3(0, stageNameTargetPos.y, stageNameStartPos.z), fadeSpeed);
		//	return stageNameText.transform.position.x >= -0.01f;
		//}, cancellationToken: token);

		// 真ん中で停止
		await UniTask.Delay(TimeSpan.FromSeconds(displayTime), cancellationToken: token) ;

		//stageNumberTargetPos = new Vector3( -stageNumberStartPos.x,stageNumberStartPos.y, stageNumberStartPos.z);
		//stageNameTargetPos = new Vector3(-stageNameStartPos.x, stageNameStartPos.y, stageNameStartPos.z);

		//// 外に出る
		//await UniTask.WaitUntil(
		//() =>
		//{
		//	stageNumberText.transform.position = Vector3.Lerp(stageNumberText.transform.position, stageNumberTargetPos, fadeSpeed);
		//	stageNameText.transform.position = Vector3.Lerp(stageNameText.transform.position, stageNameTargetPos, fadeSpeed);
		//	return stageNameText.transform.position.x >= (-stageNameStartPos.x) - 0.01f;
		//}, cancellationToken: token);

		return true;
	}

	/// <summary>
	/// ステージ開始
	/// </summary>
	/// <returns></returns>
	/*async UniTask<bool>*/void IStageStartCanvas.StageStart()
	{
		//var token = this.GetCancellationTokenOnDestroy();

		// ステージ数設定
		stageNumberText.text = "ステージ" + StageManager.NowStageNumber.ToString();
		stageNameText.text = StageManager.Instance.StageNames[StageManager.NowStageNumber - 1].ToString();

		//await StartMessageFade();

		//gameObject.SetActive(false);
		//return true;
	}
}
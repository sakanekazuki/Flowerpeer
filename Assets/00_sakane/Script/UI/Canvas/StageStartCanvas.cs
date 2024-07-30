using System;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �X�e�[�W�J�n��
public class StageStartCanvas : MonoBehaviour,IStageStartCanvas
{
	// �\�����鎞��
	[SerializeField]
	float displayTime = 0.3f;

	// �X�e�[�W��
	[SerializeField]
	TMP_Text stageNumberText;
	// �X�e�[�W��
	[SerializeField]
	Text stageNameText;

	// �t�F�[�h���x
	[SerializeField]
	float fadeSpeed;

	private void Awake()
	{
		// ���C���J�����ݒ�
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	/// <summary>
	/// �t�F�[�h
	/// </summary>
	async UniTask<bool> StartMessageFade()
	{
		var token = this.GetCancellationTokenOnDestroy();

		//var stageNumberTargetPos = stageNumberText.transform.position;
		//var stageNumberStartPos = stageNumberText.transform.position;
		//var stageNameTargetPos = stageNameText.transform.position;
		//var stageNameStartPos = stageNameText.transform.position;

		//// �^�񒆂ɗ���܂ő҂�
		//await UniTask.WaitUntil(
		//() =>
		//{
		//	stageNumberText.transform.position = Vector3.Lerp(stageNumberText.transform.position, new Vector3(0, stageNumberTargetPos.y, stageNumberStartPos.z), fadeSpeed);
		//	stageNameText.transform.position = Vector3.Lerp(stageNameText.transform.position, new Vector3(0, stageNameTargetPos.y, stageNameStartPos.z), fadeSpeed);
		//	return stageNameText.transform.position.x >= -0.01f;
		//}, cancellationToken: token);

		// �^�񒆂Œ�~
		await UniTask.Delay(TimeSpan.FromSeconds(displayTime), cancellationToken: token) ;

		//stageNumberTargetPos = new Vector3( -stageNumberStartPos.x,stageNumberStartPos.y, stageNumberStartPos.z);
		//stageNameTargetPos = new Vector3(-stageNameStartPos.x, stageNameStartPos.y, stageNameStartPos.z);

		//// �O�ɏo��
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
	/// �X�e�[�W�J�n
	/// </summary>
	/// <returns></returns>
	/*async UniTask<bool>*/void IStageStartCanvas.StageStart()
	{
		//var token = this.GetCancellationTokenOnDestroy();

		// �X�e�[�W���ݒ�
		stageNumberText.text = "�X�e�[�W" + StageManager.NowStageNumber.ToString();
		stageNameText.text = StageManager.Instance.StageNames[StageManager.NowStageNumber - 1].ToString();

		//await StartMessageFade();

		//gameObject.SetActive(false);
		//return true;
	}
}
using Cysharp.Threading.Tasks;
using UnityEngine;

// カウントダウン
public class CountDownManager : ManagerBase<CountDownManager>
{
	// カウントダウンのキャンバス
	[SerializeField]
	GameObject countDownCanvasPrefab;
	GameObject countDownCanvas;
	ICountDownCanvas icountDownCanvas;

	private void Start()
	{
		countDownCanvas = Instantiate(countDownCanvasPrefab);
		countDownCanvas.SetActive(false);
		icountDownCanvas = countDownCanvas.GetComponent<ICountDownCanvas>();
	}

	/// <summary>
	/// カウントダウン開始
	/// </summary>
	/// <param name="countDownTime">カウントダウンする時間</param>
	/// <returns></returns>
	public async UniTask<bool> CountDownStart(float countDownTime)
	{
		countDownCanvas.SetActive(true);
		var time = countDownTime;
		while (time >= 0)
		{
			await UniTask.DelayFrame(1);
			time -= Time.deltaTime;
			icountDownCanvas.SetCountDownText(Mathf.Floor(time).ToString());
		}
		countDownCanvas.SetActive(false);
		return true;
	}
}
using TMPro;
using UnityEngine;

// カウントダウンキャンバス
public class CountDownCanvas : MonoBehaviour, ICountDownCanvas
{
	// カウントダウンテキスト
	[SerializeField]
	TextMeshProUGUI countDownTxt;

	/// <summary>
	/// カウントダウンテキストの設定
	/// </summary>
	/// <param name="value">設定する文字</param>
	void SetCountDownText(string value)
	{
		countDownTxt.text = value;
	}

	void ICountDownCanvas.SetCountDownText(string value)
	{
		SetCountDownText(value);
	}
}
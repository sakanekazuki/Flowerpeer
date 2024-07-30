using UnityEngine;
using UnityEngine.UI;

// テキストメッシュプロ点滅
public class TextMeshProFlash : MonoBehaviour
{
	// 点滅させるテキスト
	Image messageText;

	// 点滅速度
	[SerializeField]
	float speed;

	// 色
	Color defaultColor = Color.white;

	// 点滅している時間
	float time = 0;

	private void Awake()
	{
		// テキスト取得
		messageText = GetComponent<Image>();
		defaultColor = messageText.color;
	}

	private void FixedUpdate()
	{
		time += speed;
		var alphaValue = (Mathf.Sin(time) + 1) / 2;
		messageText.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alphaValue);
	}
}
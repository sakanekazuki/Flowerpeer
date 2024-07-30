using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

// ボタン
public class MyButton : MonoBehaviour
{
	// 速度
	float animSpeed = 0.05f;

	// true = アニメーション中
	bool isAnim = false;

	Vector3 defaultScale = Vector3.zero;

	private void Awake()
	{
		defaultScale = transform.localScale;
	}

	private void OnDisable()
	{
		isAnim = false;
	}

	/// <summary>
	/// クリック
	/// </summary>
	public void Click()
	{
		//Debug.Log(transform.position);
		var pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		ClickEffectManager.Instance.EffectCreate(new Vector3(pos.x, pos.y, 0));
		//Debug.Log(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
		//ClickEffectManager.Instance.EffectCreate(new Vector3(0, 0, 0));
		//ClickEffectManager.Instance.EffectCreate(Mouse.current.position.ReadValue());
	}

	/// <summary>
	/// 選択
	/// </summary>
	public async void Select()
	{
		// トークン取得
		//var token = this.GetCancellationTokenOnDestroy();
		var time = 0.0f;
		isAnim = true;
		while (isAnim)
		{
			time += animSpeed;
			var value = (Mathf.Sin(time) + 1) * 0.1f + defaultScale.x;
			transform.localScale = new Vector3(value, value, transform.localScale.z);
			await UniTask.DelayFrame(1/*, cancellationToken: token*/);
		}
	}

	/// <summary>
	/// 選択解除
	/// </summary>
	public void Deselect()
	{
		isAnim = false;
		// 元の大きさに戻す
		transform.localScale = defaultScale;
	}
}
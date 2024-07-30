using System.Collections.Generic;
using UnityEngine;

// アクティブ状態反転のスイッチ
public class DisappearSwitch : SwitchGimmickBase
{
	// 消えるオブジェクト
	[SerializeField]
	List<GameObject> targetNeedle = new List<GameObject>();

	// true = オブジェクトが出現している
	bool isActive = true;

	Animator animator;

	// 衝突した際に鳴らすSE
	[SerializeField]
	AudioClip hitSE;

	// true = キノコを押している
	bool isOn = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Character"))
		{
			isOn = false;
			animator.SetTrigger("Off");
		}
	}

	protected override void OnSwitch()
	{
		if (isOn)
		{
			return;
		}
		// 針の状態反転
		isActive = !isActive;
		foreach (var obj in targetNeedle)
		{
			obj.SetActive(isActive);
		}
		animator.SetTrigger("On");
		SoundManager.Instance.SEPlay(hitSE).Forget();
		isOn = true;
	}
}
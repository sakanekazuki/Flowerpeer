using UnityEngine;

// スイッチ
public abstract class SwitchGimmickBase : GimmickBase
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Character"))
		{
			OnSwitch();
		}
	}

	// スイッチを押した際のアクション
	protected virtual void OnSwitch()
	{

	}
}
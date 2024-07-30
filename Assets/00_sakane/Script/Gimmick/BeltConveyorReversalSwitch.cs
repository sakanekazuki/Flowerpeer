using System.Collections.Generic;
using UnityEngine;

// ベルトコンベアー反転
public class BeltConveyorReversalSwitch : MonoBehaviour
{
	// 反転させるコンベアー
	[SerializeField]
	List<GameObject> targets = new List<GameObject>();

	// アニメーター
	Animator animator;

	// 衝突時のSE
	[SerializeField]
	AudioClip hitSE;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Character"))
		{
			foreach (var target in targets)
			{
				target.GetComponent<IBeltConveyor>().Reversal();
			}
			animator.SetTrigger("On");
			SoundManager.Instance.SEPlay(hitSE).Forget();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Character"))
		{
			animator.SetTrigger("Off");
		}
	}
}
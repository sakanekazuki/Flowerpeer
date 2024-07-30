using System.Collections.Generic;
using UnityEngine;

// �x���g�R���x�A�[���]
public class BeltConveyorReversalSwitch : MonoBehaviour
{
	// ���]������R���x�A�[
	[SerializeField]
	List<GameObject> targets = new List<GameObject>();

	// �A�j���[�^�[
	Animator animator;

	// �Փˎ���SE
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
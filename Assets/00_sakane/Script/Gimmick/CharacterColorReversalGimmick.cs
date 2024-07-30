using UnityEngine;

// 色反転ギミック
public class CharacterColorReversalGimmick : GimmickBase
{
	Animator animator;

	// 衝突時のSE
	[SerializeField]
	AudioClip hitSE;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	protected override void Invocation()
	{
		// 全てのキャラクターの色変更
		CharacterManager.Instance.AllCharacterColorChange();
		SoundManager.Instance.SEPlay(hitSE).Forget();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Character"))
		{
			Invocation();
			animator.SetTrigger("On");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Character"))
		{
			animator.SetTrigger("Off");
		}
	}
}
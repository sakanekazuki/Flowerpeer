using UnityEngine;

// �F���]�M�~�b�N
public class CharacterColorReversalGimmick : GimmickBase
{
	Animator animator;

	// �Փˎ���SE
	[SerializeField]
	AudioClip hitSE;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	protected override void Invocation()
	{
		// �S�ẴL�����N�^�[�̐F�ύX
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
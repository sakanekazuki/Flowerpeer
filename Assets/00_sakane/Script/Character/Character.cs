using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

// �ړ�����L�����N�^�[
public class Character : MonoBehaviour, ICharacter
{
	// �ړ����x
	[SerializeField]
	float speed;

	// ����
	Rigidbody2D rb2d;

	// �X�v���C�g
	SpriteRenderer spriteRenderer;
	// �摜�T�C�Y
	Vector2 spriteSize = Vector2.zero;
	public Vector2 SpriteSize
	{
		get => spriteSize;
	}

	// �������Ă��錮��ID
	static List<int> keyIds = new List<int>();

	// true = ���S
	bool isDead = false;

	// true = �L�����N�^�[��~
	bool isStop = false;

	// true = �L�����N�^�[�S�[��
	bool isGoal = false;

	// �o�邱�Ƃ̂ł��鍂��
	[SerializeField]
	float climHeight = 0.2f;

	// �A�j���[�^�[
	Animator animator;

	// true = �Փ˂ł���
	//bool canHit = true;

	// �O��̈ʒu
	Vector3 beforePosition = Vector3.zero;

	// ���̏�Ŏ~�܂��Ă����t���[����
	int stopFrameNumber = 0;

	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteSize = spriteRenderer.sprite.bounds.size * new Vector2(transform.localScale.x, transform.localScale.y);

		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		if (isDead || isStop || isGoal)
		{
			return;
		}
		// �ړ�
		rb2d.velocity = transform.right * speed + new Vector3(0, rb2d.velocity.y * rb2d.gravityScale, 0);
		if (beforePosition == transform.position)
		{
			stopFrameNumber++;
			if (stopFrameNumber > 3)
			{
				if (transform.right.x < 0)
				{
					transform.localEulerAngles = new Vector3(0, 0, 0);
				}
				else
				{
					transform.localEulerAngles = new Vector3(0, 180, 0);
				}
			}
		}
		else
		{
			stopFrameNumber = 0;
		}
		beforePosition = transform.position;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//if (!canHit)
		//{
		//	return;
		//}
		//canHit = false;
		//HitDelay();

		// �h�A�ɓ��������ꍇ���ŊJ�����m�F
		if (collision.gameObject.CompareTag("Door"))
		{
			// �h�A�C���^�[�t�F�[�X�擾
			var idoor = collision.gameObject.GetComponent<IDoor>();
			foreach (var id in keyIds)
			{
				if (idoor.Open(id))
				{
					return;
				}
			}
		}

		// �j�ɏՓ˂����ꍇ���S
		if (collision.gameObject.CompareTag("Needle"))
		{
			Dead();
			return;
		}

		if (collision.contactCount <= 0)
		{
			return;
		}
		// �^���ɂ������Ă����ꍇ�͔�����s��Ȃ�
		if (collision.contacts[0].normal == Vector2.up)
		{
			return;
		}

		// �Փˑ���̊p�x�𒲂ׂ�
		var angle = Vector2.SignedAngle(transform.right, collision.contacts[0].normal);
		// �Փ˂����I�u�W�F�N�g�������̌�������45�x�������ꍇ�o��
		if (!(collision.contacts[0].normal.y > 0 && (180 - angle) >= GM_Main.climbableAngle))
		{
			// ���]
			if (transform.right == Vector3.right)
			{
				transform.localEulerAngles = new Vector3(0, 180, 0);
			}
			else if (transform.right == -Vector3.right)
			{
				transform.localEulerAngles = new Vector3(0, 0, 0);
			}
			return;
		}
		//// �Փˑ���̊p�x�𒲂ׂ�
		//var angle =	collision.gameObject.transform.localEulerAngles;
		//// �Փ˂����I�u�W�F�N�g�������̌�������45�x�������ꍇ�o��
		//if (collision.contacts[0].normal.y > 0 && (angle.z <= GM_Main.climbableAngle || (180 - angle.z) <= GM_Main.climbableAngle))
		//{
		//	return;
		//}

		var dir = Direction(collision.contacts[0].normal);
		if (transform.right != new Vector3(dir.x, dir.y))
		{
			// �i���𒴂���
			if (collision.contacts[0].point.y > (transform.position + transform.up * climHeight).y)
			{
				// ���]
				if (transform.right == Vector3.right)
				{
					transform.localEulerAngles = new Vector3(0, 180, 0);
				}
				else if (transform.right == -Vector3.right)
				{
					transform.localEulerAngles = new Vector3(0, 0, 0);
				}
				return;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Goal"))
		{
			Goal();
		}
	}

	private void OnDestroy()
	{
		keyIds.Clear();
	}

	/// <summary>
	/// �Փ˂��Ȃ�
	/// </summary>
	async void HitDelay()
	{
		await UniTask.DelayFrame(1);
		//canHit = true;
	}

	/// <summary>
	/// X��Y�ő傫����r���āA���K��
	/// </summary>
	/// <param name="dir">��r�������</param>
	/// <returns>���K�����ꂽ�l</returns>
	Vector2 Direction(Vector2 dir)
	{
		return Mathf.Abs(dir.x) > Mathf.Abs(dir.y) ? new Vector2(dir.x, 0).normalized : new Vector2(0, dir.y).normalized;
	}

	/// <summary>
	/// �S�[��
	/// </summary>
	void Goal()
	{
		if (!isGoal)
		{
			isGoal = true;
			GM_Main.Instance.GetComponent<IGM_Main>().CharacterGoal(gameObject);
			animator.SetTrigger("Goal");
			//rb2d.simulated = false;
			//GetComponent<CapsuleCollider2D>().enabled = false;
		}
	}

	/// <summary>
	/// ���S
	/// </summary>
	public void Dead()
	{
		// ���S
		isDead = true;
		Player.player.GetComponent<IPlayer>().NonControl();
		GM_Main.Instance.GetComponent<IGM_Main>().GameOver();
		// ���S�A�j���[�V������G�t�F�N�g
		animator.SetTrigger("Dead");

		//Destroy(gameObject);
	}

	void ICharacter.ColorChange(CharacterStatus status)
	{
		gameObject.tag = status.tag;
		gameObject.layer = status.layer;
		spriteRenderer.sprite = status.sprite;
		spriteRenderer.color = status.color;
		animator.SetTrigger(status.characterColor.ToString());
	}

	int ICharacter.PickUpTheKey(int keyId)
	{
		keyIds.Add(keyId);
		return keyIds.Count;
	}

	void ICharacter.Start()
	{
		isStop = false;
		animator.speed = 1;
	}

	void ICharacter.Stop()
	{
		isStop = true;
		animator.speed = 0;
	}

	Vector2 ICharacter.GetSize()
	{
		return SpriteSize;
	}
}
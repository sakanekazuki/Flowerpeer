using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

// 移動するキャラクター
public class Character : MonoBehaviour, ICharacter
{
	// 移動速度
	[SerializeField]
	float speed;

	// 物理
	Rigidbody2D rb2d;

	// スプライト
	SpriteRenderer spriteRenderer;
	// 画像サイズ
	Vector2 spriteSize = Vector2.zero;
	public Vector2 SpriteSize
	{
		get => spriteSize;
	}

	// 所持している鍵のID
	static List<int> keyIds = new List<int>();

	// true = 死亡
	bool isDead = false;

	// true = キャラクター停止
	bool isStop = false;

	// true = キャラクターゴール
	bool isGoal = false;

	// 登ることのできる高さ
	[SerializeField]
	float climHeight = 0.2f;

	// アニメーター
	Animator animator;

	// true = 衝突できる
	//bool canHit = true;

	// 前回の位置
	Vector3 beforePosition = Vector3.zero;

	// その場で止まっていたフレーム数
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
		// 移動
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

		// ドアに当たった場合鍵で開くか確認
		if (collision.gameObject.CompareTag("Door"))
		{
			// ドアインターフェース取得
			var idoor = collision.gameObject.GetComponent<IDoor>();
			foreach (var id in keyIds)
			{
				if (idoor.Open(id))
				{
					return;
				}
			}
		}

		// 針に衝突した場合死亡
		if (collision.gameObject.CompareTag("Needle"))
		{
			Dead();
			return;
		}

		if (collision.contactCount <= 0)
		{
			return;
		}
		// 真下にあたっていた場合は判定を行わない
		if (collision.contacts[0].normal == Vector2.up)
		{
			return;
		}

		// 衝突相手の角度を調べる
		var angle = Vector2.SignedAngle(transform.right, collision.contacts[0].normal);
		// 衝突したオブジェクトが自分の向きから45度だった場合登る
		if (!(collision.contacts[0].normal.y > 0 && (180 - angle) >= GM_Main.climbableAngle))
		{
			// 反転
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
		//// 衝突相手の角度を調べる
		//var angle =	collision.gameObject.transform.localEulerAngles;
		//// 衝突したオブジェクトが自分の向きから45度だった場合登る
		//if (collision.contacts[0].normal.y > 0 && (angle.z <= GM_Main.climbableAngle || (180 - angle.z) <= GM_Main.climbableAngle))
		//{
		//	return;
		//}

		var dir = Direction(collision.contacts[0].normal);
		if (transform.right != new Vector3(dir.x, dir.y))
		{
			// 段差を超える
			if (collision.contacts[0].point.y > (transform.position + transform.up * climHeight).y)
			{
				// 反転
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
	/// 衝突しない
	/// </summary>
	async void HitDelay()
	{
		await UniTask.DelayFrame(1);
		//canHit = true;
	}

	/// <summary>
	/// XとYで大きさ比較して、正規化
	/// </summary>
	/// <param name="dir">比較する方向</param>
	/// <returns>正規化された値</returns>
	Vector2 Direction(Vector2 dir)
	{
		return Mathf.Abs(dir.x) > Mathf.Abs(dir.y) ? new Vector2(dir.x, 0).normalized : new Vector2(0, dir.y).normalized;
	}

	/// <summary>
	/// ゴール
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
	/// 死亡
	/// </summary>
	public void Dead()
	{
		// 死亡
		isDead = true;
		Player.player.GetComponent<IPlayer>().NonControl();
		GM_Main.Instance.GetComponent<IGM_Main>().GameOver();
		// 死亡アニメーションやエフェクト
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
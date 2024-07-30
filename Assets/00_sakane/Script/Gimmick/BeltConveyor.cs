using UnityEngine;

// ベルトコンベア
public class BeltConveyor : MonoBehaviour, IBeltConveyor
{
	// コンベアの移動方向
	[SerializeField]
	Vector3 direction = Vector3.right;
	public Vector3 Direction
	{
		get => direction;
	}

	// ベルトコンベアの回転速度
	[SerializeField]
	float speed;
	public float Speed
	{
		get => speed;
	}

	// 衝突しているキャラクターのリジッドボディ
	//List<Rigidbody2D> rigidbody2Ds = new List<Rigidbody2D>();

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// キャラクターが衝突した際にコンベアの移動方向に移動
		if (!collision.gameObject.CompareTag("Character"))
		{
			return;
		}
		// 方向が違う場合反転
		if (collision.transform.right != direction)
		{
			collision.transform.localEulerAngles += new Vector3(0, 180, 0);
			return;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Character"))
		{
			// 速度追加
			collision.rigidbody.AddForce(direction * speed);
		}
	}

	void IBeltConveyor.Reversal()
	{
		direction = -direction;
		foreach (var e in GetComponentsInChildren<IBeltConveyorRotate>())
		{
			e.Reversal();
		}
	}
}
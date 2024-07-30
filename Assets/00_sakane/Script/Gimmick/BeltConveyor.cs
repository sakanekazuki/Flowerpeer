using UnityEngine;

// �x���g�R���x�A
public class BeltConveyor : MonoBehaviour, IBeltConveyor
{
	// �R���x�A�̈ړ�����
	[SerializeField]
	Vector3 direction = Vector3.right;
	public Vector3 Direction
	{
		get => direction;
	}

	// �x���g�R���x�A�̉�]���x
	[SerializeField]
	float speed;
	public float Speed
	{
		get => speed;
	}

	// �Փ˂��Ă���L�����N�^�[�̃��W�b�h�{�f�B
	//List<Rigidbody2D> rigidbody2Ds = new List<Rigidbody2D>();

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// �L�����N�^�[���Փ˂����ۂɃR���x�A�̈ړ������Ɉړ�
		if (!collision.gameObject.CompareTag("Character"))
		{
			return;
		}
		// �������Ⴄ�ꍇ���]
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
			// ���x�ǉ�
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
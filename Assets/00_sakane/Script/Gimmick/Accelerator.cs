using UnityEngine;

// ����ʍs
public class Accelerator : MonoBehaviour
{
	// �ʂ�����
	[SerializeField]
	Vector3 direction = Vector3.right;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.gameObject.CompareTag("Character"))
		{
			return;
		}
		// �ʂ�Ȃ������̏ꍇ���]
		if (collider.transform.right == -direction)
		{
			collider.transform.localEulerAngles += new Vector3(0, 180, 0);
		}
	}
}
using UnityEngine;

// ˆê•û’Ês
public class Accelerator : MonoBehaviour
{
	// ’Ê‚ê‚é•ûŒü
	[SerializeField]
	Vector3 direction = Vector3.right;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.gameObject.CompareTag("Character"))
		{
			return;
		}
		// ’Ê‚ê‚È‚¢•ûŒü‚Ìê‡”½“]
		if (collider.transform.right == -direction)
		{
			collider.transform.localEulerAngles += new Vector3(0, 180, 0);
		}
	}
}
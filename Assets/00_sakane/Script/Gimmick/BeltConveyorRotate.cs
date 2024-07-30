using UnityEngine;

// �x���g�R���x�A�̉�]
public class BeltConveyorRotate : MonoBehaviour,IBeltConveyorRotate
{
	BeltConveyor beltConveyor;

	// ��]���x
	float speed;

	private void Awake()
	{
		beltConveyor = transform.parent.GetComponent<BeltConveyor>();
		speed = beltConveyor.Speed * -beltConveyor.Direction.x;
	}

	private void FixedUpdate()
	{
		transform.Rotate(0, 0, speed);
	}

	void IBeltConveyorRotate.Reversal()
	{
		speed = beltConveyor.Speed * -beltConveyor.Direction.x;
	}
}
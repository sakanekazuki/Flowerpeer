using UnityEngine;

// �X�C�b�`
public abstract class SwitchGimmickBase : GimmickBase
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Character"))
		{
			OnSwitch();
		}
	}

	// �X�C�b�`���������ۂ̃A�N�V����
	protected virtual void OnSwitch()
	{

	}
}
using System.Collections.Generic;
using UnityEngine;

// �A�N�e�B�u��Ԕ��]�̃X�C�b�`
public class DisappearSwitch : SwitchGimmickBase
{
	// ������I�u�W�F�N�g
	[SerializeField]
	List<GameObject> targetNeedle = new List<GameObject>();

	// true = �I�u�W�F�N�g���o�����Ă���
	bool isActive = true;

	Animator animator;

	// �Փ˂����ۂɖ炷SE
	[SerializeField]
	AudioClip hitSE;

	// true = �L�m�R�������Ă���
	bool isOn = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Character"))
		{
			isOn = false;
			animator.SetTrigger("Off");
		}
	}

	protected override void OnSwitch()
	{
		if (isOn)
		{
			return;
		}
		// �j�̏�Ԕ��]
		isActive = !isActive;
		foreach (var obj in targetNeedle)
		{
			obj.SetActive(isActive);
		}
		animator.SetTrigger("On");
		SoundManager.Instance.SEPlay(hitSE).Forget();
		isOn = true;
	}
}
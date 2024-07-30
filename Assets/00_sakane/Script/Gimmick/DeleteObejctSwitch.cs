using System.Collections.Generic;
using UnityEngine;

// �I�u�W�F�N�g�����Ĉȍ~�o�����Ȃ��Ȃ�X�C�b�`
public class DeleteObejctSwitch : SwitchGimmickBase
{
	// �o�����Ȃ��Ȃ�I�u�W�F�N�g
	[SerializeField]
	List<GameObject> targetObject = new List<GameObject>();

	Animator animator;

	// �Փˎ���SE
	[SerializeField]
	AudioClip hitSE;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	protected override void OnSwitch()
	{
		if (targetObject.Count != 0)
		{
			foreach (var obj in targetObject)
			{
				// �^�[�Q�b�g���폜
				Destroy(obj);
			}
			targetObject.Clear();
			animator.SetTrigger("On");
			SoundManager.Instance.SEPlay(hitSE).Forget();
		}
	}
}
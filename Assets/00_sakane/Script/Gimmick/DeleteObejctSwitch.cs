using System.Collections.Generic;
using UnityEngine;

// オブジェクトが壊れて以降出現しなくなるスイッチ
public class DeleteObejctSwitch : SwitchGimmickBase
{
	// 出現しなくなるオブジェクト
	[SerializeField]
	List<GameObject> targetObject = new List<GameObject>();

	Animator animator;

	// 衝突時のSE
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
				// ターゲットを削除
				Destroy(obj);
			}
			targetObject.Clear();
			animator.SetTrigger("On");
			SoundManager.Instance.SEPlay(hitSE).Forget();
		}
	}
}
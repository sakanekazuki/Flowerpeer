using System.Collections.Generic;
using UnityEngine;

// ���[�v�M�~�b�N
public class WarpGimmick : GimmickBase,IWarpGimmick
{
	// �s����
	[SerializeField]
	List<GameObject> warpHoles;

	// ���[�v����SE
	[SerializeField]
	AudioClip warpSound;

	void IWarpGimmick.Warp(GameObject hitObject, GameObject warpObject)
	{
		// ���[�v����I�u�W�F�N�g�̔ԍ��v�Z
		var number = (warpHoles.IndexOf(warpObject) + 1) % warpHoles.Count;
		// ���[�v��̃I�u�W�F�N�g�����[�v�o���Ȃ���Ԃɂ���
		warpHoles[number].GetComponent<IWarpHole>().IsWarped();
		// ���[�v��ɃI�u�W�F�N�g���ړ�
		hitObject.transform.position = warpHoles[number].transform.position + new Vector3(0, hitObject.GetComponent<ICharacter>().GetSize().y / 2);
		SoundManager.Instance.SEPlay(warpSound).Forget();
	}

	void IWarpGimmick.AddWarpHole(UnityEngine.GameObject warpHole)
	{
		warpHoles.Add(warpHole);
	}
}
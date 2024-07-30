using System.Collections.Generic;
using UnityEngine;

// ワープギミック
public class WarpGimmick : GimmickBase,IWarpGimmick
{
	// 行き先
	[SerializeField]
	List<GameObject> warpHoles;

	// ワープ時のSE
	[SerializeField]
	AudioClip warpSound;

	void IWarpGimmick.Warp(GameObject hitObject, GameObject warpObject)
	{
		// ワープするオブジェクトの番号計算
		var number = (warpHoles.IndexOf(warpObject) + 1) % warpHoles.Count;
		// ワープ先のオブジェクトをワープ出来ない状態にする
		warpHoles[number].GetComponent<IWarpHole>().IsWarped();
		// ワープ先にオブジェクトを移動
		hitObject.transform.position = warpHoles[number].transform.position + new Vector3(0, hitObject.GetComponent<ICharacter>().GetSize().y / 2);
		SoundManager.Instance.SEPlay(warpSound).Forget();
	}

	void IWarpGimmick.AddWarpHole(UnityEngine.GameObject warpHole)
	{
		warpHoles.Add(warpHole);
	}
}
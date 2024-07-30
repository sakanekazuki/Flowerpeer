using System.Collections.Generic;
using UnityEngine;

// クリック時のエフェクト管理クラス
public class ClickEffectManager : ManagerBase<ClickEffectManager>
{
	// 生成するエフェクト
	[SerializeField]
	GameObject clickEffectPrefab;

	// 生成したエフェクト
	List<GameObject> clickEffects = new List<GameObject>();
	public List<GameObject> ClickEffects
	{
		get => clickEffects;
	}

	/// <summary>
	/// エフェクト生成
	/// </summary>
	/// <param name="position">生成する位置</param>
	public void EffectCreate(Vector3 position)
	{
		clickEffects.Add(Instantiate(clickEffectPrefab, position, Quaternion.identity));
	}

	/// <summary>
	/// エフェクトを削除
	/// </summary>
	/// <param name="effect">削除するエフェクト</param>
	public void EffectRemove(GameObject effect)
	{
		clickEffects.Remove(effect);
	}
}
using UnityEngine;

// ワープギミックのインターフェース
public interface IWarpGimmick
{
	/// <summary>
	/// ワープ
	/// </summary>
	/// <param name="hitObject">当たったオブジェクト</param>
	/// <param name="warpObject">ワープさせるオブジェクト</param>
	void Warp(GameObject hitObject, GameObject warpObject);

	/// <summary>
	/// ワープホール追加
	/// </summary>
	/// <param name="warpHole">追加するワープホール</param>
	void AddWarpHole(GameObject warpHole);
}
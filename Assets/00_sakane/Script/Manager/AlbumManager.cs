using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// アルバム管理クラス
public class AlbumManager : ManagerBase<AlbumManager>
{
	// アルバムキャンバス
	[SerializeField]
	GameObject albumCanvasPrefab;

	// スチル画像
	[SerializeField]
	List<Sprite> stillSprites = new List<Sprite>();

	// スチルに入れるエフェクト
	[SerializeField]
	List<Sprite> stillEffects = new List<Sprite>();

	// エフェクト用マテリアル
	[SerializeField]
	List<Material> effectMaterials = new List<Material>();

	/// <summary>
	/// スチル画像取得
	/// </summary>
	/// <param name="stillNumber">取得する画像の番号</param>
	/// <returns>取得した画像</returns>
	public Sprite GetStillSprite(int stillNumber)
	{
		return stillSprites[stillNumber];
	}

	/// <summary>
	/// スチルのエフェクト取得
	/// </summary>
	/// <param name="stillNumber">取得するエフェクトの番号</param>
	/// <returns>エフェクト</returns>
	public Sprite GetStillEffect(int stillNumber)
	{
		return stillEffects[stillNumber];
	}

	/// <summary>
	/// スチルエフェクト用のマテリアル取得
	/// </summary>
	/// <param name="stillNumber">取得するマテリアルの番号</param>
	/// <returns>マテリアル</returns>
	public Material GetStillMaterial(int stillNumber)
	{
		return effectMaterials[stillNumber];
	}
}
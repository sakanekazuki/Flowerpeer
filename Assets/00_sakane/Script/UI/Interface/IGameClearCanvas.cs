using UnityEngine;

// ゲームクリアキャンバスのインターフェース
public interface IGameClearCanvas
{
	/// <summary>
	/// クリア時の画像設定
	/// </summary>
	/// <param name="sprite">設定する画像</param>
	void SetGameClearImage(Sprite sprite);

	/// <summary>
	/// クリア時のエフェクト設定
	/// </summary>
	/// <param name="sprite">設定する画像</param>
	void SetGameClearEffect(Sprite sprite);

	/// <summary>
	/// エフェクトのマテリアル設定
	/// </summary>
	/// <param name="material">設定するマテリアル</param>
	void SetGameClearEffectMaterial(Material material);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="isActive"></param>
	void AddAlbumMessgeActive(bool isActive);
}
// キャラクターのインターフェース
using UnityEngine;

public interface ICharacter
{
	/// <summary>
	/// 色変更
	/// </summary>
	/// <param name="status">変更する色のステータス</param>
	void ColorChange(CharacterStatus status);

	/// <summary>
	/// 鍵を拾う
	/// </summary>
	/// <param name="keyId">拾った鍵のID</param>
	int PickUpTheKey(int keyId);

	/// <summary>
	/// キャラクター移動開始
	/// </summary>
	void Start();

	/// <summary>
	/// キャラクター停止
	/// </summary>
	void Stop();

	/// <summary>
	/// キャラクターの大きさ取得
	/// </summary>
	/// <returns>キャラクターの大きさ</returns>
	Vector2 GetSize();
}
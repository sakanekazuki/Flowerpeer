using System.Collections.Generic;
using UnityEngine;

// キャラクター生成クラス
public class CharacterFactory : FactoryBase<CharacterFactory>
{
	// キャラクターの初期ステータス
	[SerializeField]
	SO_CharacterStatus charInitStatus;

	/// <summary>
	/// キャラクター生成
	/// </summary>
	/// <param name="positions">生成する位置</param>
	/// <returns>生成したキャラクター</returns>
	public List<GameObject> Create(List<Vector3> positions)
	{
		var characters = new List<GameObject>();

		// キャラクター生成
		foreach(var pos in positions)
		{
			var character = base.Create(pos);
			characters.Add(character);
			CharacterManager.Instance.AddCharacter(character);
		}

		// キャラクターの初期設定
		foreach (var character in characters)
		{
			// キャラクターの番号
			var number = characters.IndexOf(character);
			// タグ設定
			character.tag = charInitStatus.statuses[number].tag;
			// レイヤー設定
			character.layer = charInitStatus.statuses[number].layer;

			var spriteRenderer = character.GetComponent<SpriteRenderer>();

			spriteRenderer.sprite = charInitStatus.statuses[number].sprite;
			// スプライトの設定
			spriteRenderer.color = charInitStatus.statuses[number].color;

			if(character.layer!=9)
			{
				// アニメーションを色に対応したアニメーションに切り替え
				character.GetComponent<Animator>().SetTrigger(charInitStatus.statuses[number].characterColor.ToString());
			}
		}

		return characters;
	}
}
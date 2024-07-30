using System.Collections.Generic;
using UnityEngine;

// キャラクター管理クラス
public class CharacterManager : ManagerBase<CharacterManager>
{
	// キャラクターのステータス
	[SerializeField]
	SO_CharacterStatus status;

	// 管理するキャラクター
	List<GameObject> characters = new List<GameObject>();
	public List<GameObject> Characters
	{
		get => characters;
	}

	/// <summary>
	/// キャラクター削除
	/// </summary>
	public void CharacterClear()
	{
		characters.Clear();
	}

	/// <summary>
	/// キャラクター追加
	/// </summary>
	/// <param name="character">追加するキャラクター</param>
	public void AddCharacter(GameObject character)
	{
		characters.Add(character);
	}

	/// <summary>
	/// キャラクターの色変更
	/// </summary>
	public void AllCharacterColorChange()
	{
		foreach (var character in characters)
		{
			// 違う色のステータスに変更
			var s = status.statuses[0];
			foreach (var e in status.statuses)
			{
				if (e.layer != character.gameObject.layer)
				{
					s = e;
					break;
				}
			}

			// 色変更
			character.GetComponent<ICharacter>().ColorChange(s);
		}
	}
}
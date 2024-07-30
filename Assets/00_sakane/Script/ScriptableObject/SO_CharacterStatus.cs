using System.Collections.Generic;
using UnityEngine;

// キャラクターのスクリプタブルオブジェクト
[CreateAssetMenu(fileName = "CharacterStatus", menuName = "ScriptableObject/CharacterStatus")]
public class SO_CharacterStatus : ScriptableObject
{
	public List<CharacterStatus> statuses = new List<CharacterStatus>();
}
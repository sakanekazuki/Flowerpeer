using System.Collections.Generic;
using UnityEngine;

// �L�����N�^�[�̃X�N���v�^�u���I�u�W�F�N�g
[CreateAssetMenu(fileName = "CharacterStatus", menuName = "ScriptableObject/CharacterStatus")]
public class SO_CharacterStatus : ScriptableObject
{
	public List<CharacterStatus> statuses = new List<CharacterStatus>();
}
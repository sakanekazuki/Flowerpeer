using System.Collections.Generic;
using UnityEngine;

// �L�����N�^�[�Ǘ��N���X
public class CharacterManager : ManagerBase<CharacterManager>
{
	// �L�����N�^�[�̃X�e�[�^�X
	[SerializeField]
	SO_CharacterStatus status;

	// �Ǘ�����L�����N�^�[
	List<GameObject> characters = new List<GameObject>();
	public List<GameObject> Characters
	{
		get => characters;
	}

	/// <summary>
	/// �L�����N�^�[�폜
	/// </summary>
	public void CharacterClear()
	{
		characters.Clear();
	}

	/// <summary>
	/// �L�����N�^�[�ǉ�
	/// </summary>
	/// <param name="character">�ǉ�����L�����N�^�[</param>
	public void AddCharacter(GameObject character)
	{
		characters.Add(character);
	}

	/// <summary>
	/// �L�����N�^�[�̐F�ύX
	/// </summary>
	public void AllCharacterColorChange()
	{
		foreach (var character in characters)
		{
			// �Ⴄ�F�̃X�e�[�^�X�ɕύX
			var s = status.statuses[0];
			foreach (var e in status.statuses)
			{
				if (e.layer != character.gameObject.layer)
				{
					s = e;
					break;
				}
			}

			// �F�ύX
			character.GetComponent<ICharacter>().ColorChange(s);
		}
	}
}
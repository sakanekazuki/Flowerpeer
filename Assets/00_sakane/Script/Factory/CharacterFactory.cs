using System.Collections.Generic;
using UnityEngine;

// �L�����N�^�[�����N���X
public class CharacterFactory : FactoryBase<CharacterFactory>
{
	// �L�����N�^�[�̏����X�e�[�^�X
	[SerializeField]
	SO_CharacterStatus charInitStatus;

	/// <summary>
	/// �L�����N�^�[����
	/// </summary>
	/// <param name="positions">��������ʒu</param>
	/// <returns>���������L�����N�^�[</returns>
	public List<GameObject> Create(List<Vector3> positions)
	{
		var characters = new List<GameObject>();

		// �L�����N�^�[����
		foreach(var pos in positions)
		{
			var character = base.Create(pos);
			characters.Add(character);
			CharacterManager.Instance.AddCharacter(character);
		}

		// �L�����N�^�[�̏����ݒ�
		foreach (var character in characters)
		{
			// �L�����N�^�[�̔ԍ�
			var number = characters.IndexOf(character);
			// �^�O�ݒ�
			character.tag = charInitStatus.statuses[number].tag;
			// ���C���[�ݒ�
			character.layer = charInitStatus.statuses[number].layer;

			var spriteRenderer = character.GetComponent<SpriteRenderer>();

			spriteRenderer.sprite = charInitStatus.statuses[number].sprite;
			// �X�v���C�g�̐ݒ�
			spriteRenderer.color = charInitStatus.statuses[number].color;

			if(character.layer!=9)
			{
				// �A�j���[�V������F�ɑΉ������A�j���[�V�����ɐ؂�ւ�
				character.GetComponent<Animator>().SetTrigger(charInitStatus.statuses[number].characterColor.ToString());
			}
		}

		return characters;
	}
}
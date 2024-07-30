using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// �A���o���Ǘ��N���X
public class AlbumManager : ManagerBase<AlbumManager>
{
	// �A���o���L�����o�X
	[SerializeField]
	GameObject albumCanvasPrefab;

	// �X�`���摜
	[SerializeField]
	List<Sprite> stillSprites = new List<Sprite>();

	// �X�`���ɓ����G�t�F�N�g
	[SerializeField]
	List<Sprite> stillEffects = new List<Sprite>();

	// �G�t�F�N�g�p�}�e���A��
	[SerializeField]
	List<Material> effectMaterials = new List<Material>();

	/// <summary>
	/// �X�`���摜�擾
	/// </summary>
	/// <param name="stillNumber">�擾����摜�̔ԍ�</param>
	/// <returns>�擾�����摜</returns>
	public Sprite GetStillSprite(int stillNumber)
	{
		return stillSprites[stillNumber];
	}

	/// <summary>
	/// �X�`���̃G�t�F�N�g�擾
	/// </summary>
	/// <param name="stillNumber">�擾����G�t�F�N�g�̔ԍ�</param>
	/// <returns>�G�t�F�N�g</returns>
	public Sprite GetStillEffect(int stillNumber)
	{
		return stillEffects[stillNumber];
	}

	/// <summary>
	/// �X�`���G�t�F�N�g�p�̃}�e���A���擾
	/// </summary>
	/// <param name="stillNumber">�擾����}�e���A���̔ԍ�</param>
	/// <returns>�}�e���A��</returns>
	public Material GetStillMaterial(int stillNumber)
	{
		return effectMaterials[stillNumber];
	}
}
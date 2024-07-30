using System.Collections.Generic;
using UnityEngine;

// �N���b�N���̃G�t�F�N�g�Ǘ��N���X
public class ClickEffectManager : ManagerBase<ClickEffectManager>
{
	// ��������G�t�F�N�g
	[SerializeField]
	GameObject clickEffectPrefab;

	// ���������G�t�F�N�g
	List<GameObject> clickEffects = new List<GameObject>();
	public List<GameObject> ClickEffects
	{
		get => clickEffects;
	}

	/// <summary>
	/// �G�t�F�N�g����
	/// </summary>
	/// <param name="position">��������ʒu</param>
	public void EffectCreate(Vector3 position)
	{
		clickEffects.Add(Instantiate(clickEffectPrefab, position, Quaternion.identity));
	}

	/// <summary>
	/// �G�t�F�N�g���폜
	/// </summary>
	/// <param name="effect">�폜����G�t�F�N�g</param>
	public void EffectRemove(GameObject effect)
	{
		clickEffects.Remove(effect);
	}
}
using UnityEngine;

// ���[�v�M�~�b�N�̃C���^�[�t�F�[�X
public interface IWarpGimmick
{
	/// <summary>
	/// ���[�v
	/// </summary>
	/// <param name="hitObject">���������I�u�W�F�N�g</param>
	/// <param name="warpObject">���[�v������I�u�W�F�N�g</param>
	void Warp(GameObject hitObject, GameObject warpObject);

	/// <summary>
	/// ���[�v�z�[���ǉ�
	/// </summary>
	/// <param name="warpHole">�ǉ����郏�[�v�z�[��</param>
	void AddWarpHole(GameObject warpHole);
}
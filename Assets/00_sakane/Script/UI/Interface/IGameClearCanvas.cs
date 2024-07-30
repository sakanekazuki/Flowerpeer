using UnityEngine;

// �Q�[���N���A�L�����o�X�̃C���^�[�t�F�[�X
public interface IGameClearCanvas
{
	/// <summary>
	/// �N���A���̉摜�ݒ�
	/// </summary>
	/// <param name="sprite">�ݒ肷��摜</param>
	void SetGameClearImage(Sprite sprite);

	/// <summary>
	/// �N���A���̃G�t�F�N�g�ݒ�
	/// </summary>
	/// <param name="sprite">�ݒ肷��摜</param>
	void SetGameClearEffect(Sprite sprite);

	/// <summary>
	/// �G�t�F�N�g�̃}�e���A���ݒ�
	/// </summary>
	/// <param name="material">�ݒ肷��}�e���A��</param>
	void SetGameClearEffectMaterial(Material material);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="isActive"></param>
	void AddAlbumMessgeActive(bool isActive);
}
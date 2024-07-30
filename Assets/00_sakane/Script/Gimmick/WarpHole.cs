using UnityEngine;

// ���[�v�z�[��
public class WarpHole : MonoBehaviour,IWarpHole
{
	// true = ���[�v�ł���
	bool canWarp = true;

	// ���[�v�M�~�b�N
	IWarpGimmick iwarpGimmick;

	private void Start()
	{
		// ���[�v�M�~�b�N�擾
		iwarpGimmick = transform.parent.GetComponent<IWarpGimmick>();
		// ���[�v�z�[���ݒ�
		iwarpGimmick.AddWarpHole(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �L�����N�^�[�ɓ����ă��[�v�ł����Ԃł���΃��[�v
		if (collision.tag == "Character" && canWarp)
		{
			iwarpGimmick.Warp(collision.gameObject, gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// �L�����N�^�[�����[�v�]�[�����痣�ꂽ�ꍇ���[�v�ł����Ԃɖ߂�
		if (collision.tag == "Character")
		{
			canWarp = true;
		}
	}

	void IWarpHole.IsWarped()
	{
		canWarp = false;
	}
}
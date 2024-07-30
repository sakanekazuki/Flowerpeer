using UnityEngine;

// ���t���h�A
public class DoorWithLock : GimmickBase, IDoor
{
	// �h�A��id
	[SerializeField]
	int id = 0;
	// �h�A���J�����ۂ�SE
	[SerializeField]
	AudioClip openSE;

	bool IDoor.Open(int keyValue)
	{
		if (id == keyValue)
		{
			// �J������

			// ���͏���
			Destroy(gameObject);
			SoundManager.Instance.SEPlay(openSE).Forget();

			return true;
		}
		return false;
	}
}
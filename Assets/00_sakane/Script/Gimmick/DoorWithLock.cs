using UnityEngine;

// 鍵付きドア
public class DoorWithLock : GimmickBase, IDoor
{
	// ドアのid
	[SerializeField]
	int id = 0;
	// ドアが開いた際のSE
	[SerializeField]
	AudioClip openSE;

	bool IDoor.Open(int keyValue)
	{
		if (id == keyValue)
		{
			// 開く処理

			// 今は消す
			Destroy(gameObject);
			SoundManager.Instance.SEPlay(openSE).Forget();

			return true;
		}
		return false;
	}
}
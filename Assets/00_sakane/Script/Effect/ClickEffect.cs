using UnityEngine;

// �N���b�N���̃G�t�F�N�g
public class ClickEffect : MonoBehaviour
{
	private void OnDestroy()
	{
		ClickEffectManager.Instance.EffectRemove(gameObject);
	}
}
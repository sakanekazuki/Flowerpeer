using UnityEngine;

// クリック時のエフェクト
public class ClickEffect : MonoBehaviour
{
	private void OnDestroy()
	{
		ClickEffectManager.Instance.EffectRemove(gameObject);
	}
}
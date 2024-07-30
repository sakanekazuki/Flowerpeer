using UnityEngine;
using UnityEngine.UI;

// ゲームクリア時のキャンバス
public class GameClearCanvas : MonoBehaviour, IGameClearCanvas
{
	// クリア時に表示する画像
	[SerializeField]
	Image clearImage;

	// エフェクト
	[SerializeField]
	Image effectImage;

	[SerializeField]
	GameObject addAlbumMessage;

	float unScaledTime = 0;

	private void Awake()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	private void Update()
	{
		effectImage?.material.SetFloat("_UnScaledTime", unScaledTime);
		unScaledTime += Time.unscaledDeltaTime;
	}

	public void StillClose()
	{
		gameObject.SetActive(false);
	}

	void IGameClearCanvas.SetGameClearImage(UnityEngine.Sprite sprite)
	{
		clearImage.sprite = sprite;
	}

	void IGameClearCanvas.SetGameClearEffect(UnityEngine.Sprite sprite)
	{
		effectImage.sprite = sprite;
	}

	void IGameClearCanvas.SetGameClearEffectMaterial(UnityEngine.Material material)
	{
		effectImage.material = material;
	}

	void IGameClearCanvas.AddAlbumMessgeActive(bool isActive)
	{
		addAlbumMessage.SetActive(isActive);
	}
}
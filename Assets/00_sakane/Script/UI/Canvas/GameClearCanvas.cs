using UnityEngine;
using UnityEngine.UI;

// �Q�[���N���A���̃L�����o�X
public class GameClearCanvas : MonoBehaviour, IGameClearCanvas
{
	// �N���A���ɕ\������摜
	[SerializeField]
	Image clearImage;

	// �G�t�F�N�g
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
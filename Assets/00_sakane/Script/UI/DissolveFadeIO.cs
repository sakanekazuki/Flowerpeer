using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// �f�B�]���u�V�F�[�_�[�ł̃t�F�[�h
public class DissolveFadeIO : Singleton<DissolveFadeIO>
{
	// �t�F�[�h����C���[�W
	Image fadeImg;
	// �t�F�[�h�̒l��ύX����}�e���A��
	Material fadeMaterial;

	// true = �t�F�[�h��
	public bool IsFading
	{
		get;
		private set;
	}

	// �t�F�[�h�X�s�[�h
	[SerializeField]
	float fadeSpeed = 0.08f;

	// �t�F�[�h�̒l
	float fadeValue = 0;

	private void Awake()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;

		// �q�I�u�W�F�N�g����Image�N���X���擾
		fadeImg = GetComponentInChildren<Image>();
		fadeMaterial = fadeImg.material;
	}

	/// <summary>
	/// �t�F�[�h�C��
	/// </summary>
	public async UniTask<bool> FadeIn()
	{
		CancellationToken token = this.GetCancellationTokenOnDestroy();
		// �t�F�[�h��Ԃɂ���
		IsFading = true;
		fadeImg.raycastTarget = true;
		fadeValue = 0;

		// �F���Ȃ��Ȃ�܂Ń��[�v
		while (fadeValue < 1)
		{
			fadeValue += fadeSpeed;
			// �A���t�@�𔲂�
			fadeMaterial.SetFloat("_DizolbValue", fadeValue);
			// 1�t���[���҂�
			await UniTask.DelayFrame(1, cancellationToken: token);
		}

		// �t�F�[�h�I��
		IsFading = false;
		fadeImg.raycastTarget = false;

		return true;
	}

	/// <summary>
	/// �t�F�[�h�A�E�g
	/// </summary>
	public async UniTask<bool> FadeOut()
	{
		CancellationToken token = this.GetCancellationTokenOnDestroy();
		fadeImg.raycastTarget = true;
		// �t�F�[�h��Ԃɂ���
		IsFading = true;
		fadeValue = 1;

		// �F���Ȃ��Ȃ�܂Ń��[�v
		while (fadeValue > 0)
		{
			fadeValue -= fadeSpeed;
			// �A���t�@�𔲂�
			fadeMaterial.SetFloat("_DizolbValue", fadeValue);

			// 1�t���[���҂�
			await UniTask.DelayFrame(1, cancellationToken: token);
		}

		// �t�F�[�h�I��
		IsFading = false;
		fadeImg.raycastTarget = false;

		return true;
	}
}
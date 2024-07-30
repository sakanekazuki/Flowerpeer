using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

// �t�F�[�h�C���E�t�F�[�h�A�E�g�Ǘ��N���X
public class FadeIO : Singleton<FadeIO>
{
	// �t�F�[�h�����邽�߂�Image
	Image fadeImg;

	// true = �t�F�[�h��
	public bool IsFading
	{
		get;
		private set;
	}

	// �t�F�[�h�X�s�[�h
	[SerializeField]
	float fadeSpeed = 0.08f;

	private void Awake()
	{
		// �q�I�u�W�F�N�g����Image�N���X���擾
		fadeImg = GetComponentInChildren<Image>();
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
		fadeImg.color = Color.black;

		// �F���Ȃ��Ȃ�܂Ń��[�v
		while (fadeImg.color.a > 0.1f)
		{
			// �A���t�@�𔲂�
			fadeImg.color -= new Color(0, 0, 0, fadeSpeed);
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
		fadeImg.color = new Color(0, 0, 0, 0);

		// �F���Ȃ��Ȃ�܂Ń��[�v
		while (fadeImg.color.a < 0.9f)
		{
			// �A���t�@�𔲂�
			fadeImg.color += new Color(0, 0, 0, fadeSpeed);
			// 1�t���[���҂�
			await UniTask.DelayFrame(1, cancellationToken: token);
		}

		// �t�F�[�h�I��
		IsFading = false;
		fadeImg.raycastTarget = false;

		return true;
	}
}
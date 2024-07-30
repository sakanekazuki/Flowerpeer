using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

// �X�g�[���[��\������N���X
public class Story : MonoBehaviour
{
	// �e�L�X�g
	[SerializeField]
	TMP_Text storyText;

	// �X�g�[���[�̓��e
	[SerializeField, Multiline]
	List<string> storyContents = new List<string>();

	// ���x
	[SerializeField]
	float storySpeed;

	// �Ō�̕�����\�����Ă��鎞��
	[SerializeField]
	float lastTime;

	// ���s�����ۂɎ~�܂鎞��
	[SerializeField]
	float newLIneTime = 0.2f;

	// �X�g�[���[�̔ԍ�
	int storyNumber = 0;

	// �L�����Z���g�[�N���\�[�X
	CancellationTokenSource tokenSouce;
	CancellationToken token;

	// �Ō�̃X�g�[���[��\�����鎞��
	[SerializeField]
	float lastStoryDisplayTime;

	private void Awake()
	{
		storyText.text = storyContents[storyNumber];
		storyText.maxVisibleCharacters = storyNumber;
	}

	private void OnEnable()
	{
		tokenSouce = new CancellationTokenSource();
		token = tokenSouce.Token;
		StoryDisplay(token).Forget();
	}

	private void OnDisable()
	{
		tokenSouce.Cancel();
	}

	private void OnDestroy()
	{
		tokenSouce.Cancel();
	}

	/// <summary>
	/// �X�g�[���[�\��
	/// </summary>
	/// <param name="token">�L�����Z���g�[�N��</param>
	async UniTaskVoid StoryDisplay(CancellationToken token)
	{
		while (true)
		{
			await CharacterFeed(token);
			++storyNumber;
			storyNumber = storyNumber % storyContents.Count;
			if (storyNumber == 0)
			{
				await UniTask.Delay(System.TimeSpan.FromSeconds(lastStoryDisplayTime), cancellationToken: token);
				// �^�C�g�����S�\��
				transform.root.GetComponent<ITitleCanvas>().LogoDisplay();
			}
		}
	}

	/// <summary>
	/// ��������
	/// </summary>
	/// <param name="token">�L�����Z���g�[�N��</param>
	async UniTask<bool> CharacterFeed(CancellationToken token)
	{
		// �\�����镶����
		int charaValue = 1;
		// �X�g�[�����
		storyText.text = storyContents[storyNumber];
		// �������\�����镶���𒴂���܂Ń��[�v
		while (charaValue < storyContents[storyNumber].Count() + 1)
		{
			// �������ݒ�
			storyText.maxVisibleCharacters = charaValue;
			if (storyText.text[charaValue - 1].ToString() == "\n")
			{
				// ���s�����ۂɏ����~�܂�
				await UniTask.Delay(System.TimeSpan.FromSeconds(newLIneTime), cancellationToken: token);
			}
			await UniTask.Delay(System.TimeSpan.FromSeconds(storySpeed), cancellationToken: token);
			// �\�����镶���ǉ�
			++charaValue;
		}
		// �Ō�̕����𐔕b�\��
		await UniTask.Delay(System.TimeSpan.FromSeconds(lastTime), cancellationToken: token);
		return true;
	}
}
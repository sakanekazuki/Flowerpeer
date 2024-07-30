using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

// �^�C�g���L�����o�X
public class TitleCanvas : MonoBehaviour, ITitleCanvas
{
	//// �N���b�N���ɐ���
	//[SerializeField]
	//GameObject clickEffect;

	// �^�C�g�����S
	[SerializeField]
	GameObject logoObject;

	// �X�g�[���[��\������܂ł̎���
	[SerializeField]
	float storyDisplayTime = 5;

	// �X�g�[���[
	[SerializeField]
	GameObject storyText;

	// �{�^���𖳎�������{�^��
	[SerializeField]
	GameObject buttonIgnoreImage;

	CancellationTokenSource tokenSource;
	CancellationToken token;

	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	private void OnEnable()
	{
		storyText.SetActive(false);
		buttonIgnoreImage.SetActive(false);
		logoObject.SetActive(true);
		tokenSource = new CancellationTokenSource();
		token = tokenSource.Token;
		StoryTextDisplay(token).Forget();
	}

	private void OnDisable()
	{
		tokenSource.Cancel();
	}

	async UniTaskVoid StoryTextDisplay(CancellationToken token)
	{
		await UniTask.Delay(System.TimeSpan.FromSeconds(storyDisplayTime), cancellationToken: token);
		logoObject.SetActive(false);
		storyText.SetActive(true);
	}

	/// <summary>
	/// �X�e�[�W�I����ʂɈړ�
	/// </summary>
	public void ToStageSelect()
	{
		buttonIgnoreImage.SetActive(true);
		GM_Title.Instance.GetComponent<IGM_Title>().ToStageSelect();
	}

	/// <summary>
	/// �I�v�V������ʂɈړ�
	/// </summary>
	public void ToOption()
	{
		buttonIgnoreImage.SetActive(true);
		GM_Title.Instance.GetComponent<IGM_Title>().ToOption();
	}

	/// <summary>
	/// �Q�[���I��
	/// </summary>
	public void GameQuit()
	{
		buttonIgnoreImage.SetActive(true);
		LevelManager.GameQuit();
	}

	/// <summary>
	/// SE�Đ�
	/// </summary>
	/// <param name="clip">�Đ����鉹</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	void ITitleCanvas.LogoDisplay()
	{
		logoObject.SetActive(true);
		storyText.SetActive(false);
		StoryTextDisplay(token).Forget();
	}
}
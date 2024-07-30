using Cysharp.Threading.Tasks;
using UnityEngine;

// �^�C�g���̊Ǘ��N���X
public class GM_Title : GameManagerBase, IGM_Title
{
	// �^�C�g���L�����o�X
	[SerializeField]
	GameObject titleCanvasPrefab;
	GameObject titleCanvas;

	// �X�e�[�W�I���L�����o�X
	[SerializeField]
	GameObject stageSelectCanvasPrefab;
	GameObject stageSelectCanvas;

	// �I�v�V�����L�����o�X
	[SerializeField]
	GameObject optionCanvasPrefab;
	GameObject optionCanvas;

	// �O��̏��
	GameInstance.GameState beforeGameState = GameInstance.GameState.Title;

	protected override void Start()
	{
		Time.timeScale = 1.0f;
		base.Start();

		// ����
		titleCanvas = Instantiate(titleCanvasPrefab);
		stageSelectCanvas = Instantiate(stageSelectCanvasPrefab);
		optionCanvas = Instantiate(optionCanvasPrefab);
		titleCanvas.SetActive(false);
		stageSelectCanvas.SetActive(false);
		optionCanvas.SetActive(false);
		beforeGameState = GameInstance.gameState;

		// ���݂̃L�����o�X��\��
		switch (GameInstance.gameState)
		{
		case GameInstance.GameState.Title:
			titleCanvas.SetActive(true);
			break;
		case GameInstance.GameState.StageSelect:
			stageSelectCanvas.SetActive(true);
			break;
		case GameInstance.GameState.Option:
			optionCanvas.SetActive(true);
			break;
		}
		FadeIO.Instance.FadeIn().Forget();
	}

	/// <summary>
	/// �X�e�[�W�I����ʂɈړ�
	/// </summary>
	async void ToStageSelect()
	{
		await UniTask.WaitUntil(() => ClickEffectManager.Instance.ClickEffects.Count <= 0);
		await FadeIO.Instance.FadeOut();
		stageSelectCanvas.SetActive(true);
		titleCanvas.SetActive(false);
		optionCanvas.SetActive(false);
		beforeGameState = GameInstance.gameState;
		GameInstance.gameState = GameInstance.GameState.StageSelect;
		await FadeIO.Instance.FadeIn();
	}

	/// <summary>
	/// �^�C�g����ʂɈړ�
	/// </summary>
	async void ToTitle()
	{
		await UniTask.WaitUntil(() => ClickEffectManager.Instance.ClickEffects.Count <= 0);
		await FadeIO.Instance.FadeOut();
		titleCanvas.SetActive(true);
		stageSelectCanvas.SetActive(false);
		optionCanvas.SetActive(false);
		beforeGameState = GameInstance.gameState;
		GameInstance.gameState = GameInstance.GameState.Title;
		await FadeIO.Instance.FadeIn();
	}

	/// <summary>
	/// �I�v�V������ʂɈړ�
	/// </summary>
	async void ToOption()
	{
		await UniTask.WaitUntil(() => ClickEffectManager.Instance.ClickEffects.Count <= 0);
		optionCanvas.SetActive(true);
		titleCanvas.SetActive(false);
		stageSelectCanvas.SetActive(false);
		beforeGameState = GameInstance.gameState;
		GameInstance.gameState = GameInstance.GameState.Option;
	}

	/// <summary>
	/// �߂�
	/// </summary>
	public void Return()
	{
		switch (beforeGameState)
		{
		case GameInstance.GameState.Title:
			ToTitle();
			break;
		case GameInstance.GameState.StageSelect:
			ToStageSelect();
			break;
		case GameInstance.GameState.Option:
			ToOption();
			break;
		}
	}

	void IGM_Title.ToStageSelect()
	{
		ToStageSelect();
	}

	void IGM_Title.ToTitle()
	{
		ToTitle();
	}

	void IGM_Title.ToOption()
	{
		ToOption();
	}

	void IGM_Title.Return()
	{
		Return();
	}
}
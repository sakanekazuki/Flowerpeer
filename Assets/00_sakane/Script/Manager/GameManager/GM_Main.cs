using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

// ���C���̃Q�[���}�l�[�W���[
public class GM_Main : GameManagerBase, IGM_Main
{
	public enum GameProgressSpeed
	{
		Stop = 0,
		Default = 1,
		Twice = 2,
	}

	// ���C���̃L�����o�X
	[SerializeField]
	GameObject mainGameCanvasPrefab;
	GameObject mainGameCanvas;

	// �Q�[���N���A���̃L�����o�X
	[SerializeField]
	GameObject gameClearCanvasPrefab;
	GameObject gameClearCanvas;

	// �X�e�[�W�N���A���̑I���L�����o�X
	[SerializeField]
	GameObject nextStageORstageSelectCanvasPrefab;
	GameObject nextStageORstageSelectCanvas;

	// �Q�[���I�[�o�[���̑I���L�����o�X
	[SerializeField]
	GameObject gameOverSelectCanvasPrefab;
	GameObject gameOverSelectCanvas;

	// ���C�����j���[�L�����o�X
	[SerializeField]
	GameObject mainMenuCanvasPrefab;
	GameObject mainMenuCanvas;

	// �I�v�V�����L�����o�X
	[SerializeField]
	GameObject optionCanvasPrefab;
	GameObject optionCanvas;

	// �X�e�[�W�J�n�L�����o�X
	[SerializeField]
	GameObject stageStartCanvasPrefab;
	GameObject stageStartCanvas;

	// �Q�[���N���A���̃L�����o�X
	[SerializeField]
	GameObject gameClearResultCanvasPrefab;
	GameObject gameClearResultCanvas;

	// �Q�[���J�n���̃J�E���g�_�E���̎���
	[SerializeField]
	float startCountDownTime;

	// �Q�[�����x
	float gameSpeed = 1;
	public float GameSpeed
	{
		get => gameSpeed;
		set
		{
			gameSpeed = value;
			Time.timeScale = gameSpeed;
		}
	}

	// ���������L�����N�^�[
	public List<GameObject> Characters
	{
		get;
		private set;
	}

	// �o�邱�Ƃ̂ł���p�x
	public static readonly float climbableAngle = 45;

	// �S�[�������L�����N�^�[
	List<GameObject> goalCharacters = new List<GameObject>();

	// �X�e�[�W�ԍ�
	[HideInInspector]
	public static int stageNumber = 1;

	// ���������ۂɂ������ɂȂ�傫��
	public static float slowScale = 0.0f;

	// true = ������
	bool isFastForward = false;
	// �����莞�̑��x
	[SerializeField]
	float fastForwardSpeed = 2.0f;

	// �Q�[���̐i�s���x
	GameProgressSpeed gameProgressSpeed = GameProgressSpeed.Default;

	// �o�ߎ���
	float elapsedTime = 0;

	// �Q�[����
	public static bool isPlaying = false;

	// �X�`�����ω�����~�b�V�����̃N���A��
	[SerializeField]
	List<int> stillChangeMissionClearValue = new List<int>();

	// �X�e�[�W�N���A���̑ҋ@����
	[SerializeField]
	float stageClearDelayTime = 2.0f;

	// �X�e�[�W�N���A���ɗ�����
	[SerializeField]
	AudioClip stageClearAudio;

	[SerializeField]
	AudioClip gameOverAudio;

	protected override void Start()
	{
		GameInstance.gameState = GameInstance.GameState.MainGame;

		// UI����
		gameClearCanvas = Instantiate(gameClearCanvasPrefab);
		mainGameCanvas = Instantiate(mainGameCanvasPrefab);
		nextStageORstageSelectCanvas = Instantiate(nextStageORstageSelectCanvasPrefab);
		gameOverSelectCanvas = Instantiate(gameOverSelectCanvasPrefab);
		optionCanvas = Instantiate(optionCanvasPrefab);
		mainMenuCanvas = Instantiate(mainMenuCanvasPrefab);
		stageStartCanvas = Instantiate(stageStartCanvasPrefab);
		gameClearResultCanvas = Instantiate(gameClearResultCanvasPrefab);

		Init().Forget();
	}

	protected override void Update()
	{
		elapsedTime += Time.deltaTime;

		//if(Keyboard.current.escapeKey.isPressed)
		//{
		//	GameClear();
		//}
		//if (Keyboard.current.escapeKey.isPressed)
		//{
		//	MainMenu();
		//}
	}

	/// <summary>
	/// ������
	/// </summary>
	async UniTaskVoid Init()
	{
		var token = this.GetCancellationTokenOnDestroy();
		Time.timeScale = 1;

		gameClearCanvas.SetActive(false);
		mainGameCanvas.SetActive(true);
		nextStageORstageSelectCanvas.SetActive(false);
		gameOverSelectCanvas.SetActive(false);
		optionCanvas.SetActive(false);
		mainMenuCanvas.SetActive(false);
		stageStartCanvas.SetActive(false);
		gameClearResultCanvas.SetActive(false);

		mainGameCanvas.GetComponentInChildren<IFastForwardButton>().SetUVMove(false);
		mainGameCanvas.GetComponent<IMainCanvas>().SetStartMessageActive(true);

		stageStartCanvas.SetActive(true);

		// �`���[�g���A���\��
		if (stageNumber == 1 && !SaveManager.data.isStage1TutorialFirst)
		{
			TutorialManager.Instance.Stage1TutorialEnable();
		}
		else if (stageNumber == 10 && !SaveManager.data.isStage10TutorialFirst)
		{
			TutorialManager.Instance.Stage10TutorialEnable();
		}

		// �F�ύX�\��Ԑݒ�
		Player.player.GetComponent<IPlayer>().SetCanColorChange(stageNumber >= 10);

		// ���̃X�e�[�W�ɐi�ރ{�^���̏�Ԑݒ�
		if (stageNumber == 30)
		{
			mainMenuCanvas.GetComponent<IMainMenuCanvas>().SetNextStageButtonInteractable(false);
			nextStageORstageSelectCanvas.GetComponent<INextStageORStageSelectCanvas>().NextStageInInteractable(false);
			gameOverSelectCanvas.GetComponent<IGameOverSelectCanvas>().SetNextStageInteractable(false);
		}
		else
		{
			mainMenuCanvas.GetComponent<IMainMenuCanvas>().SetNextStageButtonInteractable(true);
			nextStageORstageSelectCanvas.GetComponent<INextStageORStageSelectCanvas>().NextStageInInteractable(true);
			gameOverSelectCanvas.GetComponent<IGameOverSelectCanvas>().SetNextStageInteractable(true);
		}

		// �X�e�[�W�������I���܂ő҂�
		await StageManager.Instance.StageLoadAsync(stageNumber);
		mainGameCanvas.GetComponent<IMainCanvas>().KeyReset();

		// �����ʒu
		var positions = new List<Vector3>();
		foreach (var starter in playerStarter)
		{
			positions.Add(starter.transform.position);
		}
		// �L�����N�^�[����
		Characters = CharacterFactory.Instance.Create(positions);

		// �L�����N�^�[�𓮂��Ȃ���Ԃɂ���
		foreach (var character in Characters)
		{
			character.GetComponent<ICharacter>().Stop();
		}

		// �v���C���[�̑���֎~
		Player.player.GetComponent<IPlayer>().NonControl();

		// �X�e�[�W���ƃX�e�[�W�ԍ��\��
		stageStartCanvas.GetComponent<IStageStartCanvas>().StageStart();

		// �t�F�[�h�C��
		await FadeIO.Instance.FadeIn();

		// �`���[�g���A���̕\�����I���܂ő҂�
		await UniTask.WaitUntil(() => !TutorialManager.Instance.IsEnable, cancellationToken: token);

		// �v���C���[�̑���J�n
		Player.player.GetComponent<IPlayer>().Control();

		await UniTask.WaitUntil(() => isPlaying, cancellationToken: token);

		var e = mainGameCanvas?.GetComponent<IMainCanvas>();
		mainGameCanvas?.GetComponent<IMainCanvas>()?.SetStartMessageActive(false);
		stageStartCanvas.SetActive(false);

		// �L�����N�^�[�𓮂����Ԃɂ���
		foreach (var character in Characters)
		{
			character.GetComponent<ICharacter>().Start();
		}

		// �o�ߎ��ԃ��Z�b�g
		elapsedTime = 0;
	}

	/// <summary>
	/// �I��
	/// </summary>
	async UniTask<bool> Final()
	{
		// �t�F�[�h�A�E�g
		await FadeIO.Instance.FadeOut();

		// �X�^�[�g�ʒu�폜
		playerStarter.Clear();
		// �S�[�������L�����N�^�[�폜
		goalCharacters.Clear();

		// �������Ă����L�����N�^�[�폜
		foreach (var e in Characters)
		{
			Destroy(e);
		}
		// �������Ă������폜
		LineManager.Instance.LineDeleteAll();

		// �������Ă����L�����N�^�[�폜
		CharacterManager.Instance.CharacterClear();

		isPlaying = false;

		return true;
	}

	/// <summary>
	/// �X�e�[�W�N���A
	/// </summary>
	async void StageClear()
	{
		var token = this.GetCancellationTokenOnDestroy();

		SoundManager.Instance.SEPlay(stageClearAudio).Forget();

		// ���b�Z�[�W�ύX
		mainGameCanvas.GetComponent<IMainCanvas>().SetMessageText("StageClear");
		// �X�e�[�W�N���A�������Ƃ�`����
		StageManager.Instance.StageClear();
		// �v���C���[������ł��Ȃ���Ԃɂ���
		Player.player.GetComponent<IPlayer>().NonControl();
		// ���Ԃ̑�����߂�
		Time.timeScale = 1;
		gameProgressSpeed = GameProgressSpeed.Default;
		isFastForward = false;

		mainGameCanvas.GetComponentInChildren<IFastForwardButton>().SetUVMove(false);
		mainGameCanvas.GetComponent<IMainCanvas>().SetButtonInteractable(false);
		await UniTask.Delay(System.TimeSpan.FromSeconds(stageClearDelayTime), cancellationToken: token);

		mainGameCanvas.GetComponent<IMainCanvas>().SetButtonInteractable(true);
		if (stageNumber != 30)
		{
			// ���̃X�e�[�W�Ɉړ����邩�X�e�[�W�I����ʂɈړ����邩�̑I������\��
			nextStageORstageSelectCanvas.SetActive(true);
		}
		else
		{
			GameClear();
		}
	}

	/// <summary>
	/// �Q�[���N���A
	/// </summary>
	public async void GameClear()
	{
		var token = this.GetCancellationTokenOnDestroy();
		var missionClearValue = 0;
		var stillNumber = 0;
		// �Q�[���N���A��ʂ̉摜�ݒ�
		var iGameClearCanvas = gameClearCanvas.GetComponent<IGameClearCanvas>();
		// �N���A�����~�b�V�����̐��v�Z
		foreach (var e in SaveManager.data.missionStates)
		{
			missionClearValue += (int)e;
		}
		for (int i = stillChangeMissionClearValue.Count - 1; i > 0; --i)
		{
			if (stillChangeMissionClearValue[i] <= missionClearValue)
			{
				stillNumber = i;
				if (stillChangeMissionClearValue[i] < missionClearValue)
				{
					iGameClearCanvas.AddAlbumMessgeActive(true);
				}
				break;
			}
			else
			{
				iGameClearCanvas.AddAlbumMessgeActive(false);
			}
		}
		iGameClearCanvas.SetGameClearImage(AlbumManager.Instance.GetStillSprite(stillNumber));
		iGameClearCanvas.SetGameClearEffect(AlbumManager.Instance.GetStillEffect(stillNumber));
		iGameClearCanvas.SetGameClearEffectMaterial(AlbumManager.Instance.GetStillMaterial(stillNumber));

		// �X�`�����J���������ݒ�
		SaveManager.data.stillOpeingValue = stillNumber + 1;

		// ����s�\
		Player.player.GetComponent<IPlayer>().NonControl();

		gameClearResultCanvas.SetActive(true);
		await UniTask.WaitUntil(() => !gameClearResultCanvas.activeSelf, cancellationToken: token);

		// �t�F�[�h�A�E�g
		await FadeIO.Instance.FadeOut();
		// �Q�[���N���A�\��
		gameClearCanvas.SetActive(true);
		// �t�F�[�h�C��
		await FadeIO.Instance.FadeIn();
		// �}�E�X���͂�����܂ő҂�
		await UniTask.WaitUntil(() => Mouse.current.leftButton.isPressed, cancellationToken: token);

		iGameClearCanvas.AddAlbumMessgeActive(false);
		// �V�[��������ɐ؂�ւ��Ȃ�
		LevelManager.canLevelMove = false;
		// �V�[���ǂݍ���
		LevelManager.OpenLevel("TitleScene").Forget();
		// �^�C�g����Ԃɖ߂�
		GameInstance.gameState = GameInstance.GameState.Title;
		// �t�F�[�h�A�E�g
		FadeIO.Instance.FadeOut().Forget();
		// �t�F�[�h�ƃV�[���ǂݍ��݂��I������܂ő҂�
		await UniTask.WaitUntil(() => !FadeIO.Instance.IsFading && !LevelManager.IsLevelLoading, cancellationToken: token);
		// �ǂݍ��݂��I��������V�[���؂�ւ��ł����Ԃɂ���
		LevelManager.canLevelMove = true;
	}

	/// <summary>
	/// �Q�[���I�[�o�[
	/// </summary>
	void GameOver()
	{
		Player.player.GetComponent<IPlayer>().NonControl();
		mainGameCanvas.GetComponent<IMainCanvas>().SetMessageText("GameOver");
		SoundManager.Instance.SEPlay(gameOverAudio).Forget();
		gameOverSelectCanvas.SetActive(true);
	}

	/// <summary>
	/// �Q�[����~
	/// </summary>
	void GameStop()
	{
		Player.player.GetComponent<IPlayer>().NonControl();
		gameProgressSpeed = GameProgressSpeed.Stop;
		Time.timeScale = 0;
	}

	void MainMenu()
	{
		GameStop();
		optionCanvas.SetActive(false);
		mainMenuCanvas.SetActive(true);
		Player.player.GetComponent<IPlayer>().NonControl();
	}

	/// <summary>
	/// �X�e�[�W���쐬���邽�߂̏���
	/// </summary>
	async void StagePreparation()
	{
		await Final();
		Init().Forget();
	}

	void IGM_Main.Init()
	{
		Init().Forget();
	}

	void IGM_Main.Final()
	{
		Final().Forget();
	}

	void IGM_Main.CharacterGoal(GameObject goalCharcter)
	{
		goalCharacters.Add(goalCharcter);
		if (goalCharacters.Count == Characters.Count)
		{
			StageClear();
		}
	}

	void IGM_Main.GameOver()
	{
		GameOver();
	}

	void IGM_Main.NextStage()
	{
		++stageNumber;
		StagePreparation();
	}

	void IGM_Main.Option()
	{
		optionCanvas.SetActive(true);
		mainMenuCanvas.SetActive(false);
	}

	void IGM_Main.ToGame()
	{
		Player.player.GetComponent<IPlayer>().Control();
		if (isPlaying)
		{
			foreach (var character in Characters)
			{
				character.GetComponent<ICharacter>().Start();
			}
		}
		mainMenuCanvas.SetActive(false);
	}

	void IGM_Main.MainMenu()
	{
		MainMenu();
	}

	void IGM_Main.GameClear()
	{
		GameClear();
	}

	List<GameObject> IGM_Main.GetInstanceCharacterObjects()
	{
		return Characters;
	}

	void IGM_Main.SetLineMagnitude(float lineMagnitude)
	{
		mainGameCanvas.GetComponent<IMainCanvas>().SetScoreText(lineMagnitude.ToString() + "cm");
	}

	void IGM_Main.GameStop()
	{
		GameStop();
	}

	void IGM_Main.GameReStart()
	{
		Time.timeScale = !isFastForward ? 1.0f : fastForwardSpeed;
		gameProgressSpeed = !isFastForward ? GameProgressSpeed.Twice : GameProgressSpeed.Default;
		Player.player.GetComponent<IPlayer>().Control();
	}

	void IGM_Main.FastForward()
	{
		Time.timeScale = isFastForward ? 1.0f : fastForwardSpeed;
		gameProgressSpeed = isFastForward ? GameProgressSpeed.Twice : GameProgressSpeed.Default;
		isFastForward = !isFastForward;
	}

	bool IGM_Main.GetIsFastForward()
	{
		return isFastForward;
	}

	void IGM_Main.CharacterGetKey(int number, GameInstance.KeyType keyType)
	{
		mainGameCanvas.GetComponent<IMainCanvas>().CharacterGetKey(number, keyType);
	}
}
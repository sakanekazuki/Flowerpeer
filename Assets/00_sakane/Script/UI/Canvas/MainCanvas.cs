using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// メインのキャンバス
public class MainCanvas : MonoBehaviour, IMainCanvas
{
	// メッセージ
	[SerializeField]
	TextMeshProUGUI message;

	// 引いた線の長さの合計
	[SerializeField]
	TextMeshProUGUI score;

	// 表示する選択肢
	[SerializeField]
	//GameObject trueFalseSelectCanvasPrefab;
	GameObject trueFalseSelectCanvas;

	// 開始メッセージ
	[SerializeField]
	GameObject startMessageObject;
	Image startMessageImage;
	bool isMessageDisplay = false;
	float time = 0;
	float flashValue = 0;
	[SerializeField]
	float flashSpeed;

	// 取得した鍵を表示する
	[SerializeField]
	List<Image> keyImageList = new List<Image>();

	// 鍵のスプライト
	[SerializeField]
	List<Sprite> keySprites = new List<Sprite>();

	// ボタンを無視する画像
	[SerializeField]
	GameObject buttonIgnoreImage;

	private void Start()
	{
		//trueFalseSelectCanvas = Instantiate(trueFalseSelectCanvasPrefab);
		trueFalseSelectCanvas.SetActive(false);
		GetComponent<Canvas>().worldCamera = Camera.main;
		startMessageImage = startMessageObject.GetComponent<Image>();
		buttonIgnoreImage.SetActive(false);
	}

	private void Update()
	{
		if (isMessageDisplay)
		{
			time += flashSpeed;
			flashValue = Mathf.Sin(time);
			flashValue = (flashValue + 3.0f) / 4.0f;
			startMessageImage.material.SetFloat("_AlphaValue", flashValue);
		}
	}

	/// <summary>
	/// メニューボタンが押された時に呼ばれる関数
	/// </summary>
	public void ToMenu()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().MainMenu();
	}

	/// <summary>
	/// リトライボタンが押された時に呼ばれる関数
	/// </summary>
	public async void Retry()
	{
		// 選択肢の表示
		trueFalseSelectCanvas.SetActive(true);
		// メッセージ設定
		trueFalseSelectCanvas.GetComponent<ITrueFalseSelectCanvas>().SetMessage("リトライしますか？");

		// ゲーム停止
		GM_Main.Instance.GetComponent<IGM_Main>().GameStop();

		// ボタンが押されるまで待つ
		var result = await trueFalseSelectCanvas.GetComponent<ITrueFalseSelectCanvas>().SelectWait();

		// はいを選択した場合は最初から
		if (result)
		{
			LevelManager.OpenLevel("MainScene").Forget();
			return;
		}

		// 選択肢非表示
		trueFalseSelectCanvas.SetActive(false);

		// ゲーム再開
		GM_Main.Instance.GetComponent<IGM_Main>().GameReStart();
	}

	/// <summary>
	/// 早送り
	/// </summary>
	public void FastForward()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().FastForward();
	}

	/// <summary>
	/// ポーズボタンが押された時に呼ばれる関数
	/// </summary>
	public void Pause()
	{
		GM_Main.Instance.GetComponent<IGM_Main>();
	}

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生する音</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	/// <summary>
	/// UIにカーソルを合わせたとき
	/// </summary>
	public void UIHovered()
	{
		Player.player.GetComponent<IPlayer>().DefaultCursor();
	}

	/// <summary>
	/// UIからカーソルを外したとき
	/// </summary>
	public void UIUnHovered()
	{
		Player.player.GetComponent<IPlayer>().MagicCursor();
	}

	void IMainCanvas.SetStartMessageActive(bool isActive)
	{
		startMessageObject?.SetActive(isActive);
		isMessageDisplay = isActive;
	}

	void IMainCanvas.SetMessageText(string message)
	{
		//this.message.text = message;
	}

	void IMainCanvas.SetScoreText(string score)
	{
		this.score.text = score;
	}

	void IMainCanvas.CharacterGetKey(int number, GameInstance.KeyType keyType)
	{
		keyImageList[number - 1].color = Color.white;
		keyImageList[number - 1].sprite = keySprites[(int)keyType];
	}

	void IMainCanvas.KeyReset()
	{
		var keys = GameObject.FindObjectsByType<Key>(FindObjectsSortMode.None);
		// 透明にする
		foreach (var key in keyImageList)
		{
			key.color = Color.clear;
		}
		// 鍵がある数だけ黒くする
		for (int i = 0; i < keys.Length; i++)
		{
			this.keyImageList[i].color = Color.black;
		}
	}

	void IMainCanvas.SetButtonInteractable(bool interactable)
	{
		buttonIgnoreImage.SetActive(!interactable);
	}
}
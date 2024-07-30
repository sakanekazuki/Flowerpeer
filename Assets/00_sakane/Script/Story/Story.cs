using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

// ストーリーを表示するクラス
public class Story : MonoBehaviour
{
	// テキスト
	[SerializeField]
	TMP_Text storyText;

	// ストーリーの内容
	[SerializeField, Multiline]
	List<string> storyContents = new List<string>();

	// 速度
	[SerializeField]
	float storySpeed;

	// 最後の文字を表示している時間
	[SerializeField]
	float lastTime;

	// 改行した際に止まる時間
	[SerializeField]
	float newLIneTime = 0.2f;

	// ストーリーの番号
	int storyNumber = 0;

	// キャンセルトークンソース
	CancellationTokenSource tokenSouce;
	CancellationToken token;

	// 最後のストーリーを表示する時間
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
	/// ストーリー表示
	/// </summary>
	/// <param name="token">キャンセルトークン</param>
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
				// タイトルロゴ表示
				transform.root.GetComponent<ITitleCanvas>().LogoDisplay();
			}
		}
	}

	/// <summary>
	/// 文字送り
	/// </summary>
	/// <param name="token">キャンセルトークン</param>
	async UniTask<bool> CharacterFeed(CancellationToken token)
	{
		// 表示する文字数
		int charaValue = 1;
		// ストーリ代入
		storyText.text = storyContents[storyNumber];
		// 文字が表示する文字を超えるまでループ
		while (charaValue < storyContents[storyNumber].Count() + 1)
		{
			// 文字数設定
			storyText.maxVisibleCharacters = charaValue;
			if (storyText.text[charaValue - 1].ToString() == "\n")
			{
				// 改行した際に少し止まる
				await UniTask.Delay(System.TimeSpan.FromSeconds(newLIneTime), cancellationToken: token);
			}
			await UniTask.Delay(System.TimeSpan.FromSeconds(storySpeed), cancellationToken: token);
			// 表示する文字追加
			++charaValue;
		}
		// 最後の文字を数秒表示
		await UniTask.Delay(System.TimeSpan.FromSeconds(lastTime), cancellationToken: token);
		return true;
	}
}
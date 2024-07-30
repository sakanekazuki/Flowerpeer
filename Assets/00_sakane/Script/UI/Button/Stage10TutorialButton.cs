using UnityEngine;
using UnityEngine.UI;

// ステージ10のチュートリアルを表示するボタン
public class Stage10TutorialButton : MonoBehaviour
{
	// チュートリアルボタン
	Button btn;

	private void Awake()
	{
		// ボタン取得
		btn = GetComponent<Button>();
	}

	private void OnEnable()
	{
		// チュートリアルを一度表示している場合選択できるようにする
		if (SaveManager.data.isStage10TutorialFirst)
		{
			btn.interactable = true;
		}
		else
		{
			btn.interactable = false;
		}
	}
}

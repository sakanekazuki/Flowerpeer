using UnityEngine;
using UnityEngine.UI;

// ステージ1のチュートリアルを表示するボタン
public class Stage1TutorialButton : MonoBehaviour
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
		if (SaveManager.data.isStage1TutorialFirst)
		{
			btn.interactable = true;
		}
		else
		{
			btn.interactable = false;
		}
	}
}
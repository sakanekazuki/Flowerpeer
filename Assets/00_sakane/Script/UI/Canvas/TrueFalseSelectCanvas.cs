using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

// 選択
public class TrueFalseSelectCanvas : MonoBehaviour,ITrueFalseSelectCanvas
{
	//// テキスト
	//[SerializeField]
	//TextMeshProUGUI messageText;
	//// テキストに表示する文字
	//[SerializeField]
	//string message;

	// 肯定した際に呼ぶ
	[SerializeField]
	UnityEvent trueClick;
	// 否定した際に呼ぶ
	[SerializeField]
	UnityEvent falseClick;

	// true = クリックした
	bool isClick = false;
	// true = 肯定した
	bool isTrue = false;

	private void Start()
	{
		//messageText.text = message;
	}

	/// <summary>
	/// 肯定した
	/// </summary>
	public void TrueClick()
	{
		trueClick.Invoke();
		isClick = true;
		isTrue = true;
	}

	/// <summary>
	/// 否定した
	/// </summary>
	public void FalseClick()
	{
		falseClick.Invoke();
		isClick = true;
		isTrue = false;
	}

	async UniTask<bool> ITrueFalseSelectCanvas.SelectWait()
	{
		isClick = false;

		await UniTask.WaitUntil(() => isClick);

		return isTrue;
	}

	void ITrueFalseSelectCanvas.SetMessage(string message)
	{
		//messageText.text = message;
	}
}
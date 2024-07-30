using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

// �I��
public class TrueFalseSelectCanvas : MonoBehaviour,ITrueFalseSelectCanvas
{
	//// �e�L�X�g
	//[SerializeField]
	//TextMeshProUGUI messageText;
	//// �e�L�X�g�ɕ\�����镶��
	//[SerializeField]
	//string message;

	// �m�肵���ۂɌĂ�
	[SerializeField]
	UnityEvent trueClick;
	// �ے肵���ۂɌĂ�
	[SerializeField]
	UnityEvent falseClick;

	// true = �N���b�N����
	bool isClick = false;
	// true = �m�肵��
	bool isTrue = false;

	private void Start()
	{
		//messageText.text = message;
	}

	/// <summary>
	/// �m�肵��
	/// </summary>
	public void TrueClick()
	{
		trueClick.Invoke();
		isClick = true;
		isTrue = true;
	}

	/// <summary>
	/// �ے肵��
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
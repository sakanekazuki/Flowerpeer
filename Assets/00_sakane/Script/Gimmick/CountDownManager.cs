using Cysharp.Threading.Tasks;
using UnityEngine;

// �J�E���g�_�E��
public class CountDownManager : ManagerBase<CountDownManager>
{
	// �J�E���g�_�E���̃L�����o�X
	[SerializeField]
	GameObject countDownCanvasPrefab;
	GameObject countDownCanvas;
	ICountDownCanvas icountDownCanvas;

	private void Start()
	{
		countDownCanvas = Instantiate(countDownCanvasPrefab);
		countDownCanvas.SetActive(false);
		icountDownCanvas = countDownCanvas.GetComponent<ICountDownCanvas>();
	}

	/// <summary>
	/// �J�E���g�_�E���J�n
	/// </summary>
	/// <param name="countDownTime">�J�E���g�_�E�����鎞��</param>
	/// <returns></returns>
	public async UniTask<bool> CountDownStart(float countDownTime)
	{
		countDownCanvas.SetActive(true);
		var time = countDownTime;
		while (time >= 0)
		{
			await UniTask.DelayFrame(1);
			time -= Time.deltaTime;
			icountDownCanvas.SetCountDownText(Mathf.Floor(time).ToString());
		}
		countDownCanvas.SetActive(false);
		return true;
	}
}
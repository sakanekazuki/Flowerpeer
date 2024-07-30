using TMPro;
using UnityEngine;

// �J�E���g�_�E���L�����o�X
public class CountDownCanvas : MonoBehaviour, ICountDownCanvas
{
	// �J�E���g�_�E���e�L�X�g
	[SerializeField]
	TextMeshProUGUI countDownTxt;

	/// <summary>
	/// �J�E���g�_�E���e�L�X�g�̐ݒ�
	/// </summary>
	/// <param name="value">�ݒ肷�镶��</param>
	void SetCountDownText(string value)
	{
		countDownTxt.text = value;
	}

	void ICountDownCanvas.SetCountDownText(string value)
	{
		SetCountDownText(value);
	}
}
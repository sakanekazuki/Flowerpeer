using UnityEngine;
using UnityEngine.UI;

// ������{�^��
public class FastForwardButton : MonoBehaviour, IFastForwardButton
{
	[SerializeField]
	GameObject speedUpImg;

	/// <summary>
	/// ������{�^�����������Ƃ�
	/// </summary>
	public void FastForward()
	{
		if (GM_Main.Instance.GetComponent<IGM_Main>().GetIsFastForward())
		{
			speedUpImg.GetComponent<Image>().material.SetFloat("_ScrollSpeed", -0.5f);
		}
		else
		{
			speedUpImg.GetComponent<Image>().material.SetFloat("_ScrollSpeed", 0);
		}
	}

	void IFastForwardButton.SetUVMove(bool isMove)
	{
		if (isMove)
		{
			speedUpImg.GetComponent<Image>().material.SetFloat("_ScrollSpeed", -0.5f);
		}
		else
		{
			speedUpImg.GetComponent<Image>().material.SetFloat("_ScrollSpeed", 0);
		}
	}
}
using UnityEngine;

// ���̃X�e�[�W�Ƀ{�^��
public class ToNextStageButton : MonoBehaviour
{
	/// <summary>
	/// �N���b�N���ɌĂ�
	/// </summary>
	public void NextStage()
	{
		// ���̃X�e�[�W�Ɉړ�
		GM_Main.Instance.GetComponent<IGM_Main>().NextStage();

		transform.parent.gameObject.SetActive(false);
	}
}
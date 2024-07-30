using UnityEngine;
using UnityEngine.UI;

// �X�e�[�W1�̃`���[�g���A����\������{�^��
public class Stage1TutorialButton : MonoBehaviour
{
	// �`���[�g���A���{�^��
	Button btn;

	private void Awake()
	{
		// �{�^���擾
		btn = GetComponent<Button>();
	}

	private void OnEnable()
	{
		// �`���[�g���A������x�\�����Ă���ꍇ�I���ł���悤�ɂ���
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
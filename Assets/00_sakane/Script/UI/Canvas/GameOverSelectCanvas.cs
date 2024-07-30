using UnityEngine;
using UnityEngine.UI;

// �Q�[���I�[�o�[���ɏo�Ă���I����
public class GameOverSelectCanvas : MonoBehaviour,IGameOverSelectCanvas
{
	// ���̃X�e�[�W�Ɉړ�����{�^��
	[SerializeField]
	Button nextStageButton;

	/// <summary>
	/// �X�e�[�W�I���ɖ߂�
	/// </summary>
	public void ToStageSelect()
	{
		GameInstance.gameState = GameInstance.GameState.StageSelect;
		LevelManager.OpenLevel("TitleScene").Forget();
	}

	/// <summary>
	/// ���̃X�e�[�W�ɐi��
	/// </summary>
	public void NextStage()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().NextStage();
	}

	/// <summary>
	/// ���g���C
	/// </summary>
	public void ReTry()
	{
		LevelManager.OpenLevel("MainScene").Forget();
	}

	/// <summary>
	/// SE�Đ�
	/// </summary>
	/// <param name="clip">�Đ����鉹</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	void IGameOverSelectCanvas.SetNextStageInteractable(bool isInteractable)
	{
		nextStageButton.interactable = isInteractable;
	}
}
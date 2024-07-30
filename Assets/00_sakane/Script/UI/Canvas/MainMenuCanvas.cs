using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ���C���̃��j���[�L�����o�X
public class MainMenuCanvas : MonoBehaviour,IMainMenuCanvas
{
// ���̃X�e�[�W�i�ރ{�^��
	[SerializeField]
	Button nextStageButton;

	// �X�e�[�W�ԍ�
	[SerializeField]
	TMP_Text stageNumberText;

	private void OnEnable()
	{
		stageNumberText.text = StageManager.NowStageNumber.ToString();
	}

	/// <summary>
	/// SE�Đ�
	/// </summary>
	/// <param name="clip">�Đ����鉹</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}

	/// <summary>
	/// �X�e�[�W�I��
	/// </summary>
	public void StageSelect()
	{
		GameInstance.gameState = GameInstance.GameState.StageSelect;
		LevelManager.OpenLevel("TitleScene").Forget();
	}

	/// <summary>
	/// ���̃X�e�[�W
	/// </summary>
	public  void NextStage()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().NextStage();
	}

	/// <summary>
	/// �I�v�V����
	/// </summary>
	public void Option()
	{
		GM_Main.Instance.GetComponent<IGM_Main>().Option();
	}

	/// <summary>
	/// �߂�
	/// </summary>
	public void Return()
	{
		gameObject.SetActive(false);
		GM_Main.Instance.GetComponent<IGM_Main>().ToGame();
		GM_Main.Instance.GetComponent<IGM_Main>().GameReStart();
	}

	void IMainMenuCanvas.SetNextStageButtonInteractable(bool intaractable)
	{
		nextStageButton.interactable = intaractable;
	}
}
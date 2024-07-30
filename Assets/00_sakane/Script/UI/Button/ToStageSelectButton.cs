using Cysharp.Threading.Tasks;
using UnityEngine;

// �X�e�[�W�I���Ɉړ�����{�^��
public class ToStageSelectButton : MonoBehaviour
{
	/// <summary>
	/// �X�e�[�W�I���Ɉړ�
	/// </summary>
	public void ToStageSelect()
	{
		// �V�[���؂�ւ������Ȃ��悤�ɐݒ�
		LevelManager.canLevelMove = false;
		// �V�[���ǂݍ���
		LevelManager.OpenLevel("TitleScene").Forget();
		// �Q�[���̏�Ԃ��X�e�[�W�I���ɂ���
		GameInstance.gameState = GameInstance.GameState.StageSelect;
		// �V�[����؂�ւ�
		LevelManager.canLevelMove = true;
	}
}
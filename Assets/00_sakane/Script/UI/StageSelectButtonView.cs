using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �X�e�[�W�I���{�^���̃X�N���[��
public class StageSelectButtonView : MonoBehaviour
{
	// �X�N���[���l
	float scrollValue = 0.04f;

	// �X�N���[���𐧌䂵�Ă���I�u�W�F�N�g
	ScrollRect scrollViewObject;

	private void Start()
	{
		scrollViewObject = transform.root.GetComponentInChildren<ScrollRect>();
	}

	/// <summary>
	/// �X�N���[��
	/// </summary>
	/// <param name="eventData">�C�x���g�f�[�^</param>
	public void Scroll(BaseEventData eventData)
	{
		scrollViewObject.verticalScrollbar.value += eventData.currentInputModule.input.mouseScrollDelta.y * scrollValue;
	}
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ステージ選択ボタンのスクロール
public class StageSelectButtonView : MonoBehaviour
{
	// スクロール値
	float scrollValue = 0.04f;

	// スクロールを制御しているオブジェクト
	ScrollRect scrollViewObject;

	private void Start()
	{
		scrollViewObject = transform.root.GetComponentInChildren<ScrollRect>();
	}

	/// <summary>
	/// スクロール
	/// </summary>
	/// <param name="eventData">イベントデータ</param>
	public void Scroll(BaseEventData eventData)
	{
		scrollViewObject.verticalScrollbar.value += eventData.currentInputModule.input.mouseScrollDelta.y * scrollValue;
	}
}
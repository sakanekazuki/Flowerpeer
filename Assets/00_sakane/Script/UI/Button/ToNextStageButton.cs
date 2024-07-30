using UnityEngine;

// 次のステージにボタン
public class ToNextStageButton : MonoBehaviour
{
	/// <summary>
	/// クリック時に呼ぶ
	/// </summary>
	public void NextStage()
	{
		// 次のステージに移動
		GM_Main.Instance.GetComponent<IGM_Main>().NextStage();

		transform.parent.gameObject.SetActive(false);
	}
}
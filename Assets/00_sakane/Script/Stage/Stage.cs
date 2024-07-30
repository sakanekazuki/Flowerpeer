using UnityEngine;
using UnityEngine.UI;

// ステージクラス
public class Stage : MonoBehaviour
{
	// ステージ番号
	public int number = 0;

	// true = クリアしたことのあるステージ
	bool isClear = false;

	// クリアしたことがあるステージの横に表示される画像
	[SerializeField]
	GameObject clearImg;

	// ミッション進行具合
	GameInstance.MissionState missionState;

	private void Awake()
	{
		// クリアしたか調べる
		isClear = SaveManager.data.isClear[number - 1];
		if (number == 1 || SaveManager.data.isOpening[number - 1])
		{
			GetComponentInChildren<Button>().interactable = true;
			SaveManager.data.isOpening[number - 1] = true;
		}
		else
		{
			GetComponentInChildren<Button>().interactable = false;
		}
		missionState = SaveManager.data.missionStates[number - 1];
	}

	private void Start()
	{
		// クリアしている場合は表示
		if (isClear)
		{
			clearImg.SetActive(true);
		}
		else
		{
			clearImg.SetActive(false);
		}
	}

	/// <summary>
	/// カーソルがあっている際に呼び出す関数
	/// </summary>
	public void Hovered()
	{
		transform.root.gameObject.GetComponent<IStageSelectCanvas>().SetLineMagnitude(SaveManager.data.minLineMagnitude[number - 1], isClear);
		transform.root.gameObject.GetComponent<IStageSelectCanvas>().SetPreviewImage(number);
		transform.root.gameObject.GetComponent<IStageSelectCanvas>().MissionProgression((int)missionState);
	}
}
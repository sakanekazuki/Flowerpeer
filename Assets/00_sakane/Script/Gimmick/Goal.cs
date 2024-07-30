using UnityEngine;

// ゴール
public class Goal : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Character"))
		{
			// キャラクターのインターフェース取得
			ICharacter icharacter = collision.gameObject.GetComponent<ICharacter>();

			// キャラクターを止める
			icharacter.Stop();

			// キャラクターのゴールアニメーション？

			// GM_Mainにゴールしたことを伝える
			GM_Main.Instance.GetComponent<IGM_Main>().CharacterGoal(collision.gameObject);
		}
	}
}
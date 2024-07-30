using UnityEngine;

// ワープホール
public class WarpHole : MonoBehaviour,IWarpHole
{
	// true = ワープできる
	bool canWarp = true;

	// ワープギミック
	IWarpGimmick iwarpGimmick;

	private void Start()
	{
		// ワープギミック取得
		iwarpGimmick = transform.parent.GetComponent<IWarpGimmick>();
		// ワープホール設定
		iwarpGimmick.AddWarpHole(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// キャラクターに当ってワープできる状態であればワープ
		if (collision.tag == "Character" && canWarp)
		{
			iwarpGimmick.Warp(collision.gameObject, gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// キャラクターがワープゾーンから離れた場合ワープできる状態に戻す
		if (collision.tag == "Character")
		{
			canWarp = true;
		}
	}

	void IWarpHole.IsWarped()
	{
		canWarp = false;
	}
}
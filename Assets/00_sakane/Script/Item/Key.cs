using UnityEngine;

// 鍵
public class Key : ItemBase<int>
{
	// 鍵のid
	[SerializeField]
	int id = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// キャラクターに衝突した際にキャラクターに拾われる
		if (collision.gameObject.CompareTag("Character"))
		{
			var keyNumber = collision.gameObject.GetComponent<ICharacter>().PickUpTheKey(id);
			GM_Main.Instance.GetComponent<IGM_Main>().CharacterGetKey(keyNumber, (GameInstance.KeyType)id);
			Destroy(gameObject);
		}
	}
}
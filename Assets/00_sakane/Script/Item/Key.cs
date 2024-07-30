using UnityEngine;

// ��
public class Key : ItemBase<int>
{
	// ����id
	[SerializeField]
	int id = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �L�����N�^�[�ɏՓ˂����ۂɃL�����N�^�[�ɏE����
		if (collision.gameObject.CompareTag("Character"))
		{
			var keyNumber = collision.gameObject.GetComponent<ICharacter>().PickUpTheKey(id);
			GM_Main.Instance.GetComponent<IGM_Main>().CharacterGetKey(keyNumber, (GameInstance.KeyType)id);
			Destroy(gameObject);
		}
	}
}
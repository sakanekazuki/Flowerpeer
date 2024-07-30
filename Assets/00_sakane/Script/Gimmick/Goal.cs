using UnityEngine;

// �S�[��
public class Goal : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Character"))
		{
			// �L�����N�^�[�̃C���^�[�t�F�[�X�擾
			ICharacter icharacter = collision.gameObject.GetComponent<ICharacter>();

			// �L�����N�^�[���~�߂�
			icharacter.Stop();

			// �L�����N�^�[�̃S�[���A�j���[�V�����H

			// GM_Main�ɃS�[���������Ƃ�`����
			GM_Main.Instance.GetComponent<IGM_Main>().CharacterGoal(collision.gameObject);
		}
	}
}
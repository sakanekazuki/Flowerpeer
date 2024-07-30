using UnityEngine;
using UnityEngine.UI;

// �e�L�X�g���b�V���v���_��
public class TextMeshProFlash : MonoBehaviour
{
	// �_�ł�����e�L�X�g
	Image messageText;

	// �_�ő��x
	[SerializeField]
	float speed;

	// �F
	Color defaultColor = Color.white;

	// �_�ł��Ă��鎞��
	float time = 0;

	private void Awake()
	{
		// �e�L�X�g�擾
		messageText = GetComponent<Image>();
		defaultColor = messageText.color;
	}

	private void FixedUpdate()
	{
		time += speed;
		var alphaValue = (Mathf.Sin(time) + 1) / 2;
		messageText.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alphaValue);
	}
}
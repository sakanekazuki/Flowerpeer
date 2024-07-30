using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

// �v���C���[�i����͓��͂���邾���j
public class Player : MonoBehaviour, IPlayer
{
	public static GameObject player;

	// ���𐶐�����ʒu
	Vector3 startPos;
	Vector3 endPos;

	// �ύX�̃f�B���C
	[SerializeField]
	float colorChangeDelayTime = 0.1f;

	// true = �F�ύX�\
	bool canColorChange = true;
	bool canColorChangeStage = false;

	// ���́i�ǂ��ŊǗ����邩�Y�ݒ��j
	GameInput input;

	// �v���C���[���������̐F�̔ԍ�
	int colorNumber = 0;
	// �������Ƃ��ł�����̐F�̐�
	[SerializeField]
	int colorValue = 2;

	// �h���b�O�����ۂ̉�
	[SerializeField]
	AudioClip dragClip;
	// �h���b�v�����ۂ̉�
	[SerializeField]
	AudioClip dropClip;

	// �J�[�\���p�e�N�X�`��
	[SerializeField]
	List<Texture2D> cursorTextures = new List<Texture2D>();

	// true = ���𐶐��\
	bool canLineCreate = false;

	// �J�[�\���̃I�t�Z�b�g
	[SerializeField]
	Vector2 curosrOffset = Vector2.zero;

	// �G�t�F�N�g
	[SerializeField]
	GameObject effectPrefab;
	GameObject effect;

	// �����I��
	bool isLineCreateForcedTermination = false;

	private void Awake()
	{
		player = gameObject;

		input = new GameInput();

		/*----- ���������� -----*/
		input.Player.LineCreating.started += LineCreateStart;
		input.Player.LineCreating.canceled += LineCreateEnd;
		input.Player.LineCreating.performed += LineCreate;

		/*----- ���폜 -----*/
		input.Player.LineDelete.started += LineDelete;

		/*----- ���̐F�ύX���� -----*/
		input.Player.ColorChange.performed += ColorChange;

		MagicCursor();
	}

	private void OnDestroy()
	{
		player = null;
		DefaultCursor();
		if (input != null)
		{
			input.Disable();

			/*----- ���������� -----*/
			input.Player.LineCreating.started -= LineCreateStart;
			input.Player.LineCreating.canceled -= LineCreateEnd;
			input.Player.LineCreating.performed -= LineCreate;

			/*----- ���폜 -----*/
			input.Player.LineDelete.started -= LineDelete;

			/*----- ���̐F�ύX���� -----*/
			input.Player.ColorChange.performed -= ColorChange;
		}
	}

	public void DefaultCursor()
	{
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}

	public void MagicCursor()
	{
		if (input.asset.enabled)
		{
			Cursor.SetCursor(cursorTextures[colorNumber], curosrOffset, CursorMode.Auto);
		}
	}

	/// <summary>
	/// �������I��
	/// </summary>
	void LineCreateEnd()
	{
		canColorChange = true;
		if (!canLineCreate)
		{
			return;
		}

		if (isLineCreateForcedTermination)
		{
			//var hits = Physics2D.OverlapPointAll(endPos);
			var hit = Physics2D.Raycast(startPos, (endPos - startPos), 100, LayerMask.GetMask("NoLineCreateArea"));
			endPos = hit.point;
			LineFactory.Instance.Creating(endPos);
		}

		Destroy(effect);
		effect = null;

		// ������
		LineFactory.Instance.CreateFinish();
		// SE�Đ�
		SoundManager.Instance.SEPlay(dropClip).Forget();

		GM_Main.Instance.GetComponent<IGM_Main>().GameReStart();
		canLineCreate = false;
	}

	/// <summary>
	/// �������J�n
	/// </summary>
	/// <param name="context">����</param>
	void LineCreateStart(InputAction.CallbackContext context)
	{
		startPos = context.ReadValue<Vector2>();
		startPos = Camera.main.ScreenToWorldPoint(new Vector3(startPos.x, startPos.y, 10.0f));
		// ����`���Ȃ��ꏊ�������ꍇ�`���Ȃ�
		var hits = Physics2D.OverlapPoint(startPos, LayerMask.GetMask("NoLineCreateArea"));
		if (hits != null)
		{
			canLineCreate = false;
			return;
		}
		canLineCreate = true;
		canColorChange = false;
		LineFactory.Instance.CreateStart(startPos, colorNumber);
		// SE�Đ�
		//SoundManager.Instance.SEPlay(dragClip).Forget();

		Time.timeScale = GM_Main.slowScale;
		effect = Instantiate(effectPrefab, startPos, Quaternion.identity);
	}

	/// <summary>
	/// �������I��
	/// </summary>
	/// <param name="context">����</param>
	void LineCreateEnd(InputAction.CallbackContext context)
	{
		LineCreateEnd();
	}

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="context">����</param>
	void LineCreate(InputAction.CallbackContext context)
	{
		if (!canLineCreate)
		{
			return;
		}
		endPos = context.ReadValue<Vector2>();
		endPos = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y, 10));
		LineFactory.Instance.Creating(endPos);
		effect.transform.position = endPos;
	}

	/// <summary>
	/// ���폜
	/// </summary>
	/// <param name="context">����</param>
	void LineDelete(InputAction.CallbackContext context)
	{
		var pos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
		var col = Physics2D.OverlapPointAll(pos);
		foreach (var e in col)
		{
			if (e?.tag == "Line")
			{
				Destroy(e.gameObject);
			}
		}
	}

	/// <summary>
	/// �F�ύX
	/// </summary>
	/// <param name="context">����</param>
	void ColorChange(InputAction.CallbackContext context)
	{
		// �F�ύX�\�ł���ΕύX
		if (canColorChangeStage && canColorChange)
		{
			// �F��ύX
			if (Mathf.Abs(context.ReadValue<float>()) > 0)
			{
				colorNumber = (colorNumber + 1) % colorValue;
				canColorChange = false;
				MagicCursor();
				ColorChangeDelay().Forget();
			}
		}
	}

	/// <summary>
	/// �F��ύX�ł���悤�ɂȂ�܂ő҂�
	/// </summary>
	/// <returns>�񓯊��̏��</returns>
	public async UniTaskVoid ColorChangeDelay()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(colorChangeDelayTime));
		canColorChange = true;
	}

	void IPlayer.NonControl()
	{
		input.Disable();
		DefaultCursor();
	}

	void IPlayer.Control()
	{
		input.Enable();
		MagicCursor();
	}

	void IPlayer.SetCanColorChange(bool canColorChange)
	{
		canColorChangeStage = canColorChange;
	}

	void IPlayer.DefaultCursor()
	{
		DefaultCursor();
	}

	void IPlayer.MagicCursor()
	{
		MagicCursor();
	}

	void IPlayer.LineCreateForcedTermination()
	{
		isLineCreateForcedTermination = true;
		LineCreateEnd();
		isLineCreateForcedTermination = false;
	}
}
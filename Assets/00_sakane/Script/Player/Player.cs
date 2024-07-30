using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

// プレイヤー（今回は入力を取るだけ）
public class Player : MonoBehaviour, IPlayer
{
	public static GameObject player;

	// 線を生成する位置
	Vector3 startPos;
	Vector3 endPos;

	// 変更のディレイ
	[SerializeField]
	float colorChangeDelayTime = 0.1f;

	// true = 色変更可能
	bool canColorChange = true;
	bool canColorChangeStage = false;

	// 入力（どこで管理するか悩み中）
	GameInput input;

	// プレイヤーが引く線の色の番号
	int colorNumber = 0;
	// 引くことができる線の色の数
	[SerializeField]
	int colorValue = 2;

	// ドラッグした際の音
	[SerializeField]
	AudioClip dragClip;
	// ドロップした際の音
	[SerializeField]
	AudioClip dropClip;

	// カーソル用テクスチャ
	[SerializeField]
	List<Texture2D> cursorTextures = new List<Texture2D>();

	// true = 線を生成可能
	bool canLineCreate = false;

	// カーソルのオフセット
	[SerializeField]
	Vector2 curosrOffset = Vector2.zero;

	// エフェクト
	[SerializeField]
	GameObject effectPrefab;
	GameObject effect;

	// 強制終了
	bool isLineCreateForcedTermination = false;

	private void Awake()
	{
		player = gameObject;

		input = new GameInput();

		/*----- 線生成入力 -----*/
		input.Player.LineCreating.started += LineCreateStart;
		input.Player.LineCreating.canceled += LineCreateEnd;
		input.Player.LineCreating.performed += LineCreate;

		/*----- 線削除 -----*/
		input.Player.LineDelete.started += LineDelete;

		/*----- 線の色変更入力 -----*/
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

			/*----- 線生成入力 -----*/
			input.Player.LineCreating.started -= LineCreateStart;
			input.Player.LineCreating.canceled -= LineCreateEnd;
			input.Player.LineCreating.performed -= LineCreate;

			/*----- 線削除 -----*/
			input.Player.LineDelete.started -= LineDelete;

			/*----- 線の色変更入力 -----*/
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
	/// 線生成終了
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

		// 線生成
		LineFactory.Instance.CreateFinish();
		// SE再生
		SoundManager.Instance.SEPlay(dropClip).Forget();

		GM_Main.Instance.GetComponent<IGM_Main>().GameReStart();
		canLineCreate = false;
	}

	/// <summary>
	/// 線生成開始
	/// </summary>
	/// <param name="context">入力</param>
	void LineCreateStart(InputAction.CallbackContext context)
	{
		startPos = context.ReadValue<Vector2>();
		startPos = Camera.main.ScreenToWorldPoint(new Vector3(startPos.x, startPos.y, 10.0f));
		// 線を描けない場所だった場合描かない
		var hits = Physics2D.OverlapPoint(startPos, LayerMask.GetMask("NoLineCreateArea"));
		if (hits != null)
		{
			canLineCreate = false;
			return;
		}
		canLineCreate = true;
		canColorChange = false;
		LineFactory.Instance.CreateStart(startPos, colorNumber);
		// SE再生
		//SoundManager.Instance.SEPlay(dragClip).Forget();

		Time.timeScale = GM_Main.slowScale;
		effect = Instantiate(effectPrefab, startPos, Quaternion.identity);
	}

	/// <summary>
	/// 線生成終了
	/// </summary>
	/// <param name="context">入力</param>
	void LineCreateEnd(InputAction.CallbackContext context)
	{
		LineCreateEnd();
	}

	/// <summary>
	/// 線生成
	/// </summary>
	/// <param name="context">入力</param>
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
	/// 線削除
	/// </summary>
	/// <param name="context">入力</param>
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
	/// 色変更
	/// </summary>
	/// <param name="context">入力</param>
	void ColorChange(InputAction.CallbackContext context)
	{
		// 色変更可能であれば変更
		if (canColorChangeStage && canColorChange)
		{
			// 色を変更
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
	/// 色を変更できるようになるまで待つ
	/// </summary>
	/// <returns>非同期の状態</returns>
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
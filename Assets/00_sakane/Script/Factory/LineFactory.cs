using System.Collections.Generic;
using UnityEngine;

// 線生成
public class LineFactory : FactoryBase<LineFactory>
{
	// 線のステータス
	[SerializeField]
	SO_LineStatus lineStatus;

	// 線のスプライト
	Sprite lineSprite;

	// 線の太さ
	[SerializeField]
	float yScale = 0.2f;

	// 生成中の線
	GameObject creatingLine = null;
	// 位置
	Vector3 startPos = Vector3.zero;

	// 生成中のスプライトレンダラー
	SpriteRenderer creatingLineSpriteRenderer = null;

	// 登れない角度の場合のスプライト
	[SerializeField]
	List<Sprite> nonClimbableSprites = new List<Sprite>();

	// 線を描けないときの透明度
	[SerializeField]
	Color translucent;

	// 線を引き終わった際に出すエフェクト
	[SerializeField]
	GameObject createEffectPrefab;

	int number = 0;

	/// <summary>
	/// 生成開始
	/// </summary>
	/// <param name="position">生成位置</param>
	/// <param name="number">線のステータスの番号</param>
	public void CreateStart(Vector3 position, int number)
	{
		// 生成開始位置の設定
		startPos = position;

		// 線生成
		creatingLine = base.Create(position / 2);
		// タグ設定
		creatingLine.tag = lineStatus.statuses[number].tag;
		// レイヤー設定
		creatingLine.layer = lineStatus.statuses[number].layer;
		// スプライトレンダラー取得
		creatingLineSpriteRenderer = creatingLine.GetComponent<SpriteRenderer>();
		// 色設定
		creatingLineSpriteRenderer.sprite = lineStatus.statuses[number].sprite;
		lineSprite = lineStatus.statuses[number].sprite;
		this.number = number;
	}

	/// <summary>
	/// 生成中
	/// </summary>
	/// <param name="nowPosition">現在の位置</param>
	public void Creating(Vector3 nowPosition)
	{
		// 位置設定
		creatingLine.transform.position = (startPos + nowPosition) / 2;
		// 大きさ設定
		creatingLine.transform.localScale = new Vector3((startPos - nowPosition).magnitude, yScale);

		// 線の方向
		var direction = startPos - nowPosition;
		// 線の方向のyが0より下の場合方向が180度変わるので、マイナスにする必要がある
		if (direction.y < 0)
		{
			direction = -direction;
		}
		// 角度設定
		creatingLine.transform.localEulerAngles = new Vector3(0, 0, Vector3.Angle(Vector3.right, direction));

		// 登ることの出来ない角度
		if (creatingLine.transform.localEulerAngles.z > GM_Main.climbableAngle + 0.1f && creatingLine.transform.localEulerAngles.z < (180 - (GM_Main.climbableAngle + 0.1f)))
		{
			// 登れない角度の場合のスプライト設定
			creatingLineSpriteRenderer.sprite = nonClimbableSprites[number];
		}
		else
		{
			// 登れる角度の場合のスプライト設定
			creatingLineSpriteRenderer.sprite = lineSprite;
		}

		var isCreate = true;
		// キャラクターか同色の線に衝突していた場合半透明にする
		var hits = Physics2D.OverlapBoxAll(creatingLine.transform.position, creatingLine.transform.localScale, creatingLine.transform.localEulerAngles.z);
		foreach (var e in hits)
		{
			if (e.transform.gameObject != creatingLine)
			{
				if (e.transform.CompareTag("Character"))
				{
					isCreate = false;
					break;
				}
				else if (e.gameObject.CompareTag("Line") && (e.gameObject.layer == creatingLine.layer))
				{
					isCreate = false;
					break;
				}
				else if (e.gameObject.CompareTag("WarpHole"))
				{
					isCreate = false;
					break;
				}
			}
		}
		// 最小サイズ
		var minSize = GM_Main.Instance.GetComponent<IGM_Main>().GetInstanceCharacterObjects()[0].GetComponent<ICharacter>().GetSize() / 2;
		if (minSize.x > creatingLine.transform.localScale.x)
		{
			isCreate = false;
		}
		// 線を描けないときは半透明にする
		if (!isCreate)
		{
			creatingLineSpriteRenderer.color = translucent;
		}
		else
		{
			creatingLineSpriteRenderer.color = Color.white;
		}
	}

	/// <summary>
	/// 線の生成終了
	/// </summary>
	public void CreateFinish()
	{
		// コライダー設定
		creatingLine.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
		// スプライト設定
		creatingLineSpriteRenderer.sprite = lineSprite;

		// 線の最小サイズ
		var minSize = GM_Main.Instance.GetComponent<IGM_Main>().GetInstanceCharacterObjects()[0].GetComponent<ICharacter>().GetSize() / 2;

		var canCreate = true;
		// 最小サイズより小さい場合は生成しない
		if (minSize.x > creatingLine.transform.localScale.x)
		{
			Destroy(creatingLine);
			canCreate = false;
		}
		else
		{
			// キャラクターに衝突していた場合生成しない
			var hits = Physics2D.OverlapBoxAll(creatingLine.transform.position, creatingLine.transform.localScale, creatingLine.transform.localEulerAngles.z);
			foreach (var e in hits)
			{
				if (e.transform.gameObject != creatingLine)
				{
					if (e.transform.CompareTag("Character"))
					{
						Destroy(creatingLine);
						canCreate = false;
						break;
					}
					else if (e.gameObject.CompareTag("Line") && (e.gameObject.layer == creatingLine.layer))
					{
						Destroy(creatingLine);
						canCreate = false;
						break;
					}
					else if (e.gameObject.CompareTag("WarpHole"))
					{
						Destroy(creatingLine);
						canCreate = false;
						break;
					}
				}
			}
		}

		if (canCreate)
		{
			// 線管理クラスに登録
			LineManager.Instance.LineCreate(creatingLine);
			GM_Main.isPlaying = true;
		}
		// エフェクト生成
		//Instantiate(createEffectPrefab, creatingLine.transform.position, Quaternion.identity);

		// 設定を戻す
		creatingLine = null;
		startPos = Vector3.zero;
		creatingLineSpriteRenderer = null;
		number = 0;
	}

	/// <summary>
	/// 線生成
	/// </summary>
	/// <param name="startPos">開始位置</param>
	/// <param name="endPos">終了位置</param>
	/// <param name="number">色の番号</param>
	//public void Create(Vector3 startPos,Vector3 endPos,int number)
	//{
	//	// 線生成
	//	var line = base.Create((startPos + endPos) / 2);
	//	// タグ設定
	//	line.tag = lineStatus.statuses[number].tag;
	//	// レイヤー設定
	//	line.layer = lineStatus.statuses[number].layer;
	//	// 大きさ設定
	//	line.transform.localScale = new Vector3((startPos - endPos).magnitude, yScale);

	//	// 線の方向
	//	var direction = startPos - endPos;
	//	// 線の方向のyが0より下の場合方向が180度変わるので、マイナスにする必要がある
	//	if (direction.y < 0)
	//	{
	//		direction = -direction;
	//	}
	//	// 角度設定
	//	line.transform.localEulerAngles = new Vector3(0, 0, Vector3.Angle(Vector3.right, direction));

	//	// 線のスプライト描画クラス取得
	//	var lineSprite = line.GetComponent<SpriteRenderer>();
	//	// スプライト設定
	//	lineSprite.sprite = this.lineSprite;
	//	// スプライトの色設定
	//	lineSprite.color = lineStatus.statuses[number].color;
	//	// コライダー追加
	//	line.AddComponent<BoxCollider2D>();
	//}
}
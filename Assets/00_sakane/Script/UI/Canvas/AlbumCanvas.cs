using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// アルバムを表示するキャンバス
public class AlbumCanvas : MonoBehaviour
{
	// アルバムを表示するボタン
	[SerializeField]
	List<Button> albumButtons = new List<Button>();

	// アルバムを表示するイメージ
	[SerializeField]
	Image albumImage;

	// アルバムに出すエフェクト
	[SerializeField]
	Image albumEffect;

	float unScaledTime;

	private void Start()
	{
		//GetComponent<Canvas>().worldCamera = Camera.main;
		albumImage.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (albumEffect.IsActive())
		{
			albumEffect.material.SetFloat("_UnScaledTime", unScaledTime);
			unScaledTime += Time.unscaledDeltaTime;
		}
		else
		{
			unScaledTime = 0;
		}
	}

	private void OnEnable()
	{
		// ボタンの状態初期化
		foreach (var button in albumButtons)
		{
			button.interactable = false;
		}
		// 見ることのできる画像を表示、ボタンを有効にする
		for (int i = 0; i < SaveManager.data.stillOpeingValue; ++i)
		{
			albumButtons[i].interactable = true;
			albumButtons[i].GetComponent<Image>().sprite = AlbumManager.Instance.GetStillSprite(i);
		}
	}

	/// <summary>
	/// アルバムを表示
	/// </summary>
	/// <param name="number">アルバムの番号</param>
	public void AlbumDisplay(int number)
	{
		// 画像設定
		albumImage.sprite = AlbumManager.Instance.GetStillSprite(number);
		// 画像を表示
		albumImage.gameObject.SetActive(true);
		// エフェクト表示
		albumEffect.gameObject.SetActive(true);
		// アルバムのエフェクト生成
		albumEffect.sprite = AlbumManager.Instance.GetStillEffect(number);
		// マテリアル設定
		albumEffect.material = AlbumManager.Instance.GetStillMaterial(number);
	}

	/// <summary>
	/// 戻るボタンを押したとき
	/// </summary>
	public void Return()
	{
		gameObject.SetActive(false);
	}

	/// <summary>
	/// アルバム非表示
	/// </summary>
	public void AlbumDisable()
	{
		albumImage.gameObject.SetActive(false);
		albumEffect.gameObject.SetActive(false);
	}

	/// <summary>
	/// SE再生
	/// </summary>
	/// <param name="clip">再生するSE</param>
	public void SEPlay(AudioClip clip)
	{
		SoundManager.Instance.SEPlay(clip).Forget();
	}
}

using UnityEngine;

// キャラクターのステータス
[System.Serializable]
public class CharacterStatus
{
	public enum CharacterColor
	{
		Red,
		Blue,
	}

	public string tag;
	public int layer;
	public Color color;
	public Sprite sprite;
	public CharacterColor characterColor;
}
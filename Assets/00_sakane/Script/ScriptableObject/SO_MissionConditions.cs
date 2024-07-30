using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ミッションの条件
[CreateAssetMenu(fileName = "MissionConditions", menuName = "ScriptableObject/MissionCondisions")]
public class SO_MissionConditions : ScriptableObject
{
	public List<MissionConditions> missionConditions = new List<MissionConditions>();

	#if UNITY_EDITOR
	[ContextMenu("CSVLoad")]
	public void CSVLoad()
	{
		// ファイル名
		var fileName = "";
		// ファイル取得
		fileName = EditorUtility.OpenFilePanel("CSVLoad", "", "");

		// Resourceより下のフォルダを取得
		fileName = fileName.Substring(fileName.LastIndexOf("Resources/"));
		// 拡張子を抜く
		fileName = fileName.Substring(0, fileName.LastIndexOf("."));
		// Resourcesを抜く
		fileName = fileName.Substring(fileName.IndexOf("/") + 1);
		// ファイル読み込み
		var data = TextLoader.CSVLoad(fileName);

		missionConditions.Clear();
		foreach (var line in data)
		{
			if (data.IndexOf(line) == 0)
			{
				continue;
			}
			var conditions = new MissionConditions();
			foreach (var item in line)
			{
				conditions.ClearConditions.Add(float.Parse(item));
			}
			missionConditions.Add(conditions);
		}
	}
	#endif
}
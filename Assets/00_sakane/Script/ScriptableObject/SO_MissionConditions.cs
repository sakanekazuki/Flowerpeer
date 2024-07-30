using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// �~�b�V�����̏���
[CreateAssetMenu(fileName = "MissionConditions", menuName = "ScriptableObject/MissionCondisions")]
public class SO_MissionConditions : ScriptableObject
{
	public List<MissionConditions> missionConditions = new List<MissionConditions>();

	#if UNITY_EDITOR
	[ContextMenu("CSVLoad")]
	public void CSVLoad()
	{
		// �t�@�C����
		var fileName = "";
		// �t�@�C���擾
		fileName = EditorUtility.OpenFilePanel("CSVLoad", "", "");

		// Resource��艺�̃t�H���_���擾
		fileName = fileName.Substring(fileName.LastIndexOf("Resources/"));
		// �g���q�𔲂�
		fileName = fileName.Substring(0, fileName.LastIndexOf("."));
		// Resources�𔲂�
		fileName = fileName.Substring(fileName.IndexOf("/") + 1);
		// �t�@�C���ǂݍ���
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
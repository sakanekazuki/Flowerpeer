using System.Collections.Generic;
using UnityEngine;

// ���Ǘ��N���X
public class LineManager : ManagerBase<LineManager>
{
	// �Ǘ������
	List<GameObject> lines = new List<GameObject>();
	// ���̒���
	float blueLineMagnitude = 0;
	float redLineMagnitude = 0;
	float allLineMagnitude = 0;
	public float AllLineMagnitude
	{
		get => allLineMagnitude;
	}

	// ����`�����������L�^�ł���ő�l
	[SerializeField]
	float maxMagnitude = 9999;

	/// <summary>
	/// ���̒����ۑ�
	/// </summary>
	public void MagnitudeSave()
	{
		var stageNumber = StageManager.NowStageNumber;

		// �������������v�l���v�Z
		allLineMagnitude = blueLineMagnitude + redLineMagnitude;
		// ���v�l���ő�l�𒴂��Ă����ꍇ�ő�l�ɂ���
		if (allLineMagnitude > maxMagnitude)
		{
			allLineMagnitude = maxMagnitude;
		}
		// ���܂łň�ԒႢ�l�������ꍇ�ۑ�
		if (SaveManager.data.minLineMagnitude[stageNumber - 1] > allLineMagnitude)
		{
			SaveManager.data.minLineMagnitude[stageNumber - 1] = allLineMagnitude;
		}
	}

	/// <summary>
	/// �Ǘ�������ǉ�
	/// </summary>
	/// <param name="line">�Ǘ������</param>
	public void LineCreate(GameObject line)
	{
		lines.Add(line);

		// ����
		if (line.layer == 10)
		{
			blueLineMagnitude += line.transform.localScale.x;
		}
		else if (line.layer == 11)
		{
			redLineMagnitude += line.transform.localScale.x;
		}

		// ���v���v�Z
		var magnitude = blueLineMagnitude + redLineMagnitude;
		// �ő�̏ꍇ�ő�l�ɂ���
		if (magnitude > maxMagnitude)
		{
			magnitude = maxMagnitude;
		}
		// ���̒����̕\��(�؂�̂�)
		GM_Main.Instance.GetComponent<IGM_Main>().SetLineMagnitude(Mathf.Floor(magnitude));
	}

	/// <summary>
	/// �S�Ă̐��폜
	/// </summary>
	public void LineDeleteAll()
	{
		foreach (var line in lines)
		{
			Destroy(line);
		}
		// �`������������
		lines.Clear();
		// ���̒������Z�b�g
		blueLineMagnitude = 0;
		redLineMagnitude = 0;
		GM_Main.Instance.GetComponent<IGM_Main>().SetLineMagnitude(0);
	}
}
using System.Collections.Generic;
using UnityEngine;

// �L�����N�^�[�̏����X�e�[�^�X��ۑ����Ă����X�N���v�^�u���I�u�W�F�N�g
[CreateAssetMenu(fileName = "LineStatus", menuName = "ScriptableObject/LineStatus")]
public class SO_LineStatus : ScriptableObject
{
    public List<Status> statuses = new List<Status>();
}
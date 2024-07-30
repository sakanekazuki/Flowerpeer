using System.Collections.Generic;
using UnityEngine;

// キャラクターの初期ステータスを保存しておくスクリプタブルオブジェクト
[CreateAssetMenu(fileName = "LineStatus", menuName = "ScriptableObject/LineStatus")]
public class SO_LineStatus : ScriptableObject
{
    public List<Status> statuses = new List<Status>();
}
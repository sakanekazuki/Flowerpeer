using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 同時押しボタン
public class SimultaneouslyPressSwitch : MonoBehaviour
{
	// 猶予
	[SerializeField]
	float graceTime;

	// スイッチ
	[SerializeField]
	GameObject switchObject1;
	// スイッチ
	[SerializeField]
	GameObject switchObject2;
}
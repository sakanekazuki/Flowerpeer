using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorAngleTest : MonoBehaviour
{
	[SerializeField]
	Vector2 angle1;

	[SerializeField]
	Vector2 angle2;

	[ContextMenu("AngleCheck")]
	public void AngleCheck()
	{
		var angle = Vector2.SignedAngle(angle1, angle2);
		Debug.Log(angle);
	}
}
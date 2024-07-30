using UnityEngine;

public class test : MonoBehaviour
{
	// Update is called once per frame
	void Update()
	{
		Debug.Log(Vector3.Lerp(Vector3.zero, new Vector3(10, 0, 0), 0.08f));
	}
}

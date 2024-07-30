using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(gameObject + " : collision");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(gameObject + " : trigger");
	}
}

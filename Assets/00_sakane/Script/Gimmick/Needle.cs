using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �j
public class Needle : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Character"))
		{
			
		}
	}
}
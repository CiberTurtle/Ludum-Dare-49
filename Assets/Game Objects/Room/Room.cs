using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField] Color[] colors;

	void Awake()
	{
		GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
	}
}

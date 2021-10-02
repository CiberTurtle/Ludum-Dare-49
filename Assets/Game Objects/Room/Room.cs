using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField] Color[] colors;

	void Awake()
	{
		var size = new Vector2(Random.Range(3, 7), Random.Range(3, 10));
		GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
		GetComponent<SpriteRenderer>().size = size;
	}

	void FixedUpdate()
	{
		if (transform.position.y < 0)
		{
			Destroy(gameObject);
		}
	}
}

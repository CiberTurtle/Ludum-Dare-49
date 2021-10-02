using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDropManager : MonoBehaviour
{
	[SerializeField] GameObject box;

	[SerializeField] Transform[] spawns;

	[SerializeField] float minDropTime;
	[SerializeField] float maxDropTime;

	[SerializeField] float minTweenBox;
	[SerializeField] float maxTweenBox;

	[SerializeField] int minBox;
	[SerializeField] int maxBox;

	int boxCount;
	float tilDrop;
	float tilBox;

	void Start()
	{
		tilDrop = Random.Range(minDropTime, maxDropTime);
		boxCount = 0;
		tilBox = Random.Range(minTweenBox, maxTweenBox);
	}

	void FixedUpdate()
	{
		if (tilDrop <= 0)
		{
			if (tilBox <= 0)
			{
				Instantiate(box, spawns[Random.Range(0, spawns.Length)]);
				tilBox = Random.Range(minTweenBox, maxTweenBox);
				boxCount -= 1;
			}

			tilBox -= 1 * Time.deltaTime;

			if (boxCount == 0)
			{
				tilDrop = Random.Range(minDropTime, maxDropTime);
				boxCount = Random.Range(minBox, maxBox);
			}
		}
		else
			tilDrop -= 1 * Time.deltaTime;
	}
}

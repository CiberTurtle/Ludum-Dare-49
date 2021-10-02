using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoMannager : MonoBehaviour
{
	[SerializeField] GameObject volcanoMeteor;

	[SerializeField] Transform[] spawns;

	[SerializeField] float minVolcanoTime;
	[SerializeField] float maxVolcanoTime;

	[SerializeField] float minTweenMeteors;
	[SerializeField] float maxTweenMeteors;

	[SerializeField] int minMetors;
	[SerializeField] int maxMeteors;

	int meteorCount;
	float tilVolcano;
	float tilMeteor;

	void Start()
	{
		tilVolcano = Random.Range(minVolcanoTime, maxVolcanoTime);
		meteorCount = 0;
		tilMeteor = 0;
	}

	void FixedUpdate()
	{
		if (tilVolcano <= 0)
		{
			if (tilMeteor <= 0)
			{
				Instantiate(volcanoMeteor, spawns[Random.Range(0, spawns.Length)]);
				tilMeteor = Random.Range(minTweenMeteors, maxTweenMeteors);
				meteorCount -= 1;
			}

			tilMeteor -= 1 * Time.deltaTime;

			if (meteorCount == 0)
			{
				tilVolcano = Random.Range(minVolcanoTime, maxVolcanoTime);
				meteorCount = Random.Range(minMetors, maxMeteors);
			}
		}
		tilVolcano -= 1 * Time.deltaTime;
	}
}

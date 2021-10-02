using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
	public GameObject meteor;

	[SerializeField] float minMeteorSpawnTime;
	[SerializeField] float maxMeteorSpawnTime;
	float tilSpawn;

	[SerializeField] Transform[] spawns;

	void Start()
	{
		tilSpawn = Random.Range(minMeteorSpawnTime, maxMeteorSpawnTime);
	}
	void FixedUpdate()
	{
		if (tilSpawn <= 0)
		{
			Transform spawn = spawns[Random.Range(0, spawns.Length)];
			Instantiate(meteor, spawn.position, spawn.rotation);
			tilSpawn = Random.Range(minMeteorSpawnTime, maxMeteorSpawnTime);
		}
		tilSpawn -= 1 * Time.deltaTime;
	}
}

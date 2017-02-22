using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	SpawnManager manager;
	SpawnManager spawner;

	Wave[] waves;

	public Enemy spawn;
	public Transform[] spawners;
	public bool dayCycle = true;

	Transform spawnPoint;

	Wave currentWave;
	int currentWaveNumber;

	int enemiesRemainingToSpawn;
	int enemiesRemainingAlive;
	float nextSpawnTime;

	bool RoundCheck() {
		if (manager.GetRoundComplete()) return true;
		else return false;
	}

	void Start() {
		
		spawner = FindObjectOfType<SpawnManager>();
		manager = GetComponentInParent<SpawnManager>();
		manager.Start();

		for (int i = 0; i <= transform.childCount - 1; i++)
			spawners[i] = transform.GetChild(i);

		SetWaves();
		NextWave();
	}

	void Update() {
		if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime && Time.time > currentWave.delay) {
			FindSpawnPoint();
			enemiesRemainingToSpawn--;
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

			Enemy spawnedEnemy = Instantiate(spawn, spawnPoint.position, Quaternion.identity) as Enemy;
			spawnedEnemy.OnDeath += OnEnemyDeath;
		}
		else if (enemiesRemainingAlive == 0 && currentWaveNumber != waves.Length) {
			NextWave();
		}
		if (RoundCheck()) {
			Debug.Log(gameObject.name + " round complete");
			SetWaves();
			NextWave();
		}
	}

	void OnEnemyDeath() {
		enemiesRemainingAlive--;
		Debug.Log("Enemy was killed");
		if (enemiesRemainingAlive == 0 && currentWaveNumber >= waves.Length) {
			Debug.Log("Spawner round ended");
			manager.RoundEnd();
		}
		else if (enemiesRemainingAlive == 0) {
			NextWave();
		}
	}

	void NextWave() {

		if (spawner.dayCycle == false) {
			if (currentWaveNumber < waves.Length) {
				currentWave = waves [currentWaveNumber];

				enemiesRemainingToSpawn = currentWave.enemyCount;
				enemiesRemainingAlive = enemiesRemainingToSpawn;
				currentWave.delay += Time.time;

				Debug.Log ("Round: " + manager.GetRoundNum () + "Wave: " + (currentWaveNumber + 1) + ". Enemies: " + enemiesRemainingToSpawn);
			}
			currentWaveNumber++;
		}
	}

	void FindSpawnPoint() {
		int point = Random.Range(0, spawners.Length);
		spawnPoint = spawners[point];
	}

	void SetWaves() {
		Debug.Log("Waves set");
		waves = manager.GetWaves();
		currentWaveNumber = 0;
	}

	//[System.Serializable]
	public class Wave
	{
		public int enemyCount;

		// time in seconds
		public float timeBetweenSpawns;
		public float delay;
	}
}

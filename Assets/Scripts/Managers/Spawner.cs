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
    private float animSpawn;
    bool shouldAnim = true;

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
        if (enemiesRemainingToSpawn > 0 && Time.time > animSpawn && Time.time > currentWave.delay)
        {
            int point = FindSpawnPoint();
            if (shouldAnim)
            {
                GameObject.FindGameObjectWithTag("Flash").GetComponent<Animator>().SetTrigger("spawn");
                shouldAnim = false;
            }
            if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime && Time.time > currentWave.delay)
            {
                enemiesRemainingToSpawn--;
                nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
                animSpawn = nextSpawnTime - 0.75f;

                Enemy spawnedEnemy = Instantiate(spawn, spawnPoint.position, Quaternion.Euler(90, 0, 0)) as Enemy;
                spawnedEnemy.OnDeath += OnEnemyDeath;
                shouldAnim = true;
            }
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
		//Debug.Log("Enemy was killed");
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

	int FindSpawnPoint() {
		int point = Random.Range(0, spawners.Length);
		spawnPoint = spawners[point];
        return point;
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

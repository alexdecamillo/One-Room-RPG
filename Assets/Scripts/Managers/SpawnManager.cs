using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public event System.Action RoundChange;

	// enemy increase between rounds and waves
	// NEED TO CREATE THE ACTUAL EQUTION THAT THESE TRANSLATE TO VARIABLES IN
	const float roundMulitplier = 0.3f;
	const float waveMulitplier = 0.25f;

	int startingRound = 0;
	int round;
	int totalRounds = 25;
	int walkEnemies = 3;

	Round[] walk;
	Player player;

	bool roundComplete;
	bool waveSet;
	public bool dayCycle = true;

	public void Start() {
		walk = InitializeArray<Round>(totalRounds);
		player = FindObjectOfType<Player>();
		SetRounds(walk);
		round = startingRound;
	}

	void Update() {
			if (waveSet) {
				roundComplete = false;
				waveSet = false;
			}
	}

	// sets enemies for each wave and waves for each round
	void SetRounds(Round[] round) {
			for (int i = 0; i < totalRounds; i++) {

				float modifier = (i + 1) * roundMulitplier;

				// sets number of waves per round
				// waves increase throughout game
				//
				// round        waves
				// 1            1
				// 2-3          2
				// 4-7          3
				// 8-12         4
				// 12+          5
				// POSSIBLY ADD MORE WAVES FOR HIGHER ROUNDS
				// WILL REVISIT DURING BALANCING

				if (i == 0) {
					round [i].numWaves = 1;
				} else if (i >= 1 && i <= 2) {
					round [i].numWaves = 2;
				} else if (i >= 3 && i <= 6) {
					round [i].numWaves = 3;
				} else if (i >= 7 && i <= 11) {
					round [i].numWaves = 4;
				} else {
					round [i].numWaves = 5;
				}

				round [i]._waves = InitializeArray<Spawner.Wave> (round [i].numWaves);

				// sets the number of enemies and time between spawns for each wave
				for (int j = 0; j < round [i].numWaves; j++) {

					modifier += (j + 1) * waveMulitplier;
					switch (j) {

					case 0:
						if (round.Equals (walk))
							round [i]._waves [j].enemyCount = (int)Mathf.Ceil (walkEnemies * modifier);
						round [i]._waves [j].timeBetweenSpawns = 4;
						break;

					case 1:
						if (round.Equals (walk))
							round [i]._waves [j].enemyCount = (int)Mathf.Ceil (walkEnemies * modifier);
						round [i]._waves [j].timeBetweenSpawns = 2;
						break;

					case 2:
						if (round.Equals (walk))
							round [i]._waves [j].enemyCount = (int)Mathf.Ceil (walkEnemies * modifier);
						round [i]._waves [j].timeBetweenSpawns = 2;
						break;

					case 3:
						if (round.Equals (walk))
							round [i]._waves [j].enemyCount = (int)Mathf.Ceil (walkEnemies * modifier);
						round [i]._waves [j].timeBetweenSpawns = 1;
						break;

					default:
						if (round.Equals (walk))
							round [i]._waves [j].enemyCount = (int)Mathf.Ceil (walkEnemies * modifier);
						round [i]._waves [j].timeBetweenSpawns = .5f;
						break;
					}
				}
			}
		}


	public void RoundEnd() {
		Debug.Log("RoundEnd function");
		Debug.Log("RoundComplete function");
		round++;
		roundComplete = true;
		dayCycle = true;
		player.cycle.SetActive (false);
		if(RoundChange != null) RoundChange();
	}

	public int GetRoundNum() {
		return round + 1;
	}

	public bool GetRoundComplete() {
		return roundComplete;
	}

	public Spawner.Wave[] GetWaves() {
		waveSet = true;
		return walk[round]._waves;
	}

	// initializes objects within an array of objects
	T[] InitializeArray<T>(int length) where T : new() {
		T[] array = new T[length];
		for (int i = 0; i < length; ++i) {
			array[i] = new T();
		}

		return array;
	}

	public class Round
	{
		public int numWaves;
		public Spawner.Wave[] _waves;
	}
}

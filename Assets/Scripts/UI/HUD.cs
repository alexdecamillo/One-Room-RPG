using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	int round;
	Text[] displayText;

	SpawnManager spawner;
	Player player;

	void Start() {
		spawner = FindObjectOfType<SpawnManager>();
		player = FindObjectOfType<Player>();
		
		// subscriptions
		spawner.RoundChange += UpdateRound;
		player.TookDamage += UpdatePlayerHealth;
		player.PointChange += UpdateScore;
		displayText = GetComponentsInChildren<Text>();
	}

	void Update()
	{
		UpdateScore();
	}

	void UpdateRound() {
		round = spawner.GetRoundNum();
		//displayText[1].text = "Round: " + round;
	}

	void UpdatePlayerHealth() {
		//displayText[1].text = "Health: " + player.GetHealth();
	}

	void UpdateScore() {
        displayText[0].text = "Score: " + player.GetPoints();
	}
}
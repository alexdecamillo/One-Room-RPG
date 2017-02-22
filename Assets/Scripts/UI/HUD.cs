using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	int round;
	Text[] displayText;
	public Text cycleIndic;

	SpawnManager spawner;
	Player player;

	void Start() {
		spawner = FindObjectOfType<SpawnManager>();
		player = FindObjectOfType<Player>();
		
		// subscriptions
		spawner.RoundChange += UpdateRound;
		player.TookDamage += UpdatePlayerHealth;
		player.PointChange += UpdateScore;
		Debug.Log ("Subscriptions");
		displayText = GetComponentsInChildren<Text>();
	}

	void Update(){

		if (spawner.dayCycle == true) {
			cycleIndic.text = ("Day");
		
		} 
		else if (spawner.dayCycle == false) {
			cycleIndic.text = ("Night");
		}
	
	}

	void UpdateRound() {
		round = spawner.GetRoundNum();
		//displayText[1].text = "Round: " + round;
	}

	void UpdatePlayerHealth() {
		//displayText[1].text = "Health: " + player.GetHealth();
	}

	void UpdateScore() {
		Debug.Log ("Update Score");
		displayText[0].text = "Score: " + player.GetPoints();
	}
}
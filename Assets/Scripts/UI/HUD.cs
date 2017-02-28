using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

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
        UpdateRound();
	}

	void UpdateRound() {
		displayText[1].text = FindObjectOfType<DayCycle>().time + " " + 
            spawner.GetRoundNum() + "/" + spawner.GetMaxRound();
	}

	void UpdatePlayerHealth() {
		//displayText[1].text = "Health: " + player.GetHealth();
	}

	void UpdateScore() {
        displayText[0].text = "Score: " + player.GetPoints();
	}
}
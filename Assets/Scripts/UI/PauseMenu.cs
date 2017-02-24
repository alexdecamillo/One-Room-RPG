using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	bool paused;
	Player player;

	void Start () {
		player = FindObjectOfType<Player>();
		player.OnPause += Pause;
		gameObject.SetActive(false);
	}

	void Pause() {
		paused = !paused;
		if (paused) {
			Time.timeScale = 0.0f;
			gameObject.SetActive(true);

		} else {
			Resume();
		}
	}

	public void Resume() {
		paused = false;
		player.paused = false;
		Time.timeScale = 1.0f;
		Clear();
	}

	public void Clear() {
		gameObject.SetActive(false);
	}

	public void Settings() {

	}

	public void EndGame() {
		//Resume();
		//player.TakeDamage(float.MaxValue);
		Application.LoadLevelAsync("Title Screen");
	}

}
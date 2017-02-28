using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	bool paused;
	Player player;
	Sword sword;

	public Text stats;
    public GameObject stat;
    public GameObject pause;
    public GameObject panel;

	void Start () {
		player = FindObjectOfType<Player>();
		sword = FindObjectOfType<Sword>();
		player.OnPause += Pause;
		gameObject.SetActive(false);
		panel.SetActive(false);
	}

	void Update()
	{
		stats.text = ("Max Health: ") + player.maxHealth + ("\n") + ("Current Health: ") + player.health + ("\n") + ("Move Speed: ") + player.moveSpeed + ("\n") + ("Attack Power: ") + sword.damage;

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
        panel.SetActive(false);
        stat.SetActive(true);
        pause.SetActive(true);
        gameObject.SetActive(false);
	}

	public void Settings() {

	}

	public void Controls()
	{
		if(panel.activeSelf)
        {
            stat.SetActive(true);
            pause.SetActive(true);
            panel.SetActive(false);
		}
		else if(!panel.activeSelf)
		{
            stat.SetActive(false);
            pause.SetActive(false);
		    panel.SetActive(true);
	    }

	}
	public void RestartGame()
	{
	    //Application.LoadLevelAsync("Scene1");
        SceneManager.LoadSceneAsync("Scene1");
	}

	public void EndGame() {
		//Resume();
		//player.TakeDamage(float.MaxValue);
		//Application.LoadLevelAsync("Title Screen");
        SceneManager.LoadSceneAsync("Title Screen");
	}

}
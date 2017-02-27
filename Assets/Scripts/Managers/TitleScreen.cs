using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
 // Retrieve the name of this scene.
	[HideInInspector]
     public string sceneName;

    public GameObject title;
    public GameObject credits;

	void Update()
	{
		// Create a temporary reference to the current scene.
		 Scene currentScene = SceneManager.GetActiveScene ();
 
         sceneName = currentScene.name;


		if (Input.GetKeyDown(KeyCode.E))
		{
			StartGame();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			StartMenu();
		}
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void StartGame()
	{
	    Application.LoadLevelAsync("Scene1");
	}

	public void StartPrologue()
	{
	    Application.LoadLevelAsync("Prologue");
	}

	public void StartMenu()
	{
        //GameObject.FindGameObjectWithTag("Credits").SetActive(false);
        //GameObject.FindGameObjectWithTag("Title").SetActive(true);
        credits.SetActive(false);
        title.SetActive(true);
	}

	public void StartCredits()
	{
        //GameObject.FindGameObjectWithTag("Credits").SetActive(true);
        //GameObject.FindGameObjectWithTag("Title").SetActive(false);
        credits.SetActive(true);
        title.SetActive(false);
    }
}

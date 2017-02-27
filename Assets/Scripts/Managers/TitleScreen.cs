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


        if (Input.GetKeyDown(KeyCode.E) && (sceneName == "Prologue"))
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
	    //Application.LoadLevelAsync("Scene1");
        SceneManager.LoadSceneAsync("Scene1");
	}

	public void StartPrologue()
	{
	    //Application.LoadLevelAsync("Prologue");
        SceneManager.LoadSceneAsync("Prologue");
	}

	public void StartMenu()
	{
        credits.SetActive(false);
        title.SetActive(true);
	}

	public void StartCredits()
	{
        credits.SetActive(true);
        title.SetActive(false);
    }
}

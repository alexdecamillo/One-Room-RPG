using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
 // Retrieve the name of this scene.
	[HideInInspector]
     public string sceneName;

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
	 Debug.Log("object message");
	}

	public void StartPrologue()
	{
	 Application.LoadLevelAsync("Prologue");
	}

	public void StartMenu()
	{
		if(sceneName == "Prologue" || sceneName == "Credits"){
	 Application.LoadLevelAsync("Title Screen");
	}
	}

	public void StartCredits()
	{
	 Application.LoadLevelAsync("Credits");
	 //Debug.Log("object message");
	}
}

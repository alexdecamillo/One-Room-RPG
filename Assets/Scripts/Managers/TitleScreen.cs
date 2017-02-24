using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour {

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			StartGame();
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
	 Debug.Log("object message");
	}
}

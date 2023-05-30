using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadGame() {
		SceneManager.LoadScene ("Game");
	}

	public void LoadWin() {
		SceneManager.LoadScene ("Win");
	}
	public void LoadLose() {
		SceneManager.LoadScene ("Lose");
	}
}

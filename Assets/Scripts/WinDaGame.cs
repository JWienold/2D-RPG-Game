using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDaGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

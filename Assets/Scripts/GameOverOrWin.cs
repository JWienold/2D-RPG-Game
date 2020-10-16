using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverOrWin : MonoBehaviour {

	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		//text.text = "HI";
		if(GlobalData.health <= 0) {
			text.text = "You lose!\nScore: " + GlobalData.score;
		} else {
			text.text = "You win!\nScore: " + GlobalData.score;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

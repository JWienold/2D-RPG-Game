using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour {

	int counter = 0;
	bool canMove = false;
	public GameObject arrow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position.x < (GlobalData.camX * 32 - 14)) || (transform.position.x > (GlobalData.camX * 32 + 18))) {
			canMove = false;
		} else {
			canMove = true;
		}
		
		if((transform.position.y < (GlobalData.camY * 18 - 7)) || (transform.position.y > (GlobalData.camY * 18 + 11))) {
			canMove = false;
		}
		if(canMove) {
			if(++counter % 60 == 0) {
				GameObject temp = Instantiate(arrow,transform.position,Quaternion.identity);
				Arrow aa = temp.GetComponent<Arrow>();
				aa.v = new Vector2(0,-1);
				aa.enemy = true;
			}
		}
	}
}

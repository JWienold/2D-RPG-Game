using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

	int counter = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(++counter > 60) {
			Destroy(gameObject);
			if(!(transform.position.x < (32 - 14)) || (transform.position.x > (32 + 18)) && !(transform.position.y < (18 - 7)) || (transform.position.y > (18 + 11))) {
				GlobalData.wall = false;
			}
		}
	}
}

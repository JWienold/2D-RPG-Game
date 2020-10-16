using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
		if(other.name.Equals("Player")) {
            
            Destroy(gameObject);
            //RupeeSound.Play();
            GlobalData.hasBomb = true;
            GlobalData.bombCount += 5;
		}
	}
}

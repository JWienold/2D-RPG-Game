using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GlobalData.hasBow)
			Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
		if(other.name.Equals("Player")) {
            
            Destroy(gameObject);
            //RupeeSound.Play();
            GlobalData.hasBow = true;
            GlobalData.arrowCount+= 30;
		}
	}
}

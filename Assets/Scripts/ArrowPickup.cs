﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
		if(other.name.Equals("Player")) {
            
            Destroy(gameObject);
            GlobalData.arrowCount+= 30;
		}
	}
}

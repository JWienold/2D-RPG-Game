using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rupee : Item {
    public AudioSource RupeeSound;
    public int val;
	// Use this for initialization
	void Start () {
        RupeeSound = GameObject.FindGameObjectWithTag("Rupee").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override ItemType GetType() {
		return ItemType.Rupee;
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
		if(other.name.Equals("Player")) {
            RupeeSound.Play();
            Destroy(gameObject);
            //RupeeSound.Play();
            GlobalData.rupees += val;
            GlobalData.score += val;
		}
	}
}

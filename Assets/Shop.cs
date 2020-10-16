using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    bool playerInBox = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerInBox)
            if (Input.GetKeyDown("e"))
                Debug.Log("Test");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerInBox = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        playerInBox = false;
    }
}

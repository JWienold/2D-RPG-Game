using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartShop : MonoBehaviour {

    bool playerInBox = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInBox)
            if (Input.GetKeyDown("e") && GlobalData.rupees >= 20 && GlobalData.maxHealth != GlobalData.health)
            {
                Debug.Log("TestHeart");
                GlobalData.rupees = GlobalData.rupees - 20;
                GlobalData.health = GlobalData.health + 0.5f;
            }
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

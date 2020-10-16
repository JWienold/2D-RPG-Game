using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

    public static Item[] inventory;
    public static int rupees = 0;
    public static bool moveCamera = false;
    public static int camX = 0;
    public static int camY = 0;
    public static bool paused = false;
    public static int door = 0;
    public static float health = 6f;
    public static float maxHealth = 6f;
    public static int score = 0;
    public static bool hasBow = false;
    public static bool hasBomb = false;
    public static int bombCount = 0;
    public static int arrowCount = 0;
    public static int selectedItem = 0;
    public static bool wall = true;

    // Use this for initialization
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);

            inventory = new Item[16];
            rupees = 0;
            moveCamera = false;
            camX = 0;
            camY = 0;
            paused = false;
            door = 7;
            health = 6f;
            maxHealth = 6f;
            score = 0;
            hasBow = false;
            hasBomb = false;
            bombCount = 0;
            arrowCount = 0;
            selectedItem = 0;
            wall = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHP(float hp)
    {
        health = hp;
        maxHealth = hp;
    }
}

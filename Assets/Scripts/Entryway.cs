using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entryway : MonoBehaviour {

	public string target;
	public int door;
	public Transform blackScreenTransform;
    public AudioSource EntryWaySource;
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.name.Equals("Player")) {
            GlobalData.door = door;
            EntryWaySource.Play();
			blackScreenTransform.eulerAngles = Vector3.zero;
		}
	}
	
	public void OnTriggerStay2D(Collider2D other) {
		if(other.name.Equals("Player")) {	
			SceneManager.LoadScene(target, LoadSceneMode.Single);
		}
	}
}

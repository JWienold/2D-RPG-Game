using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
None, Rupee, Arrow
}

public class Item : MonoBehaviour {
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public virtual ItemType GetType() {
	    return ItemType.None;
	}
}

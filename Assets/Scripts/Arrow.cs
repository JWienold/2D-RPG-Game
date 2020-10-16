using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public Vector2 v = Vector2.zero;
	public bool enemy = false;
	Rigidbody2D rb2d;
	SpriteRenderer r;
	int counter = 0;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		r = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position.x < (GlobalData.camX * 32 - 14)) || (transform.position.x > (GlobalData.camX * 32 + 18))) {
			Destroy(gameObject);
		}
		
		if((transform.position.y < (GlobalData.camY * 18 - 7)) || (transform.position.y > (GlobalData.camY * 18 + 11))) {
			Destroy(gameObject);
		}
		counter++;
		rb2d.velocity = v * 16;
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(v.y, v.x)*180/Mathf.PI);
		if(counter > 300)
			Destroy(gameObject);
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
		if(enemy) {
			if(other.gameObject.name.Equals("Player")) {
				Player p = other.gameObject.GetComponent<Player>();
				Vector3 a = other.gameObject.transform.position - transform.position;
				Vector2 v = new Vector2(a.x, a.y);
				v.Normalize();
				
				p.damage(0.5f);
				p.damageVelocity = (v * 24f);
				Destroy(gameObject);
			}
			if(other.tag == "Enemy") {
				if(!other.name.Contains("Green Enemy") && !other.name.Contains("Purple Enemy"))
				{
					Destroy(gameObject);
				}
			}
		} else {
			if(other.tag == "Enemy") {
				if(other.name.Contains("Green Enemy"))
					{
						RegularEnemy e = other.gameObject.GetComponent<RegularEnemy>();
						e.damageVelocity = (v * 24f);
						e.damage(0.25f);
					}
				if(other.name.Contains("Purple Enemy"))
					{
						FastEnemy e = other.gameObject.GetComponent<FastEnemy>();
						e.damageVelocity = (v * 24f);
						e.damage(0.25f);
					}
					
					if(other.name.Contains("Miniboss"))
					{
						Miniboss e = other.gameObject.GetComponent<Miniboss>();
						e.damageVelocity = (v * 24f);
						e.damage(0.25f);
					}
				
				Destroy(gameObject);
			}
		}
	}
}

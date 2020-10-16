using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemy : MonoBehaviour {

	public Transform rupee;
	public Transform arrow;
	Rigidbody2D rb2d;
	Animator anim;
	float health = 1f;
	float speed = 2f;
	public Vector2 damageVelocity;
	Vector2 v;
	int invulnCounter = 100;
	SpriteRenderer r;
	bool goForPlayer = false;
	int stayCounter = 0;
	bool stay = false;
	bool canMove = false;
	
	int randState = 0;
	int randTimer = 0;
	
	Vector2[] moveDirs = new Vector2[4];
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D> ();
		damageVelocity = Vector2.zero;
		v = Vector2.zero;
		r = GetComponent<SpriteRenderer>();
		moveDirs[0] = new Vector2(1,0);
		moveDirs[1] = new Vector2(0,1);
		moveDirs[2] = new Vector2(0,-1);
		moveDirs[3] = new Vector2(-1,0);
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position.x < (GlobalData.camX * 32 - 14)) || (transform.position.x > (GlobalData.camX * 32 + 18))) {
			canMove = false;
		} else {
			canMove = true;
		}
		
		if((transform.position.y < (GlobalData.camY * 18 - 7)) || (transform.position.y > (GlobalData.camY * 18 + 11))) {
			canMove = false;
		}
		
		Vector2 a = (Player.loc - (Vector2)transform.position);
		goForPlayer = a.magnitude < 15;
		
		anim.enabled = canMove && !GlobalData.paused && goForPlayer;
		
		if(canMove && !GlobalData.paused) {
			damageVelocity = Vector2.Lerp(damageVelocity, Vector2.zero, 0.125f);
			if(damageVelocity.magnitude < 0.125f)
				damageVelocity = Vector2.zero;
		
			if(goForPlayer) {
				a.Normalize();
				v = a;
			} else {
				if(randTimer <= 0) {
					randTimer += Random.Range(10,55);
					randState = Random.Range(-3,4);
				} else {
					randTimer--;
					switch(randState) {
					case 0:
					case -1:
					case -2:
					case -3:
						v = Vector2.zero;
						anim.enabled = false;
						break;
					case 1:
					case 2:
					case 3:
					case 4:
 						v = moveDirs[randState-1];
						anim.enabled = true;
						break;
					}
				}
				//v = Vector2.zero;
			}
		
			r.flipX = false;
			if(v.x>Mathf.Abs(v.y) && v.x > 0) {
				anim.SetInteger("direction", 1);
				r.flipX = true;
			} else if(v.y>Mathf.Abs(v.x) && v.y > 0)
				anim.SetInteger("direction", 0);
			if(v.x<Mathf.Abs(v.y) && v.x < 0)
				anim.SetInteger("direction", 3);
			else if(v.y<Mathf.Abs(v.x) && v.y < 0)
				anim.SetInteger("direction", 2);
		
			rb2d.velocity = (v * speed + damageVelocity);
			invulnCounter++;
			if(invulnCounter < 25)
				r.enabled = !r.enabled;
			else
				r.enabled = true;
		
			if(health <= 0) {
				if(Random.Range(0,1)==0)
					Instantiate(rupee,transform.position,Quaternion.identity);
				else
					Instantiate(arrow,transform.position,Quaternion.identity);
				GlobalData.score += 50;
				Destroy(gameObject);
			}
		} else {
			rb2d.velocity = Vector2.zero;
		}
		
		if(stay) {
			stay = false;
		} else {
			stayCounter = 0;
		}
	}
	
	public void OnCollisionStay2D(Collision2D other) {
		if(other.gameObject.name.Equals("Player") && !GlobalData.paused && canMove) {
			stay = true;
			stayCounter++;
			Player p = other.gameObject.GetComponent<Player>();
			Vector3 a = other.gameObject.transform.position - transform.position;
			Vector2 v = new Vector2(a.x, a.y);
			v.Normalize();
			
			if(p.attacking) {
				damage(0.5f);
				damageVelocity = (v * -24f);
			} else {
				if(stayCounter > 3) {
					stayCounter = 0;
					p.damage(0.5f);
					p.damageVelocity = (v * 24f);
				}
			}
		}
	}
	
	public void damage(float damageAmount) {
		if(invulnCounter > 25) {
			invulnCounter = 0;
			health -= damageAmount;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	
	public float speed;
	public Rigidbody2D rb2d;
	Vector3 vec;
	Vector2 d = new Vector2(0,1);
	public static Vector2 loc;
	public Vector2 damageVelocity;
	public Animator anim;
	string mapName;
	int counter = 0;
	int gameOverCounter = 0;
	int attackCounter = 35;
	int attackLength = 12;
	bool inBuildings = false;
	bool inDungeons = false;
	public Transform pauseTransform;
	public Transform arrowTransform;
	public Transform bombTransform;
	public Transform blackScreenTransform;
	public Text text;
	public Text bombText;
	public Text arrowText;
	int invulnCounter = 100;
	public AudioClip AttackSound;
	public AudioClip GetHit;
	public AudioSource SoundEffectSource;
	AudioSource bgm;
	SpriteRenderer r;
	bool bow = false;
	
	public Image heart1;
	public Image heart2;
	public Image heart3;
	public Image heart4;
	public Image heart5;
	public Image heart6;
	
	Image[] hearts = new Image[6];
	
	public Sprite fullheart;
	public Sprite halfheart;
	public Sprite deadSprite;
	
	public Sprite shootUp;
	public Sprite shootDown;
	public Sprite shootLeft;
	public Sprite shootRight;
	public Image bowUpgrade;
	public Image bombUpgrade;
	public GameObject arrow;
	public GameObject bomb;
	public GameObject wall;
	
	public bool attacking {
		get {
		return attackCounter < attackLength;
		}
	}
	
	Camera mainCam;
	// Use this for initialization
	void Start () {
		hearts[0] = heart1;
		hearts[1] = heart2;
		hearts[2] = heart3;
		hearts[3] = heart4;
		hearts[4] = heart5;
		hearts[5] = heart6;
		speed = 4f;
		rb2d = GetComponent<Rigidbody2D> ();
		mainCam = Camera.main;
		anim = GetComponent<Animator>();
		mapName = SceneManager.GetActiveScene().name;
		blackScreenTransform.eulerAngles = Vector3.zero;
		damageVelocity = Vector2.zero;
		r = GetComponent<SpriteRenderer>();
		loc = Vector2.zero;
		bgm = mainCam.GetComponent<AudioSource>();
		switch (mapName) {
		case "Overworld":
			switch(GlobalData.door) {
			case 1:
				transform.position = new Vector3(-5.5f, -15f, -1f);
				break;
			case 2:
				transform.position = new Vector3(8.5f, -15f, -1f);
				break;
			case 3:
				transform.position = new Vector3(12.5f, -15f, -1f);
				break;
			case 4:
				transform.position = new Vector3(-7.5f, -23f, -1f);
				break;
			case 5:
				transform.position = new Vector3(35.5f, 0f, -1f);
				break;
			case 6:
				transform.position = new Vector3(-25.5f, 14f, -1f);
				break;
			case 8:
				transform.position = new Vector3(34.5f, -16f, -1f);
				break;
			default:
				transform.position = new Vector3(2.5f, 2f, -1f);
				break;
				}
			break;
		case "InBuildings":
			inBuildings = true;
			switch(GlobalData.door) {
			case 0:
				transform.position = new Vector3(4f, -3.5f, -1f);
				break;
			case 1:
				transform.position = new Vector3(41f, -33.5f, -1f);
				break;
			case 2:
				transform.position = new Vector3(-55f, -33.5f, -1f);
				break;
			case 3:
				transform.position = new Vector3(-41f, -33.5f, -1f);
				break;
			case 4:
				transform.position = new Vector3(3f, -33.5f, -1f);
				break;
			case 7:
				transform.position = new Vector3(7f, 8f, -1f);
				break;
			}
			break;
		case "InDungeons":
			inDungeons = true;
			switch(GlobalData.door) {
			case 5:
				transform.position = new Vector3(66, -19.5f, -1f);
				break;
			case 6:
				transform.position = new Vector3(2, -1.5f, -1f);
				break;
			}
			break;

        case "FinalBossArea":
            switch (GlobalData.door)
            {
            default:
                transform.position = new Vector3(34.02f, 15.52f, -1f);
                break;
            }   
            break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
		if(wall != null && !GlobalData.wall) {
			Destroy(wall);
			wall = null;
		}
		
		int i;
		
		for(i = 0; i < Mathf.Floor(GlobalData.health); i++) {
			hearts[i].enabled = true;
			hearts[i].sprite = fullheart;
		}
		if(i < 6 && Mathf.Floor(GlobalData.health) != GlobalData.health) {
			hearts[i].enabled = true;
			hearts[i++].sprite = halfheart;
		}
		for(; i < 6; i++) {
			hearts[i].enabled = false;
		}
		
		if(Input.GetKeyDown("u")) {
			GlobalData.door = 5;
			SceneManager.LoadScene("InDungeons", LoadSceneMode.Single);
		}
		
		if(Input.GetKeyDown("i")) {
			GlobalData.door = 8;
			SceneManager.LoadScene("Overworld", LoadSceneMode.Single);
		}
		
		if(Input.GetKeyDown("o")) {
			GlobalData.door = 7;
			SceneManager.LoadScene("InBuildings", LoadSceneMode.Single);
		}
		
		if(counter++ == 5)
			blackScreenTransform.eulerAngles = new Vector3(0,90,0);
		if(Input.GetKeyDown("c"))
			GlobalData.paused = !GlobalData.paused;
		
		anim.enabled=!GlobalData.paused && !bow;
		
		if(GlobalData.hasBomb) {
			bombTransform.eulerAngles = new Vector3(0,0,0);
			bombText.text = "" + GlobalData.bombCount;
			bombUpgrade.enabled = true;
		} else {
			bombTransform.eulerAngles = new Vector3(0,0,90);
			bombUpgrade.enabled = false;
		}
		
		if(GlobalData.hasBow) {
			arrowTransform.eulerAngles = new Vector3(0,0,0);
			arrowText.text = "" + GlobalData.arrowCount;
			bowUpgrade.enabled = true;
		} else {
			arrowTransform.eulerAngles = new Vector3(0,0,90);
			bowUpgrade.enabled = false;
		}
		
		if(GlobalData.paused) {
			rb2d.velocity = Vector2.zero;
			if(GlobalData.health > 0)
				pauseTransform.eulerAngles = new Vector3(0,0,0);
		} else {
			pauseTransform.eulerAngles = new Vector3(0,90,0);
			text.text = "" + GlobalData.rupees;
			int x = 0;
			int y = 0;
			if(Input.GetKey(KeyCode.UpArrow)) {
				y++;
			}
		
			if(Input.GetKey(KeyCode.LeftArrow)) {
				x--;
			}
		
			if(Input.GetKey(KeyCode.DownArrow)) {
				y--;
			}
		
			if(Input.GetKey(KeyCode.RightArrow)) {
				x++;
			}
			
			Vector2 movement = new Vector2((float)x,(float)y);
			
			if(x != 0 || y != 0)
				d = movement;
			
			if(attacking) {
				attackCounter++;
			} else {
				anim.enabled = true;
				bow = false;
				if (Input.GetKeyDown("z"))
				{
					attackCounter = 0;
					SoundEffectSource.clip = AttackSound;
					SoundEffectSource.Play();
					anim.SetBool("attacking", true);
				} else {
					anim.SetBool("attacking", false);	
				}
				
				if (Input.GetKeyDown("x"))
				{
					attackCounter = 0;
					if(GlobalData.selectedItem == 0) {
						if(GlobalData.hasBow && GlobalData.arrowCount > 0) {
							GameObject temp = Instantiate(arrow,transform.position,Quaternion.identity);
							Arrow aa = temp.GetComponent<Arrow>();
							aa.v = d;
							bow = true;
							anim.enabled = false;
							GlobalData.arrowCount--;
						}
					} else {
						if(GlobalData.hasBomb && GlobalData.bombCount > 0) {
							GameObject temp = Instantiate(bomb,transform.position,Quaternion.identity);
							GlobalData.bombCount--;
						}
					}
					//SoundEffectSource.clip = AttackSound;
					//SoundEffectSource.Play();
					//anim.SetBool("attacking", attacking);
				}
				
				if(d.x>0) {
					anim.SetInteger("direction", 1);
					if(bow)r.sprite = shootRight;
				}
			
				if(d.x<0) {
					anim.SetInteger("direction", 3);
					if(bow)r.sprite = shootLeft;
				}
			
				if(d.y>0) {
					anim.SetInteger("direction", 0);
					if(bow)r.sprite = shootUp;
				}
			
				if(d.y<0) {
					anim.SetInteger("direction", 2);
					if(bow)r.sprite = shootDown;
				}
			}

			if (movement.magnitude > 0) {
				anim.SetBool("moving", true);
				//if (inBuildings == true);

				//else if (inDungeons == true);

				//else ();
			}
			else
				anim.SetBool("moving", false);
		
			if(GlobalData.moveCamera || counter < 5)
				rb2d.velocity = Vector2.zero;
			else {
				rb2d.velocity = (movement * speed + damageVelocity);
				invulnCounter++;
				if(invulnCounter < 25)
					r.enabled = !r.enabled;
				else
					r.enabled = true;
				
				damageVelocity = Vector2.Lerp(damageVelocity, Vector2.zero, 0.125f);
				if(damageVelocity.magnitude < 0.125f)
					damageVelocity = Vector2.zero;
			}
		
			//-14 to 18
			//center x=2, left x=-30, right x=34
			if(inBuildings) {
				switch(GlobalData.door) {
				case 0:
				case 7:
					mainCam.transform.position = new Vector3(2, 2, -10);
					break;
				case 1:
					mainCam.transform.position = new Vector3(41, -28.5f, -10);
					break;
				case 2:
				case 3:
					mainCam.transform.position = new Vector3(-48, -28.5f, -10);
					break;
				case 4:
					mainCam.transform.position = new Vector3(3, -28.5f, -10);
					break;
				}
			} else {
				if(transform.position.x < (GlobalData.camX * 32 - 14)) {
					GlobalData.camX--;
					GlobalData.moveCamera = true;
				} else if(transform.position.x > (GlobalData.camX * 32 + 18)) {
					GlobalData.camX++;
					GlobalData.moveCamera = true;
				}
		
				if(transform.position.y < (GlobalData.camY * 18 - 7)) {
					GlobalData.camY--;
					GlobalData.moveCamera = true;
				} else if(transform.position.y > (GlobalData.camY * 18 + 11)) {
					GlobalData.camY++;
					GlobalData.moveCamera = true;
				}
		
				vec = new Vector3(GlobalData.camX * 32 + 2, GlobalData.camY * 18 + 2, -17);
				
				if(counter < 5)
					mainCam.transform.position = vec;
				else
					mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, vec, 0.125f);
		
				if((mainCam.transform.position - vec).magnitude < 1)
					GlobalData.moveCamera = false;
			}
		}
		loc = new Vector2(transform.position.x, transform.position.y);
		
		if(GlobalData.health <= 0) {
			gameOverCounter++;
			bgm.enabled = false;
			GlobalData.paused = true;
			anim.enabled = false;
			r.enabled = true;
			r.sprite = deadSprite;
			if(gameOverCounter >= 60)
				SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
		}
	}
	
	public void damage(float damageAmount) {
		if(invulnCounter > 25) {
			invulnCounter = 0;
			GlobalData.health -= damageAmount;
			SoundEffectSource.clip = GetHit;
			SoundEffectSource.Play();
		}
	}
}

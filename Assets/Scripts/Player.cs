using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour {
	//time
	public float t=0.4f;			//time player attack
	public float ti = 1;			//time zombie attack
	public float tj = 1;			//time zombie attack
	public int i;
	public float tS = 2;
	public float tZ = 1;
	//stats
	public float speed=3;			//speed moving
	public float atk = 20;				//dmg player
	public float hp;				//health player
	public float maxHp = 100;
	public float exp = 0;
	public float maxExp = 125;
	public int kill;
	public int level = 1;
	//references
	public GameObject zo;
	public GameObject zob;
	public Enemies x;
	public EnemyBosses xx;
	private Rigidbody2D rig;
	private SpriteRenderer rend;
	private BoxCollider2D box;
	public Sprite manw;
	public Sprite origin;
	public Gamemanager manager;
	//variable
	private bool check = false;
	private bool checkz = false;
	public bool gameOver = false;
	private List<GameObject> zos = new List<GameObject> ();
	private List<GameObject> zbos = new List<GameObject> ();
	// Use this for initialization
	void Start () {
		kill = 0;
		if (PlayerPrefs.GetFloat ("hp") <= 0) {
			hp = 100;
		}
		rig = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer> ();
		box = GetComponent<BoxCollider2D> ();
	//	Debug.Log (PlayerPrefs.GetFloat ("exp"));
		if (PlayerPrefs.GetFloat("hp") > 0) {
			speed = PlayerPrefs.GetFloat ("speed");
			atk = PlayerPrefs.GetFloat ("atk");
			hp = PlayerPrefs.GetFloat ("hp");
			maxHp = PlayerPrefs.GetFloat ("maxHp");
			exp = PlayerPrefs.GetFloat ("exp");
			maxExp = PlayerPrefs.GetFloat ("maxExp");
			kill = PlayerPrefs.GetInt ("kill");
			level = PlayerPrefs.GetInt ("level");
		}
		PlayerPrefs.DeleteKey("speed");	
		PlayerPrefs.DeleteKey("atk");	
		PlayerPrefs.DeleteKey("hp");	
		PlayerPrefs.DeleteKey("maxHp");	
		PlayerPrefs.DeleteKey("exp");	
		PlayerPrefs.DeleteKey("maxExp");	
		PlayerPrefs.DeleteKey("kill");	
		PlayerPrefs.DeleteKey("level");	
	}
	
	// Update is called once per frame
	void Update () {
		if (tS < 0) {
			if (GameObject.Find ("z" + i)) {
				zo = GameObject.Find ("z" + i);
			} else
				i++;
		} else {
			tS = tS - Time.deltaTime;
			zo = GameObject.Find ("yyyy");
		}
		if (GameObject.Find ("zb")) {
			zob = GameObject.Find ("zb");
		} else
			zob = GameObject.Find ("yyyy");
		x = zo.GetComponent<Enemies> ();
		xx = zob.GetComponent<EnemyBosses> ();
		float tempy = box.size.y;
		float horiz = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		Vector2 gerak = new Vector2 (horiz, vert);
		if (!gameOver) {
			rig.velocity = (gerak * speed);
			if (rig.velocity.x < 0) {				//move
				rend.flipX = true;
			} else if(rig.velocity.x >0)
				rend.flipX = false;

			if (Input.GetKeyDown ("space")) {		//attackinput
				rend.sprite = manw;
				box.size = new Vector2 (1.07f, tempy);				//change collider
			}
			if (t > 0) {							//attack
				t = t - 1 * Time.deltaTime;
			} else {
				rend.sprite = origin;
				box.size = new Vector2 (0.63f, tempy);				//change collider original
				t = 0.4f;
			}
		}			
		//lvl up
		if(exp>=maxExp){
			level += 1;
			maxHp = Mathf.Round(maxHp * 1.03f);
			hp = maxHp;
			maxExp = Mathf.Round(maxExp * 1.5f);
			speed = speed * 1.001f;
			atk = atk * 1.2f;
			manager.PopUpT ();
		}
		if (manager.CPop()) {
			if (ti > 0) {
				ti = ti - Time.deltaTime;
			} else {
				manager.PopUpF ();
				ti = 1f;
			}
		}
		if (check) {											//decrease hp
			if (!gameOver) {
				if (ti > 0) {
					ti = ti - Time.deltaTime;
				} else {
					float temp = x.atk;
					hp = hp - (temp * zos.Count);
					ti = 1f;
					manager.PopUpF ();
	//				Debug.Log (x.atk);
				}
			}
			check = false;
		}
		if (checkz) {											//decrease hp
			if (!gameOver) {
				if (tj > 0) {
					tj = tj - Time.deltaTime;
				} else {
					float temp1 = xx.atk;
					hp = hp - (temp1 * zbos.Count);
					tj = 1f;
					manager.PopUpF ();
					//Debug.Log (xx.atk);
				}
			}
			checkz = false;
		}
		//Constraint position
		if (transform.position.y >= 2.23f) {
			if (rig.velocity.y > 0) {
				rig.velocity = new Vector2 (0, 0);
			}
		} else if (transform.position.y <= -4.11f) {
			if (rig.velocity.y < 0) {
				rig.velocity = new Vector2 (0, 0);
			}
		}
		if (transform.position.x >= 8.53f) {
			if (rig.velocity.x > 0) {
				rig.velocity = new Vector2 (0, 0);
			}
		} else if (transform.position.x <= -8.51f) {
			if (rig.velocity.x < 0) {
				rig.velocity = new Vector2 (0, 0);
			}
		}

		if (hp <= 0) {							//Game over
			manager.GameOver ();
			gameOver = true;
			hp = 0;
			if (ti > 0) {
				ti = ti - Time.deltaTime;
			} else {
				SceneManager.LoadScene ("mainmenu");
			}
		}
		if (PlayerPrefs.GetInt ("zkill") < kill) {
			PlayerPrefs.SetInt ("zkill", kill);
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name.Contains ("z")) {
			check = true;
		}
		if (col.gameObject.name.Equals ("zb")) {
			checkz = true;
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name.Contains ("z")) {
			if (col.gameObject.name.Equals ("zb")) {
				zbos.Add (col.gameObject);
			}
			else 
				zos.Add (col.gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.name.Contains ("z")) {
			if (col.gameObject.name.Equals ("zb")) {
				zbos.Remove (col.gameObject);

			}
			else 
				zos.Remove (col.gameObject);
		}
	}
//	void OnDisable(){
//	}
		
}

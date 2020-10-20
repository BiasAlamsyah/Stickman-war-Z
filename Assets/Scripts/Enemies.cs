using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class Enemies : MonoBehaviour {
	//stats
	public float speed;
	public float  atk;
	public float hp;
	public float exp;
	public Stats[] stat = new Stats[9999];
	//references
	public GameObject player;
	private Vector2 posplayer;
	private Player playscript;
	private SpriteRenderer rend;
	private Rigidbody2D rig;
	//variable
	private bool check;
	public int i=0;
	public int j=0;
	public float t;
	public int temp;
	// Use this for initialization
	void Start () {
		check = false;
		stat [i] = new Stats(hp, "z" + i);
		i++;
		rig = GetComponent<Rigidbody2D> ();
		rend = GetComponent<SpriteRenderer> ();
		playscript = player.GetComponent<Player> ();

	}
	
	// Update is called once per frame
	void Update () {
		posplayer = new Vector2 (player.transform.position.x,player.transform.position.y);				//ai find player
		if (posplayer.x - transform.position.x > 0 & posplayer.y - transform.position.y > 0) {
			rig.velocity = new Vector2 (speed, speed);
		} else if (posplayer.x - transform.position.x < 0 & posplayer.y - transform.position.y > 0)
			rig.velocity = new Vector2 (speed * -1, speed);
		else if (posplayer.y - transform.position.y < 0 & posplayer.x - transform.position.x > 0) {
			rig.velocity = new Vector2 (speed, speed*-1);
		} else if (posplayer.y - transform.position.y < 0 & posplayer.x - transform.position.x < 0)
			rig.velocity = new Vector2 (speed * -1, speed * -1);
		else if (posplayer.x - transform.position.x > 0 & posplayer.y - transform.position.y == 0)
			rig.velocity = new Vector2 (speed, 0);
			else if (posplayer.x - transform.position.x < 0 & posplayer.y - transform.position.y == 0)
			rig.velocity = new Vector2 (speed *-1 , 0);
		else if (posplayer.y - transform.position.y > 0 & posplayer.x - transform.position.x == 0)
			rig.velocity = new Vector2 (0,speed);
		else if (posplayer.y - transform.position.y < 0 & posplayer.x - transform.position.x == 0)
			rig.velocity = new Vector2 (0,speed * -1 );
		if (transform.position.y < -10) {
			rig.velocity = new Vector2 (0, 0);
		}
		if (rig.velocity.x < 0) {						// flip sprite
			rend.flipX = true;
		} else if(rig.velocity.x >0)
			rend.flipX = false;
		if (!playscript.gameOver) {
			if (Input.GetKeyDown ("space")) {						//input, to destroy when hp 0
				if (check) {		
					Player temp2 = player.GetComponent<Player> ();
					float temp3 = temp2.atk;
					stat [temp].hp = stat [temp].hp - temp3;
					if (stat [temp].hp <= 0) {
						DestroyObject (this.gameObject);
						playscript.exp = playscript.exp + exp;
						playscript.kill += 1;
					}
					//	Debug.Log (stat [temp].hp);	
				}
				check = false;
			}
		}

	}
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name.Equals("man")) {
			check= true;
			temp = GetIndex (this.gameObject.name);
		}	

	}
	//function or tool
	public int GetIndex(string nam){							//get index
		return 0;
		while (j >= 0) {
			if (nam == stat[j].name) {
				return j;
			} else
				j++;
		}
	}

	public class Stats{
		public float hp;
		public string name;
		public Stats(float h,string n){
			hp = h;
			name = n;
		}

	}
}

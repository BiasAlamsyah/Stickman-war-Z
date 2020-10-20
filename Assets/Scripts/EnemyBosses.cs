using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBosses : MonoBehaviour {

	public float speed = 0.5f;
	public float  atk = 20;
	public float hp;
	//reference
	public GameObject player;
	private Vector2 posplayer;
	private Player playscript;
	private SpriteRenderer rend;
	private Rigidbody2D rig;
	//variable
	private bool check;
	private float timer = 2f;
	public int i=0;
	public int j=0;
	public float t;
	public int temp;
	// Use this for initialization
	void Start () {
		check = false;
		i++;
		rig = GetComponent<Rigidbody2D> ();
		rend = GetComponent<SpriteRenderer> ();
		playscript = player.GetComponent<Player> ();
	}

	// Update is called once per frame
	void Update () {
		if (timer <= 0) {
			timer = 2f;
			posplayer = new Vector2 (player.transform.position.x,player.transform.position.y);				//ai find player
		}else timer = timer - Time.deltaTime;
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
		if (transform.position.x-posplayer.x < 0) {						// flip sprite
			rend.flipX = false;
		} else if(transform.position.x-posplayer.x >0)
			rend.flipX = true;

		if (Input.GetKeyDown ("space")) {						//input, to destroy when hp 0
			if (check){		
				Player temp2 = player.GetComponent<Player> ();
				float temp3 = temp2.atk;
				hp = hp - temp3;
				if (hp <= 0) {
					DestroyObject (this.gameObject);
					playscript.exp = playscript.exp + 80;
					playscript.kill += 1;
				}
			}
			check = false;
		}

	}
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name.Equals("man")) {
			check= true;
		}	

	}
	//function or tool
}

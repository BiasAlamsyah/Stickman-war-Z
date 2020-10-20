using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zGenerator : MonoBehaviour {
	public GameObject zombie;
	public Sprite sprit;
	public Sprite sprit2;
	public float hpZ = 50;
	public float hpZb = 800;
	public float atkZ = 10;
	public float atkZb = 40;
	public float exp =25;
	public float speed = 1;
	private float t=2;
	private float tim;
	private int i=1;
	public GameObject a;
	float randx;
	float randy;
	// Use this for initialization
	void Start () {
		tim = 60;
		if (PlayerPrefs.GetFloat("hpz") > 0) {
			hpZ = PlayerPrefs.GetFloat ("hpz");
			hpZb = PlayerPrefs.GetFloat ("hpzb");
			atkZ = PlayerPrefs.GetFloat ("atkz");
			atkZb = PlayerPrefs.GetFloat ("atkzb");
			exp = PlayerPrefs.GetFloat ("exp");
			speed = PlayerPrefs.GetFloat ("zspeed");
		}
		PlayerPrefs.DeleteKey ("hpz");
		PlayerPrefs.DeleteKey ("hpzb");
		PlayerPrefs.DeleteKey ("atkz");
		PlayerPrefs.DeleteKey ("atkzb");
		PlayerPrefs.DeleteKey ("zspeed");
		PlayerPrefs.DeleteKey ("exp");
	}

	// Update is called once per frame
	void Update () {
		int randz = Random.Range (1, 3);

		if (randz == 1) {
			randx = -10.5f;
			randy = Random.Range (-6.57f, 2.6f);
		}
		else if (randz == 2) {
			randx = Random.Range (-9.11f, 9.4f);
			randy = -6.57f;
		}
		else if (randz == 3) {
			randx = 10.5f;
			randy = Random.Range (-6.57f, 2.6f);
		}
		//zombie generate
		if (t > 0) {
			t = t - Time.deltaTime;

		} else if (t < 0) {
			t = 2;
			GenerateZ();
		}
		//boss generate
		tim += Time.fixedDeltaTime;
		if(tim>=60){
			tim = 0;
			GenerateZB();
		}
	}

	public void GenerateZ(){
		a= new GameObject ("z" + i);
		a.AddComponent<Rigidbody2D> ();
		a.AddComponent<Enemies> ();
		a.AddComponent<BoxCollider2D> ();
		a.AddComponent<SpriteRenderer> ();
		Rigidbody2D aa = a.GetComponent<Rigidbody2D> ();
		aa.mass = 0.0001f;aa.gravityScale = 0;
		aa.freezeRotation = true;
		SpriteRenderer ab = a.GetComponent<SpriteRenderer> ();
		ab.sprite = sprit;
		Transform ac = a.GetComponent<Transform> ();
		ac.transform.position = new Vector2 (randx, randy);
		BoxCollider2D ad = a.GetComponent<BoxCollider2D> ();
		ad.offset=new Vector2(-0.02f,-0.89f);
		ad.size=new Vector2(0.62f,0.27f);
		Enemies ae = a.GetComponent<Enemies> ();
		ae.player = GameObject.Find ("man");
		ae.hp = hpZ;
		ae.atk = atkZ;
		ae.speed = speed;
		ae.exp = exp;
		i++;
	}

	public void GenerateZB(){
		a= new GameObject("zb");
		a.AddComponent<Rigidbody2D> ();
		a.AddComponent<EnemyBosses> ();
		a.AddComponent<BoxCollider2D> ();
		a.AddComponent<SpriteRenderer> ();
		Rigidbody2D aa = a.GetComponent<Rigidbody2D> ();
		aa.mass = 0.0001f;aa.gravityScale = 0;
		aa.freezeRotation = true;
		SpriteRenderer ab = a.GetComponent<SpriteRenderer> ();
		ab.sprite = sprit2;
		Transform ac = a.GetComponent<Transform> ();
		ac.transform.position = new Vector2 (randx, randy);
		ac.transform.localScale = new Vector3 (1.7f,1.7f,1.7f);
		BoxCollider2D ad = a.GetComponent<BoxCollider2D> ();
		ad.offset=new Vector2(-0.25f,-0.75f);
		ad.size=new Vector2(0.6f,0.7f);
		EnemyBosses ae = a.GetComponent<EnemyBosses> ();
		ae.player = GameObject.Find ("man");
		ae.hp = hpZb;
		ae.atk = atkZb;
	}
}
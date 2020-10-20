using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Flevel : MonoBehaviour {
	public GameObject man; 			//player
	public GameObject script;		//script manager
	private zGenerator zG;
	public static int floor = 1;
	private Player play;
	// Use this for initialization
	void Start () {
		man = GameObject.Find ("man");
		zG = script.GetComponent<zGenerator> ();
		play = man.GetComponent<Player> ();
		if (PlayerPrefs.GetInt ("floor")>floor) {
			floor = PlayerPrefs.GetInt ("floor");
		}
		PlayerPrefs.DeleteKey ("floor");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (floor);
		if (play.gameOver) {
			if (floor > PlayerPrefs.GetInt ("flvl")) {
				PlayerPrefs.SetInt ("flvl", floor);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name.Equals ("man")) {
			FloorSet();
			PlayerPrefs.SetFloat("speed",play.speed);
			PlayerPrefs.SetFloat("atk",play.atk);
			PlayerPrefs.SetFloat("hp",play.hp);
			PlayerPrefs.SetFloat("maxHp",play.maxHp);
			PlayerPrefs.SetFloat("exp",play.exp);
			PlayerPrefs.SetFloat("maxExp",play.maxExp);
			PlayerPrefs.SetInt("kill",play.kill);
			PlayerPrefs.SetInt("level",play.level);
			SceneManager.LoadScene(gameObject.name);
		}
	}

	void FloorSet(){
		string a = gameObject.name;
		string[] x = Regex.Split(a,@"\D+");
		foreach (string val in x) {
			int num;
			if (int.TryParse (val, out num)) {
				if (num > floor) {
					PlayerPrefs.SetFloat ("hpzb", zG.hpZb * 4);
					PlayerPrefs.SetFloat ("hpz", zG.hpZ * 4);
					PlayerPrefs.SetFloat ("atkz", zG.atkZ * 1.5f);
					PlayerPrefs.SetFloat ("atkzb", zG.atkZb * 1.5f);
					PlayerPrefs.SetFloat ("exp", zG.exp * 1.5f);
					PlayerPrefs.SetFloat ("zspeed", zG.speed * 1.2f);
					PlayerPrefs.SetInt ("floor", floor+1);

				} else if (num < floor) {
					PlayerPrefs.SetFloat ("hpzb", zG.hpZb / 4);
					PlayerPrefs.SetFloat ("hpz", zG.hpZ / 4);
					PlayerPrefs.SetFloat ("atkz", zG.atkZ / 3);
					PlayerPrefs.SetFloat ("atkzb", zG.atkZb / 3);
					PlayerPrefs.SetInt ("floor", floor-1);
					PlayerPrefs.SetFloat ("exp", zG.exp / 1.5f);
					PlayerPrefs.SetFloat ("zspeed", zG.speed / 1.2f);

				}
			}
		}
	}
}

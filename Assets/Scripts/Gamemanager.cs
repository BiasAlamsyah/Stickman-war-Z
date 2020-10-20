using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour {
	// Use this for initialization
	public Text hp;
	public Text exp;
	public Text popUP;
	public Text level;
	public Text kill;
	public Player play;
	void Start () {
		play = play.GetComponent<Player> ();
		popUP.GetComponent<Text>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		hp.text = "HP \t"+play.hp.ToString () +" / "+play.maxHp.ToString();
		exp.text = "EXP \t" + play.exp.ToString () + " / " + play.maxExp.ToString ();
		level.text = "Level \t" + play.level.ToString ();
		kill.text = "Z-Kill \r\n" + play.kill.ToString ();
	}
	public void GameOver(){
		popUP.GetComponent<Text>().enabled = true;
		popUP.text = "GAMEOVER";
	}
	public void PopUpT(){
		popUP.GetComponent<Text>().enabled = true;
	}
	public void PopUpF(){
		popUP.GetComponent<Text> ().enabled = false;
	}
	public bool CPop(){
		if (popUP.GetComponent<Text> ().enabled) {
			return true;
		} else
			return false;
	}
}

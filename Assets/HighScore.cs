using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {
	public Text hScore;
	public Text zK;
	// Use this for initialization
	void Start () {
	}
	void Awake(){
		Debug.Log ("awake");
		zK.text = PlayerPrefs.GetInt ("zkill").ToString ();
		hScore.text = PlayerPrefs.GetInt ("flvl").ToString();
	}
	// Update is called once per frame
}

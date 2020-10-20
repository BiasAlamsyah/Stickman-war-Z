using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

	// Use this for initialization

	public void OnMouseClick(string scene){
		SceneManager.LoadScene (scene);
	}
	public void exit(){
		Application.Quit ();
	}
}

using UnityEngine;
using System.Collections;

public class StartBtn : MonoBehaviour {

	void OnMouseDown(){
		gameObject.transform.Translate (Vector3.down*0.03f);
	}
	void OnMouseUp(){
		gameObject.transform.Translate (Vector3.up*0.03f);
	}
	void OnMouseUpAsButton(){
		GetComponent<AudioSource>().Play ();
		Application.LoadLevel (0);
	}
}

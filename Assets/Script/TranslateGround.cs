using UnityEngine;
using System.Collections;

public class TranslateGround : MonoBehaviour {

	private int speed=2;
	public bool move = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			gameObject.transform.Translate (Vector3.left * speed * Time.deltaTime);
			if (gameObject.transform.position.x < -6.72f) {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 13.44f, gameObject.transform.position.y, gameObject.transform.position.z);
			}
		}
	}
}

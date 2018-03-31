using UnityEngine;
using System.Collections;

public class PipeController : MonoBehaviour {

	private int speed = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager._instance.gameState == GameManager.GameState.running) {
			gameObject.transform.Translate (Vector3.left * speed * Time.deltaTime);
			if (gameObject.transform.position.x < -6) {
				Destroy (gameObject);
			}
		}
	}
}

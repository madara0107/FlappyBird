using UnityEngine;
using System.Collections;

public class PipUpAndDown : MonoBehaviour {

	public Collider2D colli;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager._instance.gameState == GameManager.GameState.over)
        {
            colli.enabled = false;
        }
        
	}
}

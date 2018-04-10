using UnityEngine;
using System.Collections;

public class pipUpAndDown : MonoBehaviour {

    public Collider2D collider;

	void Start () {
	
	}
	
	void Update () {
        if (gameManager._instance.gameState == gameManager.GameState.over)
        {
            collider.enabled = false;
        }
	}
}

using UnityEngine;
using System.Collections;

public class pipeController : MonoBehaviour {

    private int speed = 2;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (gameManager._instance.gameState == gameManager.GameState.running)
        {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (gameObject.transform.position.x < -6)
            {
                Destroy(gameObject);
            }
        }
	
	}
}

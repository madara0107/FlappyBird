using UnityEngine;
using System.Collections;

public class translateGround : MonoBehaviour {

    private int speed = 2;
    public bool move = true;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (move)
        {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (gameObject.transform.position.x < -6.72)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 13.44f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
	}
}

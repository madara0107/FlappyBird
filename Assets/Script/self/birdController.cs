using UnityEngine;
using System.Collections;

public class birdController : MonoBehaviour {

    //小鸟的游戏对象
    public GameObject birdImage;
    //要播放的飞翔动画（红，黄，或蓝）
    public Sprite[] birdSprite;
    //存储小鸟的动画
    public Sprite[] birdFlySprite;
    //获取小鸟身上的SpriteRenderer组件
    private SpriteRenderer birdRenderer;

    private float birdFlyTimer = 0f;
    private float birdMove_menu = 0.01f;

    private int birdMoveCount_menu = 0; 
    private int flyAnimationFrameIndex;
    private int flyAnimationSpeed = 15;

    private AudioSource[] audioSource;


	void Start () {
	    birdRenderer = birdImage.GetComponent<SpriteRenderer>();
        audioSource = this.transform.GetComponents<AudioSource>();

        int r = Random.Range(1, 4);
        switch (r)
        {
            case 1:
                birdSprite[0] = birdFlySprite[0];
                birdSprite[1] = birdFlySprite[1];
                birdSprite[2] = birdFlySprite[2];
                break;
            case 2:
                birdSprite[0] = birdFlySprite[3];
                birdSprite[1] = birdFlySprite[4];
                birdSprite[2] = birdFlySprite[5];
                break;
            case 3:
                birdSprite[0] = birdFlySprite[6];
                birdSprite[1] = birdFlySprite[7];
                birdSprite[2] = birdFlySprite[8];
                break;
            default:
                break;
        }
        birdRenderer.sprite = birdSprite[0];
	}
	
	void Update () {
        Debug.Log(gameManager._instance.gameState);

        if (gameManager._instance.gameState == gameManager.GameState.running)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
                audioSource[0].Play();
            }
            //birdImage
            birdImage.transform.eulerAngles = new Vector3(0, 0, gameObject.GetComponent<Rigidbody2D>().velocity.y * 10);
        }

        //小鸟死亡
        if (gameManager._instance.gameState == gameManager.GameState.over)
        {
            birdImage.transform.Rotate(Vector3.back * 400 * Time.deltaTime);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

        //不能旋转过头
        if (birdImage.transform.eulerAngles.z < 270 && birdImage.transform.eulerAngles.z > 180)
        {
            birdImage.transform.eulerAngles = new Vector3(0, 0, 270);
        }

        //不能飞太高
        if (gameObject.transform.position.y > 5)
        {
            //限制最高距离
            gameObject.transform.position = new Vector3(-1, 5 , 0);
            //将速度设为0
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (gameManager._instance.gameState != gameManager.GameState.over)
        {    
            //播放动画
            birdFlyTimer += Time.deltaTime;
            flyAnimationFrameIndex = (int)(birdFlyTimer * flyAnimationSpeed) % 3;
            birdRenderer.sprite = birdSprite[flyAnimationFrameIndex];
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y < -5)
            {
                birdRenderer.sprite = birdSprite[0];
            }
        }
        else
        {
            birdRenderer.sprite = birdSprite[0];
        }

        if (gameManager._instance.gameState == gameManager.GameState.menu)
        {
            birdMoveCount_menu++;
            if (birdMoveCount_menu > 20)
            {
                birdMoveCount_menu = 0;
                birdMove_menu = -birdMove_menu;
            }
            gameObject.transform.Translate(Vector3.up * birdMove_menu);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "addScore")
        {
            gameManager._instance.score++;
            audioSource[3].Play();
        }  
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "barrier" && gameManager._instance.gameState != gameManager.GameState.over)
        {
            gameManager._instance.gameState = gameManager.GameState.over;
            audioSource[1].Play();
            audioSource[2].Play();
        }
    }
}

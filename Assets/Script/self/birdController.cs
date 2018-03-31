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
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
            audioSource[0].Play();
        }
        //birdImage
        birdImage.transform.eulerAngles = new Vector3(0, 0, gameObject.transform.GetComponent<Rigidbody2D>().velocity.y * 10);
	    

        //不能飞太高
        if (gameObject.transform.position.y > 5)
        {
            gameObject.transform.position = new Vector3(-1, 5 , 0);
        }
    }
}

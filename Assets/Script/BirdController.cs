using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {

	public GameObject birdImage;
	private SpriteRenderer birdRender;
	public Sprite[] birdSprite;//要播放的动画
	public Sprite[] birdFlySprite; //存储小鸟的动画
	private float birdFlyTimer=0;
	private float birdMove_menu = 0.01f;
	private int birdMoveCount_menu = 0;
	private int flyAnimationFrameIndex;
	private int flyAnimationSpeed = 15;
	private AudioSource[] audioSource;

	// Use this for initialization
	void Start () {
		birdRender = birdImage.GetComponent<SpriteRenderer> ();
		audioSource = gameObject.GetComponents<AudioSource>();

		int _randomNum = Random.Range (1,4);
		switch (_randomNum) {
		case 1:
			birdSprite[0]=birdFlySprite[0];
			birdSprite[1]=birdFlySprite[1];
			birdSprite[2]=birdFlySprite[2];
			break;
		case 2:
			birdSprite[0]=birdFlySprite[3];
			birdSprite[1]=birdFlySprite[4];
			birdSprite[2]=birdFlySprite[5];
			break;
		case 3:
			birdSprite[0]=birdFlySprite[6];
			birdSprite[1]=birdFlySprite[7];
			birdSprite[2]=birdFlySprite[8];
			break;
		}
		birdRender.sprite=birdSprite[0];
	}
	
	// Update is called once per frame
	void Update () {
		//控制小鸟旋转
		if (GameManager._instance.gameState == GameManager.GameState.running) {
			if (Input.GetMouseButtonDown (0)) {
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
				audioSource[0].Play();
			}
			birdImage.transform.eulerAngles = new Vector3 (0, 0, gameObject.GetComponent<Rigidbody2D>().velocity.y * 10);
		}
		//小鸟死亡
		//小鸟死亡时面朝下
		if (GameManager._instance.gameState == GameManager.GameState.over) {
			birdImage.transform.Rotate(Vector3.back*400*Time.deltaTime);
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,gameObject.GetComponent<Rigidbody2D>().velocity.y);
		}

		//不能旋转过头
		if (birdImage.transform.eulerAngles.z < 270 && birdImage.transform.eulerAngles.z > 180) {
			birdImage.transform.eulerAngles = new Vector3(0,0,270);
		}

		//不能飞太高
		if (gameObject.transform.position.y > 5) {
			gameObject.transform.position = new Vector3(-1,5,0);
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

		//小鸟动画
		if (GameManager._instance.gameState != GameManager.GameState.over) {
			birdFlyTimer += Time.deltaTime;
			flyAnimationFrameIndex = (int)(birdFlyTimer * flyAnimationSpeed) % 3;
			birdRender.sprite = birdSprite [flyAnimationFrameIndex];
			if (gameObject.GetComponent<Rigidbody2D>().velocity.y < -5) {
				birdRender.sprite = birdSprite [0];
			}
		} else {
			birdRender.sprite = birdSprite [0];
		}
		if (GameManager._instance.gameState == GameManager.GameState.menu) {
			birdMoveCount_menu++;
			if(birdMoveCount_menu>20){
				birdMoveCount_menu=0;
				birdMove_menu = -birdMove_menu;
			}
			gameObject.transform.Translate(Vector3.up*birdMove_menu);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "addScore"){
			GameManager._instance.score++;
			audioSource[3].Play();
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "barrier" && GameManager._instance.gameState!=GameManager.GameState.over) {
			GameManager._instance.gameState = GameManager.GameState.over;
			audioSource[1].Play();
			audioSource[2].Play();
		}
	}
}

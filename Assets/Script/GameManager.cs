using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public enum GameState
	{
		menu,
		running,
		over
	}
	public static GameManager _instance;
	public GameState gameState = GameState.menu;
	public GameObject pipe;
	public GameObject effect;
	public int score = 0;
	private bool gameover=false;
	public Sprite[] numberSprite;
	public Sprite[] backgroundSprite;
	private SpriteRenderer number1Render;
	private SpriteRenderer number2Render;
	private SpriteRenderer backgroundRender;

	void Awake(){
//		PlayerPrefs.SetInt ("topScore",0);
		_instance = this;
		number1Render = GameObject.Find ("number_1").GetComponent<SpriteRenderer> ();
		number2Render = GameObject.Find ("number_2").GetComponent<SpriteRenderer> ();
		backgroundRender = GameObject.Find ("Background").GetComponent<SpriteRenderer>();
		randomBackground ();
	}

	void Update () {
		//游戏开始
		if (gameState == GameState.menu && Input.GetMouseButtonDown (0)) {
			GameObject.Find ("Bird").GetComponent<Rigidbody2D>().gravityScale = 1.5f;
			InvokeRepeating ("createPipes", 0.2f, 1.5f);
			StartCoroutine("startUIDisappear");
			gameState = GameState.running;
		}

		//显示分数
		if (score > 99) {
			gameState = GameState.over;
		} else {
			int number1 = score%10;
			int number2 = (score - number1)/10;
			number1Render.sprite=numberSprite[number1];
			if(number2!=0){
				number2Render.enabled=true;
				number2Render.sprite=numberSprite[1];
			}else{
				number2Render.enabled=false;
			}
		}

		//游戏结束
		if (gameState == GameState.over) {
			CancelInvoke("createPipes");
			GameObject.Find ("ground_1").GetComponent<TranslateGround>().move = false;
			GameObject.Find ("ground_2").GetComponent<TranslateGround>().move = false;
			if(!gameover){
				GameoverUI._instance.SendMessage("showUI");
				StartCoroutine ("createDeathEffect");
				GameObject.Find("ShowScore").SetActive(false);
				gameover=!gameover;
			}
		}

		//返回键退出游戏
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void createPipes(){
		GameObject newPipe = Instantiate (pipe, new Vector3 (6, 0, 0), Quaternion.identity) as GameObject;
		newPipe.transform.Translate (Vector3.up * Random.Range(-1.2f,2.3f));
	}

	IEnumerator createDeathEffect(){
		GameObject _effect = Instantiate(effect) as GameObject;
		yield return new WaitForSeconds(0.02f);
		_effect.GetComponent<SpriteRenderer> ().color = new Color32 (255,255,255,140);
		yield return new WaitForSeconds(0.02f);
		_effect.GetComponent<SpriteRenderer> ().color = new Color32 (255,255,255,100);
		yield return new WaitForSeconds(0.02f);
		_effect.GetComponent<SpriteRenderer> ().color = new Color32 (255,255,255,50);
		yield return new WaitForSeconds(0.02f);
		Destroy(_effect);
	}

	IEnumerator startUIDisappear(){
		SpriteRenderer infoIma = GameObject.Find ("info").GetComponent<SpriteRenderer> ();
		SpriteRenderer readyIma = GameObject.Find ("ready").GetComponent<SpriteRenderer> ();

		infoIma.color = new Color32 (255,255,255,200);
		readyIma.color = new Color32 (255,255,255,200);
		yield return new WaitForSeconds(0.04f);
		infoIma.color = new Color32 (255,255,255,160);
		readyIma.color = new Color32 (255,255,255,160);
		yield return new WaitForSeconds(0.04f);
		infoIma.color = new Color32 (255,255,255,120);
		readyIma.color = new Color32 (255,255,255,120);
		yield return new WaitForSeconds(0.04f);
		infoIma.color = new Color32 (255,255,255,80);
		readyIma.color = new Color32 (255,255,255,80);
		yield return new WaitForSeconds(0.04f);
		infoIma.color = new Color32 (255,255,255,40);
		readyIma.color = new Color32 (255,255,255,40);
		yield return new WaitForSeconds(0.04f);
		infoIma.enabled = false;
		readyIma.enabled = false;
	}

	private void randomBackground(){
		int _randomInt = Random.Range (1, 3);
		if (_randomInt == 1) {
			backgroundRender.sprite = backgroundSprite [0];
		} else {
			backgroundRender.sprite = backgroundSprite [1];
		}
	}
}

using UnityEngine;
using System.Collections;

public class GameoverUI : MonoBehaviour {

	public static GameoverUI _instance;
	public GameObject gameoverIma;
	public GameObject scoreBox;
	public GameObject startBtn;
	public GameObject rankBtn;
	private bool scoreBoxMove = false;
	private SpriteRenderer scoreNum1Ima;
	private SpriteRenderer scoreNum2Ima;
	private SpriteRenderer topScoreNum1Ima;
	private SpriteRenderer topScoreNum2Ima;
	private SpriteRenderer newIma;
	private SpriteRenderer medalIma;
	public Sprite[] numberSprite;
	public Sprite[] medalSprite;
	private int scoreCount = 0;
	private int topScore;

	void Awake(){
		_instance = this;
		scoreNum1Ima = GameObject.Find ("scoreNumber_1").GetComponent<SpriteRenderer> ();
		scoreNum2Ima = GameObject.Find ("scoreNumber_2").GetComponent<SpriteRenderer> ();
		topScoreNum1Ima = GameObject.Find ("topScoreNumber_1").GetComponent<SpriteRenderer> ();
		topScoreNum2Ima = GameObject.Find ("topScoreNumber_2").GetComponent<SpriteRenderer> ();
		newIma = GameObject.Find ("newInBox").GetComponent<SpriteRenderer> ();
		medalIma = GameObject.Find ("medalInBox").GetComponent<SpriteRenderer> ();
	}

	void showUI(){
		gameObject.transform.Translate(Vector3.right*7);
		StartCoroutine ("UIControl");
	}

	void Update(){
		float speed = 17f;
		if (scoreBoxMove) {
			scoreBox.transform.Translate (Vector3.up * speed * Time.deltaTime);
		}
	}

	IEnumerator UIControl(){
		yield return new WaitForSeconds(0.8f);
		gameoverIma.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(0.02f);
		gameoverIma.transform.Translate (Vector3.up*0.05f);
		yield return new WaitForSeconds(0.02f);
		gameoverIma.transform.Translate (Vector3.up*0.05f);
		yield return new WaitForSeconds(0.02f);
		gameoverIma.transform.Translate (Vector3.up*0.05f);
		yield return new WaitForSeconds(0.02f);
		gameoverIma.transform.Translate (Vector3.up*0.05f);
		yield return new WaitForSeconds(0.02f);
		gameoverIma.transform.Translate (Vector3.down*0.05f);
		yield return new WaitForSeconds(0.02f);
		gameoverIma.transform.Translate (Vector3.down*0.05f);
		yield return new WaitForSeconds(0.2f);
		//显示分数框
		topScore = PlayerPrefs.GetInt ("topScore", 0);
		topScoreInBoxShow (topScore);

		scoreBoxMove = true;
		yield return new WaitForSeconds(0.4f);
		scoreBoxMove = false;
		yield return new WaitForSeconds(0.2f);
		//统计分数
		InvokeRepeating ("countScore",0.02f,0.05f);

	}

	void countScore(){
		if (scoreCount <= GameManager._instance.score) {
			int scoreNum1 = scoreCount % 10;
			int scoreNum2 = (scoreCount - scoreNum1) / 10;
			scoreNum1Ima.sprite = numberSprite [scoreNum1];
			if (scoreNum2 != 0) {
				scoreNum2Ima.enabled = true;
				scoreNum2Ima.sprite = numberSprite [scoreNum2];
			} else {
				scoreNum2Ima.enabled = false;
			}
			scoreCount++;
		} else {
			if(GameManager._instance.score>topScore){
				PlayerPrefs.SetInt("topScore",GameManager._instance.score);
				topScore=PlayerPrefs.GetInt("topScore",0);
				topScoreInBoxShow(topScore);
				newIma.enabled=true;
			}
			//颁发奖章
			if(GameManager._instance.score>=10&&GameManager._instance.score<=30){
				medalIma.sprite = medalSprite[0];
				medalIma.enabled=true;
				GameObject.Find("MedalEffectSpawn").SendMessage("showMedalEffect");
			}else if(GameManager._instance.score>30){
				medalIma.sprite = medalSprite[1];
				medalIma.enabled=true;
				GameObject.Find("MedalEffectSpawn").SendMessage("showMedalEffect");
			}

			//显示按钮
			startBtn.SetActive(true);
			rankBtn.SetActive(true);
			CancelInvoke ("countScore");
		}
	}

	private void topScoreInBoxShow(int _score){
		int topScoreNum1 = _score % 10;
		int topScoreNum2 = (_score - topScoreNum1) / 10;
		topScoreNum1Ima.sprite=numberSprite[topScoreNum1];
		if (topScoreNum2 != 0) {
			topScoreNum2Ima.enabled=true;
			topScoreNum2Ima.sprite = numberSprite [topScoreNum2];
		} else {
			topScoreNum2Ima.enabled=false;
		}
	}
}

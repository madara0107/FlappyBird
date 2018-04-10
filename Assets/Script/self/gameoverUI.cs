using UnityEngine;
using System.Collections;

public class gameoverUI : MonoBehaviour
{

    public static gameoverUI _instance;
    //游戏结束的图片
    public GameObject gameoverIma;
    //计分板
    public GameObject scoreBox;
    //开始按钮
    public GameObject startBtn;
    //排行榜按钮
    public GameObject rankBtn;
    //是否移动计分板
    private bool scoreBoxMove = false;
    //分数个位图片
    private SpriteRenderer scoreNum1Ima;
    //分数十位图片
    private SpriteRenderer scoreNum2Ima;
    //总分个位图片
    private SpriteRenderer topScoreNum1Ima;
    //总分十位图片
    private SpriteRenderer topScoreNum2Ima;
    //新纪录图片
    private SpriteRenderer newIma;
    //奖牌图片
    private SpriteRenderer medalIma;
    //所有数字图片数组
    public Sprite[] numSprite;
    //所有奖牌数组
    public Sprite[] medalSprite;
    //
    private int scoreCount = 0;
    //总分
    private int topScore;

    void Awake()
    {
        _instance = this;
        scoreNum1Ima = GameObject.Find("scoreNumber_1").GetComponent<SpriteRenderer>();
        scoreNum2Ima = GameObject.Find("scoreNumber_2").GetComponent<SpriteRenderer>();
        topScoreNum1Ima = GameObject.Find("topScoreNumber_1").GetComponent<SpriteRenderer>();
        topScoreNum2Ima = GameObject.Find("topScoreNumber_2").GetComponent<SpriteRenderer>();
        newIma = GameObject.Find("newInBox").GetComponent<SpriteRenderer>();
        medalIma = GameObject.Find("mediaIn").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float speed = 17f;
        if (scoreBoxMove)
        {
            scoreBox.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

    void showUI()
    {
        gameObject.transform.Translate(Vector3.right * 7);
        StartCoroutine("UIControl");
    }

    IEnumerator UIControl()
    {
        yield return new WaitForSeconds(0.8f);
        gameoverIma.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.02f);
        gameoverIma.transform.Translate(Vector3.up * 0.05f);
        yield return new WaitForSeconds(0.02f);
        gameoverIma.transform.Translate(Vector3.up * 0.05f);
        yield return new WaitForSeconds(0.02f);
        gameoverIma.transform.Translate(Vector3.up * 0.05f);
        yield return new WaitForSeconds(0.02f);
        gameoverIma.transform.Translate(Vector3.up * 0.05f);
        yield return new WaitForSeconds(0.02f);
        gameoverIma.transform.Translate(Vector3.down * 0.05f);
        yield return new WaitForSeconds(0.02f);
        gameoverIma.transform.Translate(Vector3.down * 0.05f);
        yield return new WaitForSeconds(0.2f);

        //显示分数框
        topScore = PlayerPrefs.GetInt("topScore", 0);
        topScoreInBoxShow(topScore);

        scoreBoxMove = true;
        yield return new WaitForSeconds(0.4f);
        scoreBoxMove = false;
        yield return new WaitForSeconds(0.2f);

        //统计分数
        InvokeRepeating("countScore", 0.02f, 0.05f);

    }

    void countScore()
    {
        if (scoreCount <= gameManager._instance.score)
        {
            int scoreNum1 = scoreCount % 10;
            int scoreNum2 = (scoreCount - scoreNum1) / 10;

            scoreNum1Ima.sprite = numSprite[scoreNum1];
            if (scoreNum2 != 0)
            {
                scoreNum2Ima.enabled = true;
                scoreNum2Ima.sprite = numSprite[scoreNum2];
            }
            else
            {
                scoreNum2Ima.enabled = false;
            }
            scoreCount++;
        }
        else
        {
            if (gameManager._instance.score > topScore)
            {
                PlayerPrefs.SetInt("topScore", gameManager._instance.score);
                topScore =  PlayerPrefs.GetInt("topScore", 0);
                topScoreInBoxShow(topScore);
                newIma.enabled = true;
            }

            //颁发奖章
            if (gameManager._instance.score >= 10 && gameManager._instance.score <= 30)
            {
                medalIma.sprite = medalSprite[0];
                medalIma.enabled = true;
                GameObject.Find("MedalEffectSpawn").SendMessage("ShowMedalEffect");
            }
            else if (gameManager._instance.score > 30)
            {
                medalIma.sprite = medalSprite[1];
                medalIma.enabled = true;
                GameObject.Find("MedalEffectSpawn").SendMessage("ShowMedalEffect");
            }

            //显示按钮
            startBtn.SetActive(true);
            rankBtn.SetActive(true);
            CancelInvoke("countScore");
        }
    }

    private void topScoreInBoxShow(int _score)
    {
        int topScoreNum1 = _score % 10;
        int topScoreNum2 = (_score - topScoreNum1) / 10;

        topScoreNum1Ima.sprite = numSprite[topScoreNum1];
        if (topScoreNum2 != 0)
        {
            topScoreNum2Ima.enabled = true;
            topScoreNum2Ima.sprite = numSprite[topScoreNum2];
        }
        else
        {
            topScoreNum2Ima.enabled = false;
        }


    }
}

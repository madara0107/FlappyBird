using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

    public enum GameState
    { 
        menu,
        running,
        over
    }

    public static gameManager _instance;
    public GameState gameState = GameState.menu;
    public GameObject pipe;
    public GameObject effect;
    public int score = 0;

    private bool gameover = false;
    public Sprite[] numberSprite;
    public Sprite[] backgroundSprite;

    private SpriteRenderer numberOneRender;
    private SpriteRenderer numberTwoRender;
    private SpriteRenderer backgroundRender;


    void Awake()
    {
        _instance = this;

        numberOneRender = GameObject.Find("number_1").GetComponent<SpriteRenderer>();
        numberTwoRender = GameObject.Find("number_2").GetComponent<SpriteRenderer>();
        backgroundRender = GameObject.Find("Background").GetComponent<SpriteRenderer>();

        randomBackground();
    }

	
	void Update () 
    {
	    //游戏开始
        if (gameState == GameState.menu && Input.GetMouseButtonDown(0))
        {
            gameState = GameState.running;
            GameObject.Find("Bird").GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            //TODO 生成管子和消失开始菜单
            InvokeRepeating("createPipes", 0.2f, 1.5f);
            StartCoroutine("startUIDisappear");
            gameState = GameState.running;
        }

        //显示分数
        ShowScore();

        //游戏结束(TODO)
        GameOver();

        //返回键退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    void createPipes()
    {
        GameObject newPipe = Instantiate(pipe, new Vector3(6, 0, 0), Quaternion.identity) as GameObject;
        newPipe.transform.Translate(Vector3.up * Random.Range(-1.3f, 2.3f));

    }

    IEnumerator createDeathEffect()
    {
        GameObject _effect = Instantiate(effect) as GameObject;
        yield return new WaitForSeconds(0.02f);
        _effect.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 140);
        yield return new WaitForSeconds(0.02f);
        _effect.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 100);
        yield return new WaitForSeconds(0.02f);
        _effect.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 50);
        yield return new WaitForSeconds(0.02f);
        Destroy(_effect);
    }

    IEnumerator startUIDisappear()
    {
        SpriteRenderer infoIma = GameObject.Find("info").GetComponent<SpriteRenderer>();
        SpriteRenderer readyIma = GameObject.Find("ready").GetComponent<SpriteRenderer>();

        infoIma.color = new Color32(255, 255, 255, 200);
        readyIma.color = new Color32(255, 255, 255, 200);
        yield return new WaitForSeconds(0.04f);
        infoIma.color = new Color32(255, 255, 255, 160);
        readyIma.color = new Color32(255, 255, 255, 160);
        yield return new WaitForSeconds(0.04f);
        infoIma.color = new Color32(255, 255, 255, 120);
        readyIma.color = new Color32(255, 255, 255, 120);
        yield return new WaitForSeconds(0.04f);
        infoIma.color = new Color32(255, 255, 255, 80);
        readyIma.color = new Color32(255, 255, 255, 80);
        yield return new WaitForSeconds(0.04f);
        infoIma.color = new Color32(255, 255, 255, 40);
        readyIma.color = new Color32(255, 255, 255, 40);
        yield return new WaitForSeconds(0.04f);
        infoIma.enabled = false;
        readyIma.enabled = false;
    }

    private void GameOver()
    {
        
        if (gameState == GameState.over)
        {
            //停止调用生成管子的方法
            //TODO
            CancelInvoke("createPipes");

            //使用GameObject.Find()找到两个地面，并停止它们的运动
            //TODO
            GameObject.Find("ground1").GetComponent<translateGround>().move = false;
            GameObject.Find("ground2").GetComponent<translateGround>().move = false;

            if (!gameover)
            {
                //TODO
                gameoverUI._instance.SendMessage("showUI");
                StartCoroutine("createDeathEffect");
                GameObject.Find("ShowScore").SetActive(false);
                gameover = !gameover;
            }
        }
    }

    private void ShowScore()
    {
        if (score > 99)
        {
            gameState = GameState.over;
        }
        else
        {
            int numberOne = score % 10;
            int numberTwo = (score - numberOne) / 10;
            numberOneRender.sprite = numberSprite[numberOne];
            if (numberTwo != 0)
            {
                numberTwoRender.enabled = true;
                //numberTwoRender.sprite = numberSprite[1];
                numberTwoRender.sprite = numberSprite[numberTwo];
            }
            else
            {
                numberTwoRender.enabled = false;
            }
        }
    }

    private void randomBackground()
    {
        int r = Random.Range(1, 3);

        if (r == 1)
        {
            backgroundRender.sprite = backgroundSprite[0];
        }
        else
        {
            backgroundRender.sprite = backgroundSprite[1];
        }
    }
}

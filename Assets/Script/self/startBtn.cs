using UnityEngine;
using System.Collections;

public class startBtn : MonoBehaviour {

    //鼠标按下时按钮向下位移
    void OnMouseDown()
    {
        gameObject.transform.Translate(Vector3.down * 0.03f);
    }

    ////鼠标松开时按钮向上位移
    void OnMouseUp()
    {
        gameObject.transform.Translate(Vector3.up * 0.03f);
    }

    void OnMouseUpAsButton()
    {

        GetComponent<AudioSource>().Play();
        Application.LoadLevel(0);
    }
}

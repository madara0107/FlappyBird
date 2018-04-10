using UnityEngine;
using System.Collections;

public class medalEffectSapwn : MonoBehaviour {


    public GameObject starEffect;

    public void ShowMedalEffect()
    {
        InvokeRepeating("_showMedalEffect", 0.2f, 0.4f);
    }

    private void _showMedalEffect()
    {
        float _randomX = Random.Range(-0.43f, 0.43f);
        float _randomY = Random.Range(-0.43f, 0.43f);
        Instantiate(starEffect, gameObject.transform.position + new Vector3(_randomX, _randomY, 0), Quaternion.identity);
    }
}

using UnityEngine;
using System.Collections;

public class MedalEffectSpriteDisappear : MonoBehaviour {

	public Sprite[] medalSprite;

	void Awake(){
		StartCoroutine ("changeSprite");
	}

	IEnumerator changeSprite(){
		yield return new WaitForSeconds(0.1f);
		gameObject.GetComponent<SpriteRenderer>().sprite = medalSprite[1];
		yield return new WaitForSeconds(0.1f);
		gameObject.GetComponent<SpriteRenderer>().sprite = medalSprite[2];
		yield return new WaitForSeconds(0.1f);
		Destroy (gameObject);
	}

}

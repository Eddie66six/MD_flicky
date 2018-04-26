using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PItemAcoes : MonoBehaviour {
	private GameObject Next;
	private GameObject Player;
	private Rigidbody2D rb2d;
	private BoxCollider2D bc2d;

	private float size;
	// Use this for initialization
	void Start () {
		Next = null;
		rb2d = GetComponent<Rigidbody2D> ();
		bc2d = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Follow ();
	}

	public void SetFollow(GameObject objPlayer, GameObject obj){
		Player = objPlayer;
		Next = obj;
		rb2d.isKinematic = true;
		bc2d.isTrigger = true;
		size = (Next == Player) ? 0.3f : 0;
	}
	void Follow(){
		if (Next != null) {
			transform.position = Vector2.Lerp(transform.position, new Vector2(Next.transform.position.x, Next.transform.position.y -size), Time.deltaTime * 2f);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	//defaults
	private float _SpeedDefault;
	private float _ForceJumpDefault;

	protected List<GameObject> PItens;
	protected Rigidbody2D rb2d;
	[SerializeField]
	private float _Speed, _ForceJump;
	[SerializeField]
	private int _NumberJump;

	void Awake(){
		_SpeedDefault = 6;
		_ForceJumpDefault = 1750;
	}
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.freezeRotation = true;
		_NumberJump = 2;
		_Speed = _SpeedDefault;
		_ForceJump = _ForceJumpDefault;
		PItens = new List<GameObject> ();
	}

	void FixedUpdate () {
		
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space) && _NumberJump > 0) {
			Jump ();
			_NumberJump--;
		}
		if (Input.GetKey(KeyCode.A))
			MoveLeft ();
		if (Input.GetKey(KeyCode.D))
			MoveRigth ();
	}

	void MoveRigth(){
		transform.position += ((Vector3.right * _Speed) * Time.deltaTime);
	}

	void MoveLeft(){
		transform.position += ((Vector3.left * _Speed) * Time.deltaTime);
	}

	void Jump(){
		rb2d.AddForce (new Vector2(0,10 * _ForceJump) * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Plane" && coll.contacts [0].normal.y == 1) {
			_NumberJump = 2;
			_Speed = _SpeedDefault;
			_ForceJump = _ForceJumpDefault;
		} else if (coll.gameObject.tag == "PItem") {
			if (PItens.Count == 0) {
				coll.gameObject.GetComponent<PItemAcoes> ().SetFollow(gameObject, gameObject);
			} else {
				coll.gameObject.GetComponent<PItemAcoes> ().SetFollow (gameObject, PItens[PItens.Count-1]);
			}
			PItens.Add (coll.gameObject);
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Plane") {
			_Speed /= 1.1f;
		}
	}
}
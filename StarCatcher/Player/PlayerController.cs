using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D rb;
	public float speed;
	private int numberOfPickups;
	private LevelManager levelmanager;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		levelmanager = GameObject.FindObjectOfType<LevelManager> ();
		numberOfPickups = GameObject.FindGameObjectsWithTag ("pickup").Length;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		movePlayer();
		LoadNextLevel ();

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("pickup") ) {
			other.gameObject.SetActive (false);
			numberOfPickups--;
		}
	}

	void LoadNextLevel() {
		if (numberOfPickups == 0) {
			levelmanager.LoadWin ();
		}
	}
	void movePlayer(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb.AddForce(movement * speed);
	}
}

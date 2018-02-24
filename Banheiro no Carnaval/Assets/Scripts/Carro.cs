using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro : MonoBehaviour {

	// Use this for initialization
	public float Speed = 5;
	public float DistanceToActive = 10;
	private Transform player;
	private Rigidbody rgb;
	private bool acertouPlayer;
	private bool isActive;
	void Start () {
		player = GameObject.Find("Player").transform;	
		rgb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isActive)
		{
			float distance = Mathf.Abs(player.position.x - transform.position.x);
			isActive = distance <= DistanceToActive; 	
		}

		if (isActive)
		{
			float currentSpeed = Speed;
			if (Physics.Raycast(transform.position, Vector3.left, 100f,1<<player.gameObject.layer) && acertouPlayer)
			{
				currentSpeed = 0;
			}
			rgb.velocity = Vector3.left * currentSpeed;
		}

			
	}
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.name == "Player")
		{
				acertouPlayer = true;
		}
	}
}

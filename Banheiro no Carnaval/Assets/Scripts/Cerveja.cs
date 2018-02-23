using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerveja : MonoBehaviour {

	public int QtdCerveja = 1;
	private GameManagement gameManagement;
	// Use this for initialization
	void Start () {
		gameManagement = (GameManagement)FindObjectOfType(typeof(GameManagement));		
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.name == "Player")
		{
			gameManagement.AddCerveja(QtdCerveja);
			GameObject.Destroy(this.gameObject);
		}
	}
}

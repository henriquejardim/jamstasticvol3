using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerveja : MonoBehaviour {

	public int QtdCerveja = 1;
	private GameManagement gameManagement;
	private AudioSource audioCerveja;
	private bool FoiBebida;
	// Use this for initialization
	void Start () {
		gameManagement = (GameManagement)FindObjectOfType(typeof(GameManagement));	
		audioCerveja = GetComponent<AudioSource>();	
	}

	private void Update() {
		if (FoiBebida && !audioCerveja.isPlaying)
			GameObject.Destroy(this.gameObject);
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.name == "Player")
		{
			gameManagement.AddCerveja(QtdCerveja);
			if (gameManagement.EstaBebado())
				audioCerveja.pitch = audioCerveja.pitch/2;

			audioCerveja.Play();
			GetComponent<MeshRenderer>().enabled = false;
			FoiBebida = true;			
		}
	}
}

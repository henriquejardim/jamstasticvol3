using UnityEngine;

public class Cavalete : MonoBehaviour {

	public float TempoDeEfeito = 3f;

	// redução de velocidade em procentagem
	[Range(0.1f,1)]
	public float ReducaVelocidade = 0.3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.name == "Player")
		{
			GetComponent<BoxCollider>().enabled = false;
			print("Cavalete");
		}
	}
}

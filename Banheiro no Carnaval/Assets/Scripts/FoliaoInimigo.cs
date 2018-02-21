using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoliaoType {
	// Foliao permanece onde foi Instanciado
	Parado,
	SentidoOposto,
	EntrePontos,
	Seguidor,
	AtravessaRua
}
public class FoliaoInimigo : MonoBehaviour {

	public FoliaoType Tipo;

	//VALORES PARA DEFINIR QUAND COMEÇAR A MOVIMENTAÇÃO SE APLICAVEL
	// VALOR ALEATÓRIO PAR GERAR CERTA IMPREVISIBILIDADE
	public float DistanceToActive = 10f;
	public float ModifierRandom = 0f;
	public float Speed = 3f;
	private Player player;
	[SerializeField]
	private bool IsActive;
	//pode mudar seu tipo quando atingir certa distancia
	public bool CanSwitch;
	public float DistanceToSwitch = 4;
	public FoliaoType SwitchToType;
	//Excluisivo para movimentação entre pontos
	[SerializeField]
	private int followPoint = 4;
	private float startTime;
	void Start () {
		player = (Player) FindObjectOfType (typeof (Player));
		if (Tipo == FoliaoType.EntrePontos) {
			IsActive = true;
			startTime = Time.time;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!IsActive) {
			float ditance = Mathf.Abs (player.transform.position.x - transform.position.x + Random.Range (-ModifierRandom, ModifierRandom));
			IsActive = ditance <= DistanceToActive;
		}
		Vector3 pos = transform.position;
		if (IsActive) {
			switch (Tipo) {
				case FoliaoType.EntrePontos:
					float posTarget = GameObject.FindGameObjectWithTag ("Pista").GetComponentsInChildren<Transform> () [followPoint].position.z;
					float distanceTime = (Time.time - startTime) * Speed;
					pos.z = Mathf.Lerp (transform.position.z, posTarget, distanceTime);
					if (pos.z == posTarget) {
						startTime = Time.time;
						followPoint = followPoint == 4 ? 1 : 4;
						GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
					}
					break;
				case FoliaoType.AtravessaRua:
					pos.z = Mathf.Lerp (transform.position.z, transform.position.z - 1, Speed * Time.deltaTime);
					break;
				case FoliaoType.Seguidor:
					pos = Vector3.MoveTowards (transform.position, player.transform.position, Speed * Time.deltaTime);
					break;
				case FoliaoType.SentidoOposto:
					pos.x = Mathf.Lerp (transform.position.x, transform.position.x - 1, Speed * Time.deltaTime);
					break;
			}
		}
		pos.y = transform.position.y; // mantem sempre na mesma altura 
		transform.position = pos;

		if (CanSwitch && Mathf.Abs(transform.position.x - player.transform.position.x) <= DistanceToSwitch)
		{
			Tipo = SwitchToType;
		}
	}

	private void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "Player") {
			print ("Dano");
		}
	}
}
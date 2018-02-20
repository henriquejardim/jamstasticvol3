using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Vector2 Speed = new Vector2 (10f, 5f);
	public int faixaAtiva = 1;
	public float JumpForce = 10f;
	private List<GameObject> Faixas = new List<GameObject> ();
	private Rigidbody rgb;
	private float startTime;
	private Transform groundCheck;
	void Start () {
		rgb =  GetComponent<Rigidbody>();
		groundCheck = GameObject.Find("GroundCheck").transform;
		// Guarda os Objetos que identificas as faixas
		GameObject pista = GameObject.FindGameObjectWithTag ("Pista");
		for (int i = 0; i < pista.transform.childCount; i++) {
			Faixas.Add (pista.transform.GetChild (i).gameObject);
		}
		// Posiciona na Faixa Inicial
		transform.position = new Vector3(Faixas[faixaAtiva].transform.position.x,transform.position.y,Faixas[faixaAtiva].transform.position.z);
	}

	void Update () {
		if (PlayerInActiveSection ()) {
			if (Input.GetAxisRaw ("Vertical") >= 0.5f && faixaAtiva > 0) {
				faixaAtiva--;
			} else if (Input.GetAxisRaw ("Vertical") <= -0.5f && faixaAtiva < 3) {
				faixaAtiva++;
			}
			startTime = Time.time;
		}
		Moviment ();

		if (Input.GetAxisRaw("Jump") > 0 && OnGround())
		{
			rgb.AddForce(Vector3.up * JumpForce);
		} 
	}

	/// <summary>
	/// Movimenta o Player para frente e para Faixa ativa
	/// </summary>
	private void Moviment () {
		float distanceTime = (Time.time - startTime)*Speed.y;
		Vector3 movment = transform.position;
		if (!PlayerInActiveSection ()) {
			movment.z = Mathf.Lerp (transform.position.z, Faixas[faixaAtiva].transform.position.z, distanceTime);
		}
		movment.x = Mathf.Lerp(transform.position.x, transform.position.x + 1,Speed.x * Time.deltaTime);

		transform.position = movment;
	}

	/// <summary>
	/// Detecta se o Player se encontra na Faixa Ativa
	/// </summary>
	public bool PlayerInActiveSection () {
		return Mathf.Abs (transform.position.z - Faixas[faixaAtiva].transform.position.z) < 0.02f;
		//return transform.position.z == Faixas[faixaAtiva].transform.position.z;
	}

	/// <summary>
	/// Detecta se o Player está no chão
	/// </summary>
	/// <returns></returns>
	public bool OnGround()
	{
		return Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Pista"));
	} 
}
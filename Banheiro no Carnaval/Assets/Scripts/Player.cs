using UnityEngine;

public class Player : MonoBehaviour {

	public Vector2 Speed = new Vector2 (10f, 5f);
	public float JumpForce = 10f;
	public float ModVelocidadeCerveja = 1f;
	private Pista Pista;
	private Rigidbody rgb;
	private float startTime;
	[SerializeField]
	private Vector2 currentSpeed;
	private Transform groundCheck;
	private GameManagement gameManagement;

	public float TempoSemControle = 5f;
	[SerializeField]
	private float temporizadorSemControle;
	[SerializeField]
	private bool carroNoCaminho;

	void Start () {
		rgb = GetComponent<Rigidbody> ();
		gameManagement = (GameManagement) FindObjectOfType (typeof (GameManagement));
		groundCheck = GameObject.Find ("GroundCheck").transform;

		// Guarda os Objetos que identificas as faixas
		GameObject pista = GameObject.FindGameObjectWithTag ("Pista");
		Pista = (Pista) FindObjectOfType (typeof (Pista));
		// Posiciona na Faixa Inicial
		transform.position = new Vector3 (transform.position.x, transform.position.y, Pista.GetPosZFaixaAtual ());
		currentSpeed = Speed;
		temporizadorSemControle = 0;
	}

	void Update () {
		if (temporizadorSemControle <= 0) {
			if (PlayerInActiveSection () && OnGround ()) {
				if (Input.GetAxisRaw ("Vertical") >= 0.5f && Pista.FaixaAtual > 1) {
					Pista.FaixaAtual--;
				} else if (Input.GetAxisRaw ("Vertical") <= -0.5f && Pista.FaixaAtual < 4) {
					Pista.FaixaAtual++;
				}
				startTime = Time.time;
			}
			Moviment ();
		} else {
			temporizadorSemControle -= Time.deltaTime;
		}

	}

	void FixedUpdate () {
		if (Input.GetAxisRaw ("Jump") > 0 && OnGround () && temporizadorSemControle <= 0) {
			rgb.AddForce (Vector3.up * JumpForce);
		}
	}

	/// <summary>
	/// Movimenta o Player para frente e para Faixa ativa
	/// </summary>
	private void Moviment () {
		if (gameManagement.CervejaAcumulada > 0) {
			if (gameManagement.EstaBebado ()) {
				currentSpeed.x = Speed.x - ModVelocidadeCerveja * 2;
				currentSpeed.y = Speed.y - ModVelocidadeCerveja * 2;
			} else {
				currentSpeed.x = Speed.x + ModVelocidadeCerveja * gameManagement.CervejaAcumulada;
				currentSpeed.y = Speed.y + ModVelocidadeCerveja * gameManagement.CervejaAcumulada;
			}
		} else {
			// currentSpeed.x = Mathf.Lerp(currentSpeed.x, Speed.x, 2 * Time.deltaTime);
			// currentSpeed.y = Mathf.Lerp(currentSpeed.y, Speed.y, 2 * Time.deltaTime);
			currentSpeed.x = Speed.x;
			currentSpeed.y = Speed.y;
		}
		if (carroNoCaminho)
		{
			currentSpeed.x = 0;
		}
		//MOVIMENTO ENTRE FAIXAS - EIXO Z
		float distanceTime = (Time.time - startTime) * currentSpeed.y;
		Vector3 movment = transform.position;
		if (!PlayerInActiveSection ()) {
			movment.z = Mathf.Lerp (transform.position.z, Pista.GetPosZFaixaAtual (), distanceTime);
		}

		//MOVIMENTO DE CORRIDA - EIXO X

		movment.x = Mathf.Lerp (transform.position.x, transform.position.x + 1, currentSpeed.x * Time.deltaTime);

		transform.position = movment;
	}

	/// <summary>
	/// Detecta se o Player se encontra na Faixa Ativa
	/// </summary>
	public bool PlayerInActiveSection () {
		return Mathf.Abs (transform.position.z - Pista.GetPosZFaixaAtual ()) < 0.02f;
		//return transform.position.z == Faixas[Pista.FaixaAtual].transform.position.z;
	}

	/// <summary>
	/// Detecta se o Player está no chão
	/// </summary>
	/// <returns></returns>
	public bool OnGround () {
		return Physics.Linecast (transform.position, groundCheck.position, 1 << Pista.gameObject.layer);
	}

	private void OnCollisionEnter (Collision other) {
		if (other.gameObject.CompareTag ("Carro")) {
			print(other.gameObject.GetComponent<Rigidbody>().velocity.x);
			if(other.gameObject.GetComponent<Rigidbody>().velocity.x < - 0.01f)
			{
				temporizadorSemControle = TempoSemControle;
				print("Colisão");
			}
				

			carroNoCaminho = true;
		}
	}

	private void OnCollisionExit (Collision other) {
		if (other.gameObject.CompareTag ("Carro")) {
			carroNoCaminho = false;
		}
	}

	private void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Inimigo")) {
			if (!gameManagement.EstaBebado ()) {
				gameManagement.CervejaAcumulada = 0;
				gameManagement.TemporizadorCerveja = 0;
			}
		}
		
		if (other.gameObject.CompareTag("Bueiro"))
		{
			print("Bueiro");
			rgb.velocity = Vector3.zero;
			temporizadorSemControle = TempoSemControle;
			GetComponent<CapsuleCollider>().enabled = false;
		}

		if (other.gameObject.CompareTag("Final"))
			gameManagement.TerminouFase = true;
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {


	public float Cronometro = 120f; //fase de no maximo 2 minutos a principio
	[Range(0,100)]
	public float Pipibar = 50f; //quando alcança o valor máximo perde
	public float ModificadorPipi = 5; //quanto o valor da barra de xixi diminui se estiver na metade da barra
	public float PipiPorCerveja = 5; // quanto uma cerveja aumenta a Pipibar 
	public int CervejaAcumulada = 0; //quantidade de cervejas seguidas
	public int MaxCerveja = 3; //Máximo de Cerveja antes de ficar bebado e tomar slow
	//[HideInInspector]
	public float TempEfeitoCerveja = 2f; //Tempo de duração do efeito por cerveja  antes de começar a decair - embreagado usa o dobro do valor
	//[HideInInspector]
	public float ContTempCerveja; //Tempo restante do efeito da cerveja
	public bool IsDead; //Indica se o jogador perdeu

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Cronometro <= 0 || Pipibar >= 100)
			IsDead = true;

		if (!IsDead)
		{
			//decrementa conômetro
			Cronometro -= Time.deltaTime;

			//decrementa pipibar
			//quanto mais cheia mais rápido diminui
			Pipibar -= (ModificadorPipi/50 * Pipibar * Time.deltaTime);

			//decrementa efeito cerveja
			if(ContTempCerveja > 0)
			{
				ContTempCerveja = (ContTempCerveja - Time.deltaTime) < 0?0: ContTempCerveja - Time.deltaTime;
			}

			if (ContTempCerveja == 0)
			{
				CervejaAcumulada = 0;
			}
		}	
	}

	public void AddCerveja(int cerveja)
	{
		CervejaAcumulada += cerveja;

		if (CervejaAcumulada <= MaxCerveja)
			ContTempCerveja = TempEfeitoCerveja;
		else
			ContTempCerveja = TempEfeitoCerveja * 2;

		//quanto mais cervejas acumuladas maior é o dano à PipiBar
		Pipibar += (PipiPorCerveja * CervejaAcumulada);
	}

	public bool EstaBebado()
	{
		return CervejaAcumulada > MaxCerveja;
	}


	public void DiminuiCronometro(float tempo)
	{
		Cronometro -= tempo * Time.deltaTime;
	}
}

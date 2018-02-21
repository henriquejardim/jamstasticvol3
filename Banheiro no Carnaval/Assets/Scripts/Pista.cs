using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista : MonoBehaviour {

	public int FaixaAtual = 1;
	//posição em z da faixa atual
	[HideInInspector]
	public float LinhaFaixa;

	public float GetPosZFaixaAtual()
	{
		return GetComponentsInChildren<Transform>()[FaixaAtual].position.z;
	}
}

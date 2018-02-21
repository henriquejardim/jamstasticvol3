using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista : MonoBehaviour {

	public int FaixaAtual = 1;
	//posição em z da faixa atual
	[HideInInspector]
	public float GetPosZFaixaAtual()
	{
		return GetComponentsInChildren<Transform>()[FaixaAtual].position.z;
	}

	private void OnDrawGizmos() {
		Gizmos.color = new Color32(10,221,37,150);
		//Gizmos.color = Color.green;
		Mesh mesh = GetComponent<MeshCollider>().sharedMesh;
		Gizmos.DrawMesh(mesh,transform.position,Quaternion.identity,transform.localScale);
	}
}

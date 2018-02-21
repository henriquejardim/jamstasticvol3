using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	public float MinPos = 0;
	public float Offset = 0;
	private Transform target;
	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x = target.position.x + Offset;

		pos.x = pos.x < MinPos ? MinPos : pos.x;
		transform.position = pos;
	}
}

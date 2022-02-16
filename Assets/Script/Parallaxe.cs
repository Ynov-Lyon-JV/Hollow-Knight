using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxe : MonoBehaviour
{
	public float length, startPos;
	public GameObject cam;
	public float parallexEffect;

	void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		length = GetComponent<SpriteRenderer>().bounds.size.x;
		startPos = transform.position.x+length/2;
	}

	void FixedUpdate()
	{
		float temp = (cam.transform.position.x * (1 - parallexEffect));
		float dist = (cam.transform.position.x * parallexEffect);

		transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

		if (temp > startPos + length) startPos += length;
		else if (temp < startPos - length) startPos -= length;
	}
}

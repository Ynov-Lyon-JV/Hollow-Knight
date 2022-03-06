using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxe : MonoBehaviour
{
	public float length, startPos;
	public float lengthY, startPosY;
	public GameObject cam;
	public float parallexEffect;

	public float temp;
	public float temp2;

	void Awake()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		length = GetComponent<SpriteRenderer>().bounds.size.x;
		startPos = transform.position.x;

		lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
		startPosY = transform.position.y + lengthY / 2;
	}

	void Update()
	{
		temp = (cam.transform.position.x * (1 - parallexEffect));
		float dist = (cam.transform.position.x * parallexEffect);
		float distY = (cam.transform.position.y * parallexEffect/2);

		transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
		temp2 = startPos + length/2;
		if (temp > temp2) startPos += length;
		else if (temp < startPos - length/2) startPos -= length;
	}
}

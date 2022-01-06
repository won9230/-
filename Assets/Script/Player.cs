using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPunCallbacks
{
	private float h, v;
	private Rigidbody rigidbody;

	public float speed = 5f;

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		Debug.Log("asd");
	}

	private void Update()
	{
		if (!photonView.IsMine)
		{
			return;
		}

		v = Input.GetAxisRaw("Vertical");
		h = Input.GetAxisRaw("Horizontal");

		transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
		transform.Translate(Vector3.right * h * speed * Time.deltaTime);
	}
}

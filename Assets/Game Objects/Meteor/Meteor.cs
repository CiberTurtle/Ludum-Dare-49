using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float hitForce;
	Rigidbody2D rb;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		rb.velocity = transform.up * speed;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * hitForce);
		Destroy(gameObject);
	}
}

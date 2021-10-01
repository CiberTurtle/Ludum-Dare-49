using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager current { get; private set; }

	[SerializeField] LayerMask roomLayer;

	Transform objectInHand;

	Controls controls;
	bool pickup;
	bool drop;
	Vector2 pointerPosition;
	Vector2 oldPointerPosition;

	void Awake()
	{
		current = this;

		controls = new Controls();
		controls.Enable();

		controls.Game.Pointer.performed += ctx => pointerPosition = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
		controls.Game.Pickup.started += ctx => pickup = true;
		controls.Game.Pickup.canceled += ctx => drop = true;
	}

	void FixedUpdate()
	{
		if (objectInHand != null)
		{
			objectInHand.GetComponent<Rigidbody2D>().velocity = (pointerPosition - (Vector2)objectInHand.position) * 10;
			// objectInHand.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

			if (drop)
			{
				if (objectInHand != null)
				{
					objectInHand = null;
				}
			}
		}
		else
		{
			if (pickup)
			{
				if (objectInHand == null)
				{
					var objectAtCursor = Physics2D.OverlapPoint(pointerPosition, roomLayer)?.transform;

					if (objectAtCursor != null)
					{
						objectInHand = objectAtCursor;
					}
				}
			}
		}

		oldPointerPosition = pointerPosition;

		pickup = false;
		drop = false;
	}
}

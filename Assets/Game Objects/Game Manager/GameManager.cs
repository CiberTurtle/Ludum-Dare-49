using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager current { get; private set; }

	[SerializeField] float moveVelocityMultiplier = 10;
	[Space]
	[SerializeField] Transform highestHeightMarker;
	[Space]
	[SerializeField] TMP_Text scoreText;
	[Space]
	[SerializeField] LayerMask roomLayer;

	public int score { get => _score; set { _score = value; scoreText.text = $"{value} Points"; } }
	int _score;

	float highestHeight;

	Rigidbody2D heldRoom;

	Transform roomsContainer;

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

	void OnEnable()
	{
		roomsContainer = GameObject.FindGameObjectWithTag("Rooms Container").transform;
	}

	void Start()
	{
		score = 0;
	}

	void FixedUpdate()
	{
		if (heldRoom != null)
		{
			heldRoom.velocity = (pointerPosition - (Vector2)heldRoom.position) * moveVelocityMultiplier;

			if (drop)
			{
				heldRoom = null;
			}
		}
		else
		{
			if (pickup)
			{
				if (heldRoom == null)
				{
					var objectAtCursor = Physics2D.OverlapPoint(pointerPosition, roomLayer)?.GetComponent<Rigidbody2D>();

					if (objectAtCursor != null)
					{
						heldRoom = objectAtCursor;
					}
				}
			}
		}

		oldPointerPosition = pointerPosition;

		pickup = false;
		drop = false;

		var highestRoomHeight = 0f;
		Transform highestRoom = null;
		foreach (Transform room in roomsContainer)
		{
			if (room == heldRoom) continue;
			// if (room.GetComponent<Rigidbody2D>().velocity.magnitude > 3f * Time.fixedDeltaTime) continue;
			if (!room.GetComponent<Rigidbody2D>().IsTouchingLayers()) continue;

			var roomHeight = room.position.y;
			if (roomHeight > highestRoomHeight)
			{
				highestRoomHeight = roomHeight;
				highestRoom = room;
			}
		}

		if (highestRoomHeight > highestHeight)
		{
			highestHeight = highestRoomHeight;
			highestHeightMarker.position = new Vector3(0, highestHeight, 0);
		}
	}
}

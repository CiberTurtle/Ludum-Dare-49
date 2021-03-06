using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager current { get; private set; }

	[SerializeField] float moveVelocityMultiplier = 10;
	[SerializeField] float rotateVelocityMultiplier = 1;
	[Space]
	[SerializeField] float maxCameraSize;
	[SerializeField] float minCameraSize;
	[SerializeField] float minCameraYPosition;
	[SerializeField] float cameraSizeSafezone;
	[SerializeField] float cameraPositionUpperSafezone;
	[SerializeField] float cameraPositionSmoothing;
	[SerializeField] float cameraSizeSmoothing;
	[Space]
	[SerializeField] Transform highestHeightMarker;
	[Space]
	[SerializeField] TMP_Text scoreText;
	[Space]
	[SerializeField] LayerMask roomLayer;

	public int money { get => _money; set { _money = value; scoreText.text = _money.ToString("C"); } }
	int _money;

	float highestHeight;
	Bounds cameraBounds;

	Rigidbody2D heldRoom;

	Camera cam;
	Transform objects;
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

		controls.Game.Pointer.performed += ctx => pointerPosition = cam.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
		controls.Game.Pickup.started += ctx => pickup = true;
		controls.Game.Pickup.canceled += ctx => drop = true;
	}

	void OnEnable()
	{
		objects = GameObject.Find("Objects").transform;
		roomsContainer = objects.Find("Rooms").transform;

		cam = Camera.main;
	}

	void Start()
	{
		money = 0;
	}

	void LateUpdate()
	{
		cam.transform.position = Vector2.Lerp(cam.transform.position, new Vector3(cameraBounds.center.x, Math.Max(cameraBounds.center.y, minCameraYPosition + cam.orthographicSize), 0) + new Vector3(0, cameraPositionUpperSafezone, 0), Time.deltaTime * cameraPositionSmoothing);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Clamp(Mathf.Max(cameraBounds.size.x * (1f / cam.aspect), cameraBounds.size.y) / 2 + cameraSizeSafezone, minCameraSize, maxCameraSize), Time.deltaTime * cameraSizeSmoothing);
	}

	void FixedUpdate()
	{
		if (heldRoom != null)
		{
			heldRoom.velocity = (pointerPosition - (Vector2)heldRoom.position) * moveVelocityMultiplier;
			heldRoom.angularVelocity = (pointerPosition.x - oldPointerPosition.x) * rotateVelocityMultiplier;

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

		if (heldRoom == null)
		{
			var highestRoomHeight = 0f;
			Transform highestRoom = null;
			foreach (Transform room in roomsContainer)
			{
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

		cameraBounds = new Bounds();
		CaptureBounds(objects);

		pickup = false;
		drop = false;
	}

	void CaptureBounds(Transform root)
	{
		foreach (Transform obj in root)
		{
			cameraBounds.Encapsulate(obj.position);
			CaptureBounds(obj);
		}
	}
}

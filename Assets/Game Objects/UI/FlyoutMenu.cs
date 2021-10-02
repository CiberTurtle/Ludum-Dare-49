using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyoutMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] float tabWidth;
	[SerializeField] bool isOnRight;

	float originalPosition;

	RectTransform rect;

	void Awake()
	{
		originalPosition = transform.position.x;

		rect = GetComponent<RectTransform>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (isOnRight)
		{
			rect.DOMoveX(rect.parent.GetComponent<RectTransform>().sizeDelta.x, 0.25f);
		}
		else
		{
			rect.DOMoveX(0, 0.25f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (isOnRight)
		{
			rect.DOMoveX(rect.parent.GetComponent<RectTransform>().sizeDelta.x + rect.sizeDelta.x - tabWidth, 0.25f);
		}
		else
		{
			rect.DOMoveX(-rect.sizeDelta.x + tabWidth, 0.25f);
		}
	}
}

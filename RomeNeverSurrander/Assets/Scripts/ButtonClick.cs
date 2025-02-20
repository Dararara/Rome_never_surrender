﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ButtonClick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject hero;
    public float pressDurationTime = 1;
    public bool responseOnceByPress = false;
    public float doubleClickIntervalTime = 0.5f;

    public UnityEvent onDoubleClick;
    public UnityEvent onPress;
    public UnityEvent onClick;

    private bool isDown = false;
    private bool isPress = false;
    private float downTime = 0;

    private float clickIntervalTime = 0;
    private int clickTimes = 0;

    void Update()
    {
        if (isDown)
        {
            if (responseOnceByPress && isPress)
            {
                return;
            }
            downTime += Time.deltaTime;
            if (downTime > pressDurationTime)
            {
                isPress = true;
                onPress.Invoke();
            }
        }
        if (clickTimes >= 1)
        {
            clickIntervalTime += Time.deltaTime;
            if (clickIntervalTime >= doubleClickIntervalTime)
            {
                if (clickTimes >= 2)
                {
                    onDoubleClick.Invoke();
                }
                else
                {
                    onClick.Invoke();
                }
                clickTimes = 0;
                clickIntervalTime = 0;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        downTime = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isDown = false;
        isPress = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isPress)
        {
            //onClick.Invoke();
            clickTimes += 1;
        }
        else
            isPress = false;
    }
    public void OnDoubleClick()
    {
        Debug.Log("double click");
    }
    public void OnClick()
    {
        Debug.Log("click");
    }
    public void OnPress()
    {
        hero.transform.position += hero.transform.forward * Time.deltaTime * 2.0f;
    }
}
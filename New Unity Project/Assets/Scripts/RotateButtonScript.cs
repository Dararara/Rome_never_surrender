﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        down = false;
    }
    public GameObject camera;
    public GameObject hero;
    // Update is called once per frame
    void Update()
    {
        if (down)
        {
            DataRecorder.view_angle += Time.deltaTime * speed;
            camera.transform.LookAt(hero.transform);
        }
    }
    public bool down;
    public float speed = 1.0f;
    public void OnClickDown()
    {
        down = true;
    }
    public void OnClickUp()
    {
        down = false;
    }
}
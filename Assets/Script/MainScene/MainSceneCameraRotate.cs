using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneCameraRotate : MonoBehaviour
{

    [SerializeField] float rotateSpeed = 1.5f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f);
    }
}

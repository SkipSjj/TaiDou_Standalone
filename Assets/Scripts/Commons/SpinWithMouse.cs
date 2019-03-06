using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinWithMouse : MonoBehaviour
{ 
    public Transform targrt; 
    //实例化物体的位置 
    private bool isRotating = false;
    //是否移动，标志位 
    public float rotateSpeed = 100f;
    //移动速度 
    void Start()
    {
    }

    void Update()
    {
        
    }

    void OnMouseDrag()
    {
        SpinWithMouseFunc();
    } 

    private void SpinWithMouseFunc() 
    {
        if (Input.GetMouseButton(0)) //鼠标左键一直按下isRotating设为true 
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(0)) //鼠标左键抬起isRotating设为false 
        {
            isRotating = false;
        }
        if (isRotating)
        {
            this.gameObject.transform.Rotate(Vector3.down, Time.deltaTime * rotateSpeed * Input.GetAxis("Mouse X"), Space.Self);
        } 
    }
}

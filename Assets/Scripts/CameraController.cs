using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject PlayerController;        


    private Vector3 offset;            

    void Start()
    {
        offset = transform.position - PlayerController.transform.position;
    }

    
    void LateUpdate()
    {
        transform.position = PlayerController.transform.position + offset;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sensitivity : MonoBehaviour
{
    // Start is called before the first frame update
     public CinemachineFreeLook cinemachineVirtualCamera;
     private void Start()
     {
         cinemachineVirtualCamera.m_XAxis.m_MaxSpeed = 250f;
         cinemachineVirtualCamera.m_YAxis.m_MaxSpeed = 10f;
     }
 
 
}
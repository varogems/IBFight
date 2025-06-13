using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] GameObject m_FollowCamera;
    [SerializeField] GameObject m_WinCamera;

    void Start()
    {
        m_FollowCamera.SetActive(true);
        m_WinCamera.SetActive(false);
    }

    public void VictoryTime()
    {
        m_FollowCamera.SetActive(false);
        // Debug.Log("Turn off" + m_FollowCamera.name);
        m_WinCamera.SetActive(true);
    }

}

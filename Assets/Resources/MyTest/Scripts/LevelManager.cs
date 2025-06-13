using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum LevelName
    {
        Menu = 0,
        Ingame,
        Engame
    }


    public void LoadSceneMenu()
    {
        GenerateLevel.m_instance.LoadSceneMenu();
    }

    public void LoadSceneEngame()
    {
    }

    public void LoadSceneIngame1vs1()
    {
        GenerateLevel.m_instance.CreateMode1Vs1();
    }
    public void LoadSceneIngame1vsN()
    {
        GenerateLevel.m_instance.CreateMode1VsN();
    }
    public void LoadSceneIngameNvsN()
    {
        GenerateLevel.m_instance.CreateModeNVsN();
    }





}

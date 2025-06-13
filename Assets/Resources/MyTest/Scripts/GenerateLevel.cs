using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GenerateLevel : MonoBehaviour
{
    public enum EPrefab
    {
        Player = 0,
        Homeless,
        Boss
    }


    public enum EModeVS
    {
        Mode1Vs1,
        Mode1VsN,
        ModeNVsN
    }


    public static GenerateLevel m_instance { get; private set; }

    List<UnityEngine.Object> m_listPrefab;
    Player m_player;
    int m_curlevel;

    CameraManager m_cameraManager;


    float m_timeReact;
    float m_intervalBeforAttack;
    List<ProcessAction.InfoAction> m_infoSkillListHomeless, m_infoSkillListBoss;
    bool m_isIngame;

    void Awake()
    {
        if (FindObjectsOfType(this.GetType()).Length > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Init();

            m_isIngame = false;
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }



    void Init()
    {
        m_curlevel = 0;
        LoadPrefabs();


        m_infoSkillListHomeless = new List<ProcessAction.InfoAction>();
        m_infoSkillListBoss = new List<ProcessAction.InfoAction>(); ;
    }



    void LoadPrefabs()
    {
        m_listPrefab = new List<UnityEngine.Object>();
        m_listPrefab.Add(Resources.Load("MyTest/Prefab Variants/Player"));
        m_listPrefab.Add(Resources.Load("MyTest/Prefab Variants/Homeless"));
        m_listPrefab.Add(Resources.Load("MyTest/Prefab Variants/Boss"));
    }

    public string GetNamePrefabByIndex(int index)
    {
        return m_listPrefab[index].name.ToString();
    }
    public UnityEngine.Object GetPrefabByIndex(int index)
    {
        return m_listPrefab[(int)index];
    }



    //!
    public void CreateMode1Vs1()
    {

        SceneManager.LoadScene("Resources/MyTest/Scenes/1vs1");

        StartCoroutine(IEWaitGetCameraManager());
        StartCoroutine(IEWaitCreateMode1Vs1());

    }




    //!
    IEnumerator IEWaitGetCameraManager()
    {
        m_cameraManager = null;


        yield return new WaitForSeconds(1f);
        //! Get current cameraManager.
        yield return new WaitWhile(() =>
        {
            m_cameraManager = FindObjectOfType<CameraManager>();
            return m_cameraManager == null;
        });

        // Debug.Log("Camera ID: " + m_cameraManager.gameObject.GetInstanceID());
        yield break;
    }


    void Update()
    {
        if (!m_isIngame)
            return;

        if (PoolManager.IsDeactiveAllObjectByType())
        {
            m_player.DanceWin();
            m_isIngame = false;
            NextLevel();
        }
        else
        {
            // m_player.SetTarget(PoolManager.getNextActiveObject());
            // m_isIngame = true;
        }
    }

    void WaitAnimKnockout() { Debug.Log("Wait anim knock out"); }

    public void NextLevel()
    {
        StartCoroutine(IEVictoryTime());
    }

    //!
    IEnumerator IEVictoryTime()
    {
        //! Don't know why error ?????
        m_cameraManager.VictoryTime();

        yield return new WaitForSeconds(5f);

        //! avoid pointer to player, camera previous level.
        m_player = null;
        m_cameraManager = null;

        if (m_curlevel == 10)
        {
            LoadSceneMenu();
            yield break;
        }

        CreateMode1Vs1();
    }


    //!
    IEnumerator IEWaitCreateMode1Vs1()
    {

        GenerateDifficutyByLevel();
        yield return StartCoroutine(IEWaitGetPlayerToSetTarget());
        yield break;
    }



    //!
    IEnumerator IEWaitGetPlayerToSetTarget()
    {
        m_player = null;
        yield return new WaitForSeconds(1f);

        //! get current player.   
        yield return new WaitWhile(() =>
        {
            m_player = FindObjectOfType<Player>();
            return m_player == null;
        });


        //! After we have player, we start set target for player and enemy
        Vector3 v = new Vector3(0, 0, 0);
        GameObject enemy = PoolManager.SpawnCharacter(Human.Character.Boss, v);
        enemy.GetComponent<Enemy>().SetDifficutyAI(ref m_infoSkillListBoss,
                                                m_curlevel, m_timeReact,
                                                m_intervalBeforAttack);

                                                
        m_player.SetTarget(enemy);
        enemy.GetComponent<Human>().SetTarget(m_player.gameObject);
        
        m_isIngame = true;

        yield break;
    }

    //!
    void GenerateDifficutyByLevel()
    {
        //! Start Config.
        Config.GetInstance();

        m_curlevel++;
        // Debug.Log("Current level: " + m_curlevel);

        Config.DifficultyLevel difficultyLevel = Config.GetInstance().GetDifficultyLevel(m_curlevel);
        m_timeReact = difficultyLevel.m_timeReact;
        m_intervalBeforAttack = difficultyLevel.m_intervalBeforAttack;

        m_infoSkillListHomeless.Clear();
        List<ProcessAction.InfoAction> originInfoSkillListHomeless = Config.GetInstance().GetListActionByChar(Human.Character.Homeless);

        //! Renew m_infoSkillList with difficuty level.

        //! Homeless
        //!------------------------------------------------------------------------------------------------------------------------------
        for (int index = 0; index < originInfoSkillListHomeless.Count; index++)
        {
            ProcessAction.InfoAction infoAction = originInfoSkillListHomeless[index];

            infoAction.m_damageValueOrigin = (int)(infoAction.m_damageValueOrigin * difficultyLevel.m_ratioAttack);
            infoAction.m_timeCoolDownSkillOrigin *= difficultyLevel.m_ratioTimeCooldown;

            m_infoSkillListHomeless.Add(infoAction);
        }

        //! Boss
        //!------------------------------------------------------------------------------------------------------------------------------
        m_infoSkillListBoss.Clear();
        List<ProcessAction.InfoAction> originInfoSkillListBoss = Config.GetInstance().GetListActionByChar(Human.Character.Boss);

        //! Renew m_infoSkillList with difficuty level.
        for (int index = 0; index < originInfoSkillListBoss.Count; index++)
        {
            ProcessAction.InfoAction infoAction = originInfoSkillListBoss[index];

            infoAction.m_damageValueOrigin = (int)(infoAction.m_damageValueOrigin * difficultyLevel.m_ratioAttack);
            infoAction.m_timeCoolDownSkillOrigin *= difficultyLevel.m_ratioTimeCooldown;

            m_infoSkillListBoss.Add(infoAction);
        }




        //! Log stats enemy after balance
        // foreach (ProcessAction.InfoAction _infoAction in m_infoSkillList)
        //     Debug.Log("m_damageBy: " + _infoAction.m_damageBy +
        //                 " m_action: " + _infoAction.m_action +
        //                 " m_nameClipAnim: " + _infoAction.m_nameClipAnim +
        //                 " m_damageValueOrigin:" + _infoAction.m_damageValueOrigin +
        //                 " m_timeCoolDownSkillOrigin:" + _infoAction.m_timeCoolDownSkillOrigin + "\n");




    }



    public void CreateMode1VsN()
    {
        SceneManager.LoadScene("Resources/MyTest/Scenes/1vsN");
        StartCoroutine(IEWaitCreateMode1VsN());
        StartCoroutine(IEWaitGetPlayerToSetTargetForMode1VsN());


    }


    IEnumerator IEWaitCreateMode1VsN()
    {

        GenerateDifficutyByLevel();
        yield return StartCoroutine(IEWaitGetPlayerToSetTargetForMode1VsN());
        yield break;
    }


    IEnumerator IEWaitGetPlayerToSetTargetForMode1VsN()
    {
        m_player = null;
        yield return new WaitForSeconds(1f);

        //! get current player.   
        yield return new WaitWhile(() =>
        {
            m_player = FindObjectOfType<Player>();
            return m_player == null;
        });


        //! After we have player, we start set target for player and enemy
        Vector3 v = new Vector3(0, 0, 0);
        GameObject enemy1 = PoolManager.SpawnCharacter(Human.Character.Boss, v);
        enemy1.GetComponent<Enemy>().SetDifficutyAI(ref m_infoSkillListBoss,
                                                m_curlevel, m_timeReact,
                                                m_intervalBeforAttack);

        GameObject enemy2 = PoolManager.SpawnCharacter(Human.Character.Boss, v + new Vector3(1, 0, 2));
        enemy2.GetComponent<Enemy>().SetDifficutyAI(ref m_infoSkillListBoss,
                                                m_curlevel, m_timeReact,
                                                m_intervalBeforAttack);


        GameObject enemy3 = PoolManager.SpawnCharacter(Human.Character.Boss, v + new Vector3(2, 0, 1));
        enemy2.GetComponent<Enemy>().SetDifficutyAI(ref m_infoSkillListBoss,
                                                m_curlevel, m_timeReact,
                                                m_intervalBeforAttack);




        m_player.SetTarget(enemy1);

        enemy1.GetComponent<Human>().SetTarget(m_player.gameObject);
        enemy2.GetComponent<Human>().SetTarget(m_player.gameObject);
        enemy3.GetComponent<Human>().SetTarget(m_player.gameObject);
        
        m_isIngame = true;

        yield break;
    }

    public void CreateModeNVsN()
    {

        SceneManager.LoadScene("Resources/MyTest/Scenes/NvsN");
        GameObject enemyA, enemyB;

        Vector3 v;

        GenerateDifficutyByLevel();

        for (int i = 0; i < 25; i++)
        {

            v = new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), 0.5f, UnityEngine.Random.Range(-3f, 2.5f));
            enemyA = PoolManager.SpawnCharacter(Human.Character.Homeless, v);
            enemyA.GetComponent<Enemy>().SetDifficutyAI(ref m_infoSkillListBoss,
                                                    m_curlevel, m_timeReact,
                                                    m_intervalBeforAttack);



            enemyB = PoolManager.SpawnCharacter(Human.Character.Boss, v - new Vector3(0.5f, 0, 0.5f));
            enemyB.GetComponent<Enemy>().SetDifficutyAI(ref m_infoSkillListBoss,
                                                    m_curlevel, m_timeReact,
                                                    m_intervalBeforAttack);

            enemyA.GetComponent<Human>().SetTarget(enemyB);
            enemyB.GetComponent<Human>().SetTarget(enemyA);

        }
        m_isIngame = true;


    }

    //!
    public void LoadSceneMenu()
    {
        m_player = null;
        m_cameraManager = null;

        m_curlevel = 0;
        m_timeReact = 0;
        m_intervalBeforAttack = 0;

        m_infoSkillListHomeless.Clear();
        m_infoSkillListBoss.Clear();

        SceneManager.LoadScene("Resources/MyTest/Scenes/Menu");
    }
    
}

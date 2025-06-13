using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Human
{

    [SerializeField] protected float m_timeReact;
    [SerializeField] protected float m_intervalBeforAttack;
    [SerializeField] protected int m_levelScene;
    [SerializeField] protected bool m_timeToAtack;
    [SerializeField] protected List<ProcessAction.ETypeAction> m_skillOrderList;

    protected override void Awake()
    {
        base.Awake();
        m_typeCharacter = Character.Boss;
        m_skillOrderList = new List<ProcessAction.ETypeAction>();
        // SetLevel(2);

    }

 

    void MakeSkillOrder()
    {
        switch (m_levelScene)
        {
            case 1:
            case 2:
            case 3:
                {
                    m_skillOrderList.Add(ProcessAction.ETypeAction.KidneyAtk);
                    m_skillOrderList.Add(ProcessAction.ETypeAction.StomachAtk);
                    m_skillOrderList.Add(ProcessAction.ETypeAction.HeadAtk);
                }
                break;
            case 4:
            case 5:
            case 6:
                {
                    m_skillOrderList.Add(ProcessAction.ETypeAction.StomachAtk);
                    m_skillOrderList.Add(ProcessAction.ETypeAction.KidneyAtk);
                    m_skillOrderList.Add(ProcessAction.ETypeAction.HeadAtk);
                }
                break;
            case 7:
            case 8:
                {
                    m_skillOrderList.Add(ProcessAction.ETypeAction.KidneyAtk);
                    m_skillOrderList.Add(ProcessAction.ETypeAction.StomachAtk);
                    m_skillOrderList.Add(ProcessAction.ETypeAction.HeadAtk);
                }
                break;
            case 9:
            case 10:
                {
                    m_skillOrderList.Add(ProcessAction.ETypeAction.HeadAtk);
                    m_skillOrderList.Add(ProcessAction.ETypeAction.KidneyAtk);
                    m_skillOrderList.Add(ProcessAction.ETypeAction.StomachAtk);
                }
                break;


        }
        m_timeToAtack = true;
    }


//    public void SetLevel(int level)
//     {
//         m_levelScene = level;
//         LoadListInfoAction();
//         LoadListAnimSkill();
//         MakeSkillOrder();
//     }

    // //! Balance stats enemy
    // protected override void LoadListInfoAction()
    // {
    //     //! Start Config.
    //     Config.GetInstance();

    //     Config.DifficultyLevel difficultyLevel = Config.GetInstance().GetDifficultyLevel(m_levelScene);
    //     m_timeReact = difficultyLevel.m_timeReact;
    //     m_intervalBeforAttack = difficultyLevel.m_intervalBeforAttack;

    //     m_infoSkillList = new List<ProcessAction.InfoAction>();
    //     List<ProcessAction.InfoAction> originInfoSkillList = Config.GetInstance().GetListActionByChar(Character.Boss);

    //     //! Renew m_infoSkillList with difficuty level.
    //     for (int index = 0; index < originInfoSkillList.Count; index++)
    //     {
    //         ProcessAction.InfoAction infoAction = originInfoSkillList[index];

    //         infoAction.m_damageValueOrigin = (int)(infoAction.m_damageValueOrigin * difficultyLevel.m_ratioAttack);
    //         infoAction.m_timeCoolDownSkillOrigin *= difficultyLevel.m_ratioTimeCooldown;

    //         m_infoSkillList.Add(infoAction);
    //     }

    //     //! Log stats enemy after balance
    //     // foreach (ProcessAction.InfoAction _infoAction in m_infoSkillList)
    //     //     Debug.Log("m_damageBy: " + _infoAction.m_damageBy +
    //     //                 " m_action: " + _infoAction.m_action +
    //     //                 " m_nameClipAnim: " + _infoAction.m_nameClipAnim +
    //     //                 " m_damageValueOrigin:" + _infoAction.m_damageValueOrigin +
    //     //                 " m_timeCoolDownSkillOrigin:" + _infoAction.m_timeCoolDownSkillOrigin + "\n");




    // }

    public void SetDifficutyAI(ref List<ProcessAction.InfoAction> infoSkillList,
                                int levelScene, float timeReact, float intervalBeforeAttack)
    {
        m_levelScene = levelScene; ;
        m_infoSkillList = infoSkillList;
        m_timeReact = timeReact;
        m_intervalBeforAttack = intervalBeforeAttack;


        LoadListAnimSkill();
        MakeSkillOrder();
    }



    protected override void Update()
    {
        base.Update();
    }

    protected Human m_targetHuman = null;
    
    protected override void AI()
    {
        if (m_target == null)
            return;

        if (m_targetHuman == null)
            m_targetHuman = m_target.GetComponent<Human>();

        ProcessAction.ETypeAction actionTarget = m_targetHuman.GetCurrAction();

        ProcessAction.ETypeAction actionOwner = this.GetCurrAction();

        //! Dogde punch 's player.
        //! Depending on the reaction time will give which either dogle or take hit!
        if ((actionTarget == ProcessAction.ETypeAction.HeadAtk ||
            actionTarget == ProcessAction.ETypeAction.KidneyAtk ||
            actionTarget == ProcessAction.ETypeAction.StomachAtk))
        {
            if (actionOwner == ProcessAction.ETypeAction.Idle)
                StartCoroutine(IEAIDogle());
        }
        else
        {
            if (m_timeToAtack)
            {
                for (int indexSkill = 0; indexSkill < m_skillOrderList.Count; indexSkill++)
                {
                    if (!m_skillList[(int)m_skillOrderList[indexSkill]].IsCooldownSkill())
                    {
                        m_skillList[(int)m_skillOrderList[indexSkill]].Active();
                        m_timeToAtack = false;

                        StartCoroutine(IECountDownIntervalBeforeAttack());

                        // Debug.Log(m_skillOrderList[indexSkill]);
                        break;
                    }
                }
            }
        }
    }

    IEnumerator IEAIDogle()
    {
        yield return new WaitForSeconds(m_timeReact);
        this.Dogde();
        yield break;
    }

    IEnumerator IECountDownIntervalBeforeAttack()
    {
        yield return new WaitForSeconds(m_intervalBeforAttack);
        m_timeToAtack = true;
        yield  break;
        
    }
    


}

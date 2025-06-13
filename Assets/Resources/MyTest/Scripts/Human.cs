using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

using Random = UnityEngine.Random;

public class Human : MonoBehaviour
{

    public enum Character
    {
        Player = 0,
        Homeless,
        Boss,
        Count
    }


    [SerializeField] protected GameObject m_target;

    [SerializeField] protected Animator m_Animator;

    protected Vector3 m_backPosition;
    protected float m_distanceBack;

    [SerializeField] protected ProcessAction.ETypeAction m_currAction;

    [SerializeField] protected List<ProcessAction.InfoAction> m_infoSkillList;
    [SerializeField] protected List<ProcessAction> m_skillList;
    [SerializeField] protected Health m_health;
    protected Character m_typeCharacter;

    public Human.Character GetTypeCharacter()
    {
        return m_typeCharacter;
    }



    protected virtual void Awake()
    {
        m_distanceBack = Config.GetInstance().m_distanceBack;
        m_currAction = ProcessAction.ETypeAction.None;
        m_skillList = new List<ProcessAction>();
    }


    public void Reset()
    {
        for (int i = 0; i < m_skillList.Count; i++)
            m_skillList[i].Reset();

        m_target = null;

        m_currAction = ProcessAction.ETypeAction.None;
        m_health.Reset();

        // m_infoSkillList;
    
    }

    public ref List<ProcessAction.InfoAction> GetInfoSkillList()
    {
        return ref m_infoSkillList;
    }

    public ProcessAction.InfoAction getCurrInfoActionByTypeAction(ProcessAction.ETypeAction typeAction)
    {
        foreach (ProcessAction.InfoAction infoAction in m_infoSkillList)
        {
            if (infoAction.m_action == typeAction)
                return infoAction;

        }
        return new ProcessAction.InfoAction();
    }


    protected virtual void LoadListInfoAction()
    {
    }

    protected void LoadListAnimSkill()
    {

        if (transform.Find("SkillGameObject"))
            return;

        GameObject skillGameObject = new GameObject("SkillGameObject");
        
        skillGameObject.transform.parent = transform;
        skillGameObject.transform.position = Vector3.zero;


        ProcessAction processAction = null;

        foreach (ProcessAction.InfoAction _obj in m_infoSkillList)
        {
            processAction = null;
            processAction = skillGameObject.AddComponent<ProcessAction>();

            processAction.SetInfoAction(_obj);

            processAction.SetAnim(ref m_Animator);
            processAction.SetSkillOwner(this);
            // Debug.Log(processAction);
            m_skillList.Add(processAction);
        }
        //--------------------------------------------------------------------------

    }


    public ProcessAction.ETypeAction GetCurrAction()
    {
        return m_currAction;
    }

    public void SetCurrAction(ProcessAction.ETypeAction typeAction)
    {
        m_currAction = typeAction;
    }

    public void SetTarget(GameObject gameObject)
    {
        m_target = gameObject;
    }

    public bool IsApproach()
    {

        // //! Return if human object not in the following 3 states
        // if (!((m_currAction == ProcessAction.ETypeAction.Idle) ||
        //       (m_currAction == ProcessAction.ETypeAction.Approach) ||
        //       (m_currAction == ProcessAction.ETypeAction.None)))
        //     return false;

        float distance = Vector3.Distance(m_target.transform.position, transform.position);

        if (distance > Config.GetInstance().m_distanceAttack)
        {
            m_Animator.SetBool("IsApproaching", true);
            transform.position = Vector3.MoveTowards(transform.position,
                                                    m_target.transform.position,
                                                    (Time.deltaTime * Config.GetInstance().m_speedApproach));

            transform.Rotate(0, transform.rotation.y, 0);

            m_currAction = ProcessAction.ETypeAction.Approach;

            return true;
        }
        else
        {

            if (m_currAction == ProcessAction.ETypeAction.Approach)
            {
                m_currAction = ProcessAction.ETypeAction.Idle;
                m_Animator.SetBool("IsApproaching", false);
            }

            return false;
        }


    }

    protected bool SkillAvailable(ProcessAction.ETypeAction typeAction)
    {
        return m_skillList[(int)typeAction].IsCooldownSkill();
    }





    protected virtual void Update()
    {
        if (m_target == null)
            return;

        transform.LookAt(m_target.transform);

        //!  Human object approach other.
        if (IsApproach())
            return;


        if (m_currAction == ProcessAction.ETypeAction.Dodge)
            transform.position = Vector3.MoveTowards(transform.position,
                                                    m_backPosition,
                                                    (Time.deltaTime * Config.GetInstance().m_distanceBack));

        // Debug.Log("State :" + m_currAction);

        AI();

    }

    protected virtual void AI()
    {

    }

    IEnumerator IEIdle()
    {
        yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);
        m_currAction = ProcessAction.ETypeAction.Idle;
    }

    //! Attack skill
    //-------------------------------------------------------------------------------------------

    public void HeadPunch()
    {

        if (m_currAction != ProcessAction.ETypeAction.Idle)
            return;

        m_skillList[(int)ProcessAction.ETypeAction.HeadAtk].Active();

    }

    public void KidneyPunch()
    {
        if (m_currAction != ProcessAction.ETypeAction.Idle)
            return;

        m_skillList[(int)ProcessAction.ETypeAction.KidneyAtk].Active();

    }

    public void StomachPunch()
    {
        if (m_currAction != ProcessAction.ETypeAction.Idle)
            return;

        m_skillList[(int)ProcessAction.ETypeAction.StomachAtk].Active();

    }



    //! Be hurted
    //-------------------------------------------------------------------------------------------

    public void HeadHit()
    {
        // Debug.Log("HeadHit");
        m_skillList[(int)ProcessAction.ETypeAction.HeadHit].Active();

    }

    public void KidneyHit()
    {
        // Debug.Log("KidneyHit");
        m_skillList[(int)ProcessAction.ETypeAction.KidneyHit].Active();

    }

    public void StomachHit()
    {
        // Debug.Log("StomachHit");
        m_skillList[(int)ProcessAction.ETypeAction.StomachHit].Active();
    }


    //-------------------------------------------------------------------------------------------


    //! Defend - Dodge
    public void Dogde()
    {

        if (m_currAction != ProcessAction.ETypeAction.Idle)
            return;

        m_skillList[(int)ProcessAction.ETypeAction.Dodge].Active();

        CalculateBackVector();

    }

    // ! Not done.
    void CalculateBackVector()
    {

        // float angle = transform.rotation.y + 180;
        // m_backPosition = new Vector3(m_distanceBack / Mathf.Sin(angle * Mathf.Deg2Rad),
        //                             transform.position.y,
        //                             m_distanceBack / Mathf.Cos(angle * Mathf.Deg2Rad));


        m_backPosition = new Vector3(m_distanceBack / Mathf.Sin(-transform.rotation.y * Mathf.Deg2Rad),
                                    transform.position.y,
                                    m_distanceBack / Mathf.Cos((180 - transform.rotation.y) * Mathf.Deg2Rad));



    }


    public void KnockedOut()
    {
        // UnityEngine.Debug.Log("KnockedOut " + m_typeCharacter);
        m_skillList[(int)ProcessAction.ETypeAction.KnockedOut].Active();
    }



    //! Win dance
    //-------------------------------------------------------------------------------------------
    public void VictoryDance()
    {
        m_Animator.SetTrigger("VictoryDance");
        m_currAction = ProcessAction.ETypeAction.Win;
    }

    public void HiphopDance()
    {
        m_Animator.SetTrigger("HiphopDance");
        m_currAction = ProcessAction.ETypeAction.Win;
    }

    public void SillyDance()
    {
        m_Animator.SetTrigger("SillyDance");
        m_currAction = ProcessAction.ETypeAction.Win;
    }

    public void DanceWin()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                this.VictoryDance();
                break;
            case 1:
                this.HiphopDance();
                break;
            case 2:
                this.SillyDance();
                break;
        }
    }

    //-------------------------------------------------------------------------------------------


}
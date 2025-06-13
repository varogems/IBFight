using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessAction : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    InfoAction m_infoAction;
    [SerializeField]bool m_isCooldown;


    //! Skill Owner : Human, Player, Homeless, Boss.
    Human m_skillOwner;
    ETypeAction m_originActionOwner;
    //-------------------------------------------------------



    public enum ETypeAction
    {
        HeadAtk = 0,
        KidneyAtk,
        StomachAtk,

        Dodge,

        HeadHit,
        KidneyHit,
        StomachHit,
        KnockedOut,
        

        Approach,

        Win,
        Idle,
        None

    }

    public enum ConditionClip
    {
        Float = 0,
        Int,
        Bool,
        Trigger
    }

    public struct InfoAction
    {
        public Human.Character m_damageBy;
        public ProcessAction.ETypeAction m_action;
        public ProcessAction.ConditionClip m_conditionClip;
        public String m_nameClipAnim;
        public int m_damageValueOrigin;
        public float m_timeCoolDownSkillOrigin;
    }
    //-------------------------------------------------------



    public void Reset()
    {
        StopAllCoroutines();
        m_isCooldown = false;
        m_originActionOwner = ETypeAction.None;
    }



    public void Active()
    {
        if (!m_isCooldown)
        {

            //! Outside class
            SyncStateOwner();
            StartCoroutine(IECoolDownSkill());
            playAnim();

        }
    }

    //! Outside class: Owner object.
    void SyncStateOwner()
    {
        m_originActionOwner = m_skillOwner.GetCurrAction();
        m_skillOwner.SetCurrAction(m_infoAction.m_action);

        // Debug.Log("SyncStateOwner: " + m_skillOwner.GetCurrAction());
    }

    
    IEnumerator IECoolDownSkill()
    {
        m_isCooldown = true;
        yield return new WaitForSeconds(m_infoAction.m_timeCoolDownSkillOrigin);
        m_isCooldown = false;
        
        yield break;
    }

    public void playAnim()
    {
        switch (m_infoAction.m_conditionClip)
        {
            case ConditionClip.Float:
                break;
            case ConditionClip.Int:
                break;
            case ConditionClip.Bool:
                break;
            case ConditionClip.Trigger:
                m_animator.SetTrigger(m_infoAction.m_nameClipAnim);
                break;
        }
        StartCoroutine(IEWaitAnimDone());
    }

    IEnumerator IEWaitAnimDone()
    {
        yield return new WaitForSeconds(m_animator.GetCurrentAnimatorStateInfo(0).length);

        //! Outside class: Return origin state owner
        m_skillOwner.SetCurrAction(m_originActionOwner);

        // Debug.Log("Aft Action: ");
        
        yield break;
    }

    

    public bool IsCooldownSkill()
    {
        return m_isCooldown;
    }




    public void SetSkillOwner(Human human)
    {
        m_skillOwner = human;
    }

    public void SetAnim(ref Animator animator)
    {
        m_animator = animator;
    }

    public void SetInfoAction(ProcessAction.InfoAction infoAction)
    {
        m_infoAction = infoAction;
    }

    public ProcessAction.InfoAction GetInfoAction()
    {
        return m_infoAction;
    }
    

}

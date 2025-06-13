using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] Human m_owner;
    // ProcessAction.ETypeAction m_currActionDamage;

    void Awake()
    {
        //human = this.gameObject.transform.root.GetComponent<Human>();
    }

    //! Step 1: call this step to get type action before it turn other type(idle)
    public ProcessAction.ETypeAction GetCurTypeAction()
    {
        return m_owner.GetCurrAction();
    }

    public void Win()
    {
        m_owner.DanceWin();
    }


    //! Step 2: After get type at that time, we proceed get info about type action
    public ProcessAction.InfoAction getInfoActionByType(ProcessAction.ETypeAction typeAction)
    {
        return m_owner.getCurrInfoActionByTypeAction(typeAction);
    }

    public Human GetOwner()
    {
        return m_owner;
    }

}

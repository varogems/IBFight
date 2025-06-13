using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{

    protected override void Awake()
    {
        base.Awake();
        m_typeCharacter = Character.Player;

        LoadListInfoAction();
        LoadListAnimSkill();

    }
    protected override void LoadListInfoAction()
    {
        //! Start Config.
        Config.GetInstance();
        
        m_infoSkillList = Config.GetInstance().GetListActionByChar(Character.Player);


    }

    public void Skill1()
    {
        this.HeadPunch();
    }

    public void Skill2()
    {
        this.KidneyPunch();
    }

    public void Skill3()
    {
        this.StomachPunch();
    }


    public void Defend()
    {
        this.Dogde();
    }

    protected override void Update()
    {
        base.Update();
    }

}

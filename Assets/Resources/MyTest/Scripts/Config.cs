using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{

    public struct DifficultyLevel
    {
        public int m_level;
        public float m_ratioAttack;
        public float m_timeReact;
        public float m_ratioTimeCooldown;
        public float m_intervalBeforAttack;
    }

    //------------------------------------------------------------------
    List<KeyValuePair<Human.Character, List<ProcessAction.InfoAction>>> m_listCharSkills;
    List<DifficultyLevel> m_listDifficultyLevel;
    public float m_distanceAttack = 0.61f;
    public float m_speedApproach = 1f;

    public float m_distanceBack = 0.5f;
    private static Config m_instance;

    void InitSkill()
    {
        ProcessAction.InfoAction skill;
        List<ProcessAction.InfoAction> list;
        m_listCharSkills = new List<KeyValuePair<Human.Character, List<ProcessAction.InfoAction>>>();


        //!Player
        //---------------------------------------------------------------------------------------------
        skill = new ProcessAction.InfoAction();
        list = new List<ProcessAction.InfoAction>();
        list.Clear();


        skill.m_damageBy = Human.Character.Player;
        skill.m_action = ProcessAction.ETypeAction.HeadAtk;
        skill.m_damageValueOrigin = 25;
        skill.m_timeCoolDownSkillOrigin = 2f;
        skill.m_nameClipAnim = "HeadPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Player;
        skill.m_action = ProcessAction.ETypeAction.KidneyAtk;
        skill.m_damageValueOrigin = 15;
        skill.m_timeCoolDownSkillOrigin = 2f;
        skill.m_nameClipAnim = "KidneyPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Player;
        skill.m_action = ProcessAction.ETypeAction.StomachAtk;
        skill.m_damageValueOrigin = 15;
        skill.m_timeCoolDownSkillOrigin = 2.2f;
        skill.m_nameClipAnim = "StomachPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Player;
        skill.m_action = ProcessAction.ETypeAction.Dodge;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 2.3f;
        skill.m_nameClipAnim = "Dodge";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Player;
        skill.m_action = ProcessAction.ETypeAction.HeadHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "HeadHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Player;
        skill.m_action = ProcessAction.ETypeAction.KidneyHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "KidneyHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Player;
        skill.m_action = ProcessAction.ETypeAction.StomachHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "StomachHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Player;
        skill.m_action = ProcessAction.ETypeAction.Idle;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "KnockedOut";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        m_listCharSkills.Add(new KeyValuePair<Human.Character, List<ProcessAction.InfoAction>>(Human.Character.Player, list));
        //---------------------------------------------------------------------------------------------


        //!Homeless
        //---------------------------------------------------------------------------------------------
        skill = new ProcessAction.InfoAction();
        list = new List<ProcessAction.InfoAction>();
        list.Clear();

        skill.m_damageBy = Human.Character.Homeless;
        skill.m_action = ProcessAction.ETypeAction.HeadAtk;
        skill.m_damageValueOrigin = 25;
        skill.m_timeCoolDownSkillOrigin = 2f;
        skill.m_nameClipAnim = "HeadPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Homeless;
        skill.m_action = ProcessAction.ETypeAction.KidneyAtk;
        skill.m_damageValueOrigin = 15;
        skill.m_timeCoolDownSkillOrigin = 2f;
        skill.m_nameClipAnim = "KidneyPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Homeless;
        skill.m_action = ProcessAction.ETypeAction.StomachAtk;
        skill.m_damageValueOrigin = 15;
        skill.m_timeCoolDownSkillOrigin = 2.2f;
        skill.m_nameClipAnim = "StomachPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Homeless;
        skill.m_action = ProcessAction.ETypeAction.Dodge;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 2.2f;
        list.Add(skill);
        skill.m_nameClipAnim = "Dodge";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;

        skill.m_damageBy = Human.Character.Homeless;
        skill.m_action = ProcessAction.ETypeAction.HeadHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "HeadHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Homeless;
        skill.m_action = ProcessAction.ETypeAction.KidneyHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "KidneyHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Homeless;
        skill.m_action = ProcessAction.ETypeAction.StomachHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "StomachHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);


        
        skill.m_damageBy = Human.Character.Homeless;
        skill.m_action = ProcessAction.ETypeAction.KnockedOut;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "KnockedOut";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        m_listCharSkills.Add(new KeyValuePair<Human.Character, List<ProcessAction.InfoAction>>(Human.Character.Homeless, list));
        //---------------------------------------------------------------------------------------------



        //!Boss
        //---------------------------------------------------------------------------------------------
        skill = new ProcessAction.InfoAction();
        list = new List<ProcessAction.InfoAction>();
        list.Clear();

        skill.m_damageBy = Human.Character.Boss;
        skill.m_action = ProcessAction.ETypeAction.HeadAtk;
        skill.m_damageValueOrigin = 35;
        skill.m_timeCoolDownSkillOrigin = 3.5f;
        skill.m_nameClipAnim = "HeadPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Boss;
        skill.m_action = ProcessAction.ETypeAction.KidneyAtk;
        skill.m_damageValueOrigin = 25;
        skill.m_timeCoolDownSkillOrigin = 2.8f;
        skill.m_nameClipAnim = "KidneyPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Boss;
        skill.m_action = ProcessAction.ETypeAction.StomachAtk;
        skill.m_damageValueOrigin = 20;
        skill.m_timeCoolDownSkillOrigin = 2.5f;
        skill.m_nameClipAnim = "StomachPunch";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);


        skill.m_damageBy = Human.Character.Boss;
        skill.m_action = ProcessAction.ETypeAction.Dodge;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 2.2f;
        skill.m_nameClipAnim = "Dodge";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);



        skill.m_damageBy = Human.Character.Boss;
        skill.m_action = ProcessAction.ETypeAction.HeadHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "HeadHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Boss;
        skill.m_action = ProcessAction.ETypeAction.KidneyHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "KidneyHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        skill.m_damageBy = Human.Character.Boss;
        skill.m_action = ProcessAction.ETypeAction.StomachHit;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "StomachHit";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);

        
        skill.m_damageBy = Human.Character.Boss;
        skill.m_action = ProcessAction.ETypeAction.KnockedOut;
        skill.m_damageValueOrigin = 0;
        skill.m_timeCoolDownSkillOrigin = 0f;
        skill.m_nameClipAnim = "KnockedOut";
        skill.m_conditionClip = ProcessAction.ConditionClip.Trigger;
        list.Add(skill);


        m_listCharSkills.Add(new KeyValuePair<Human.Character, List<ProcessAction.InfoAction>>(Human.Character.Boss, list));
        //---------------------------------------------------------------------------------------------


        // //! Test Data is correct in list.
        // foreach (KeyValuePair<Human.Character, List<ProcessAction.InfoAction>> _objChar in m_listCharSkills)
        // {
        //     Debug.Log("------------------------------------------");
        //     Debug.Log("Name: " + _objChar.Key);

        //     foreach (ProcessAction.InfoAction _obj in _objChar.Value)
        //     {
        //         Debug.Log("---> " + _obj.m_damageBy + " " + _obj.m_nameClipAnim + " " + _obj.m_timeCoolDownSkillOrigin);
        //     }

        // }

    }

    void InitLevel()
    {
        m_listDifficultyLevel = new List<DifficultyLevel>();
        DifficultyLevel level = new DifficultyLevel();

        m_listDifficultyLevel.Add(level);


        //---------------------------------------------------------------------------------------------

        level.m_level = 1;
        level.m_ratioAttack = 1f;
        level.m_timeReact = 1.5f;
        level.m_ratioTimeCooldown = 2.9f;
        level.m_intervalBeforAttack = 7.5f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 2;
        level.m_ratioAttack = 1.1f;
        level.m_timeReact = 1.0f;
        level.m_ratioTimeCooldown = 1.5f;
        level.m_intervalBeforAttack = 2.5f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 3;
        level.m_ratioAttack = 1.3f;
        level.m_timeReact = 0.9f;
        level.m_ratioTimeCooldown = 2.9f;
        level.m_intervalBeforAttack = 7.5f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 4;
        level.m_ratioAttack = 1.3f;
        level.m_timeReact = 0.75f;
        level.m_ratioTimeCooldown = 2.9f;
        level.m_intervalBeforAttack = 7.0f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 5;
        level.m_ratioAttack = 1.32f;
        level.m_timeReact = 0.75f;
        level.m_ratioTimeCooldown = 2.5f;
        level.m_intervalBeforAttack = 4.5f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 6;
        level.m_ratioAttack = 1.33f;
        level.m_timeReact = 0.69f;
        level.m_ratioTimeCooldown = 1.5f;
        level.m_intervalBeforAttack = 2.5f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 7;
        level.m_ratioAttack = 1.35f;
        level.m_timeReact = 0.6f;
        level.m_ratioTimeCooldown = 2.9f;
        level.m_intervalBeforAttack = 7.0f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 8;
        level.m_ratioAttack = 1.32f;
        level.m_timeReact = 0.3f;
        level.m_ratioTimeCooldown = 2.5f;
        level.m_intervalBeforAttack = 4.9f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 9;
        level.m_ratioAttack = 1.4f;
        level.m_timeReact = 0.3f;
        level.m_ratioTimeCooldown = 1.0f;
        level.m_intervalBeforAttack = 1.0f;
        m_listDifficultyLevel.Add(level);


        level.m_level = 10;
        level.m_ratioAttack = 1.5f;
        level.m_timeReact = 0.1f;
        level.m_ratioTimeCooldown = 1.5f;
        level.m_intervalBeforAttack = 1.8f;
        m_listDifficultyLevel.Add(level);

    }


    public List<ProcessAction.InfoAction> GetListActionByChar(Human.Character human)
    {
        foreach (KeyValuePair<Human.Character, List<ProcessAction.InfoAction>> _obj in m_listCharSkills)
            if (_obj.Key == human)
                return _obj.Value;

        return null;
    }

    public DifficultyLevel GetDifficultyLevel(int indexLevel)
    {
        return m_listDifficultyLevel[indexLevel];
    }


    public void Init()
    {
        InitLevel();
        InitSkill();
    }

    public static Config GetInstance()
    {
        if (m_instance == null)
        {
            m_instance = new Config();
            m_instance.Init();
        }

        return m_instance;
    }

    private Config()
    { 
    }
}

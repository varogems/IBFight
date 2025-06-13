using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Player m_player;

    public void ButtonSkill1()
    {
        m_player.Skill1();
    }

    public void ButtonSkill2()
    {

        m_player.Skill2();
    }

    public void ButtonSkill3()
    {

        m_player.Skill3();
    }

    public void ButtonDefend()
    {

        m_player.Defend();
    }

    public void HeadHit()
    {
        m_player.HeadHit();
    }

    public void KidneyHit()
    {
        m_player.KidneyHit();
    }

    public void StomachHit()
    {
        m_player.StomachHit();
    }

    IEnumerator IEWaitGetPlayer()
    {
        yield return new WaitWhile(() =>
        {
            m_player = FindObjectOfType<Player>();
            return m_player == null;
        });

        yield break;
    }

    void Awake()
    {
        StartCoroutine(IEWaitGetPlayer());
    }


    //! Cheat Input keydown on PC
    void Update()
    {
        //! Atk
        if (Input.GetKeyDown(KeyCode.A))
            this.ButtonSkill1();
        if (Input.GetKeyDown(KeyCode.W))
            this.ButtonSkill2();
        if (Input.GetKeyDown(KeyCode.S))
            this.ButtonSkill3();
        if (Input.GetKeyDown(KeyCode.D))
            this.ButtonDefend();

        //! Be Hurted.
        if (Input.GetKeyDown(KeyCode.J))
            this.HeadHit();
        if (Input.GetKeyDown(KeyCode.K))
            this.KidneyHit();
        if (Input.GetKeyDown(KeyCode.L))
            this.StomachHit();

    }

    

}

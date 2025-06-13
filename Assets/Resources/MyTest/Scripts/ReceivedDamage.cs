using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivedDamage : MonoBehaviour
{
    //! Owner
    [SerializeField] Human m_owner;
    [SerializeField] Health m_healthOwner;


    bool m_beBeaten = false;


    DamageDealer m_damageDealer;

    void OnTriggerEnter(Collider other)
    {

        //! Get root wrong when this game object belong another object.
        // Human owner = this.transform.root.GetComponent<Human>();

        m_damageDealer = other.GetComponent<DamageDealer>();


        if (m_damageDealer != null)
        {
            ProcessAction.ETypeAction typeAction = m_damageDealer.GetCurTypeAction();



            if (!(typeAction == ProcessAction.ETypeAction.HeadAtk ||
                typeAction == ProcessAction.ETypeAction.KidneyAtk ||
                typeAction == ProcessAction.ETypeAction.StomachAtk))
                return;

            // Debug.Log("-----------> " + typeAction);
            // Debug.Log("Damage dealer: " + other.transform.root.name);
            // Debug.Log("Take dealer: " + m_owner.transform.name);

            ProcessAction.InfoAction infoAction = m_damageDealer.getInfoActionByType(typeAction);

            if (m_beBeaten)
                return;



            // Debug.Log("infoAction " + infoAction);
            m_healthOwner.TakeDamage(infoAction.m_damageValueOrigin);

            if (m_healthOwner.CurrentHealth() < 0)
            {
                //! If Player so load Scene Menu.
                if (m_owner.gameObject.name.Contains("Player"))
                {
                    m_owner.KnockedOut();
                    GenerateLevel.m_instance.LoadSceneMenu();
                }
                else
                {
                    m_owner.KnockedOut();

                    StartCoroutine(IEWaitAnimKnockout());
                    


                }

                return;

            }
            switch (typeAction)
            {
                case ProcessAction.ETypeAction.HeadAtk:
                    m_owner.HeadHit();
                    break;
                case ProcessAction.ETypeAction.KidneyAtk:
                    m_owner.KidneyHit();
                    break;
                case ProcessAction.ETypeAction.StomachAtk:
                    m_owner.StomachHit();
                    break;
            }

            m_healthOwner.TakeDamage(infoAction.m_damageValueOrigin);



            StartCoroutine(IEBeBeaten());
        }
    }

    IEnumerator IEBeBeaten()
    {
        m_beBeaten = true;
        yield return new WaitForSeconds(1.93f);
        m_beBeaten = false;
        yield break;
    }


    IEnumerator IEWaitAnimKnockout()
    {
        yield return new WaitForSeconds(1.9f);
        m_owner.gameObject.SetActive(false);
    }
}

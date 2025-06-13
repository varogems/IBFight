using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider m_healthBar;
    Player m_player;
    Health m_healthPlayer;
    int m_health;


    void Awake()
    {
        StartCoroutine(IEWaitGetPlayer());
    }

    IEnumerator IEWaitGetPlayer()
    {
        yield return new WaitWhile(() =>
        {
            m_player = FindObjectOfType<Player>();
            return m_player == null;
        });

        m_healthPlayer = m_player.gameObject.GetComponent<Health>();
        m_healthBar.maxValue = 100;

        yield break;
    }


    // Update is called once per frame
    void Update()
    {
        if (m_healthPlayer != null)
        {
            m_health = m_healthPlayer.CurrentHealth();

            // if (m_health < 0)
            //     GenerateLevel.m_instance.LoadSceneMenu();
                
            m_healthBar.value = m_health;

        }
    }
    
}

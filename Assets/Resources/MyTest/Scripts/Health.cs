using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int m_health = 100;

    public void TakeDamage(int damage)
    {
        m_health -= damage;
        // Debug.Log("Health: " + m_health);
    }

    public int CurrentHealth()
    {
        return m_health;
    }

    public void Reset()
    {
        m_health = 100;
    }
}

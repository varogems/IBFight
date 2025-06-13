using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    [SerializeField] Transform m_transformTarget;
    // Start is called before the first frame update
    float m_Distance;
    float m_speed = 1f;


    // Update is called once per frame
    void Update()
    {

        m_Distance = Vector3.Distance(this.transform.position, m_transformTarget.position);

        if (m_Distance > 0.9f)
        {
            transform.LookAt(m_transformTarget);
            transform.position = Vector3.MoveTowards(transform.position, m_transformTarget.position, (Time.deltaTime * m_speed));
            transform.Rotate(0, transform.rotation.y, 0);

            // transform.Translate((m_transformTarget.position - transform.position).normalized * (Time.deltaTime * m_speed));
        }
    }
}

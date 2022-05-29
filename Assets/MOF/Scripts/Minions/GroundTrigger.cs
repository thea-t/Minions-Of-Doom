using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    [SerializeField] private MinionBase m_Minion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Desk")
        {
            m_Minion.OnTableCollided();
            //GetComponent<Collider>().enabled = false;
                
        }
    }

    private void Reset()
    {
        m_Minion = GetComponentInParent<MinionBase>();
    }
}

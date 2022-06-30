using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour 
{
    [SerializeField] private int m_Speed;

    // Update is called once per frame
    void Update()
    {
        LookAt(GameManager.Instance.Player.transform);
        Debug.Log("UPDATE ");
    }

    void LookAt(Transform _transform) 
    {
        if (_transform != null) 
        {
            Quaternion rot = Quaternion.LookRotation(_transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * m_Speed);
        }
    }
}

using UnityEngine;

/// <summary>
/// This class rotates this object to face the player every frame
/// </summary>
public class LookAtPlayer : MonoBehaviour 
{
    [SerializeField] private int m_Speed;

    void Update()
    {
        LookAt(GameManager.Instance.Player.transform);
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

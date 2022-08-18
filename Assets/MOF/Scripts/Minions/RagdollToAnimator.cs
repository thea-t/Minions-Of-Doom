using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollToAnimator : MonoBehaviour
{
    [SerializeField] private Collider headCollider;
    private Rigidbody[] rigids;
    private Animator anim;


    private void Start()
    {
        rigids = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        ToggleRagdoll(false);
    }

    /// <summary>
    /// Toggles between ragdoll and animator mode.
    /// </summary>
    /// <param name="_isRagdolled"></param>
    public void ToggleRagdoll(bool _isRagdolled)
    {
        if (!_isRagdolled)
        {
            transform.position = rigids[3].position;
        }

        headCollider.enabled = !_isRagdolled;

        foreach (Rigidbody ragdollBone in rigids)
        {
            ragdollBone.isKinematic = !_isRagdolled;
        }

        anim.enabled = !_isRagdolled;
    }
}

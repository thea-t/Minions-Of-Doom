using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollToAnimator : MonoBehaviour
{
    [SerializeField] Collider headCollider;
    Rigidbody[] rigids;
    Animator anim;
    bool isRagdolled = false;


    private void Start()
    {
        rigids = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool _isRagdolled)
    {
        if (!_isRagdolled)
        {
            transform.position = rigids[3].position;
        }
        isRagdolled = _isRagdolled;

        //headCollider.enabled = !_isRagdolled;

        foreach (Rigidbody ragdollBone in rigids)
        {
            ragdollBone.isKinematic = !_isRagdolled;
        }

        anim.enabled = !_isRagdolled;

    }
}

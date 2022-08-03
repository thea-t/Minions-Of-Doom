using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class MinionGrabber : GrabbableEvents
{
    [SerializeField] MinionBase m_Minion;
    public override void OnGrab(Grabber grabber)
    {
        m_Minion.OnGrab();
        base.OnGrab(grabber);
    }

    private void Start() {
        m_Minion = GetComponentInParent<MinionBase>();
    }

    private void Reset() {
        m_Minion = GetComponentInParent<MinionBase>();
    }
}

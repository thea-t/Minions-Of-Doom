using BNG;
using UnityEngine;

/// <summary>
/// This class overrides GrabbableEvents class which comes from VR Interaction Framework, and triggers OnGrab events on minions.
/// </summary>
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

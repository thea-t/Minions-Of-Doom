using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ElementalMinionBase : MinionBase
{
    //Setting the cart type when the script is reset 
    protected override void Reset()
    {
        base.Reset();
        m_MinionType = MinionType.Elemental;
    }

}

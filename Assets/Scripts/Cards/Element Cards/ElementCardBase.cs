using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ElementCardBase : CardBase
{
    protected override void Reset()
    {
        base.Reset();
        m_CardType = CardType.Element;
    }

}

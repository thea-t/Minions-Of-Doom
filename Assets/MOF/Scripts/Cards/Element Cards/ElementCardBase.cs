using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ElementCardBase : CardBase
{
    //Setting the cart type when the script is reset 
    protected override void Reset()
    {
        base.Reset();
        m_CardType = CardType.Element;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCardBase : CardBase
{ 
    //Setting the cart type when the script is reset 
    protected override void Reset()
    {
        base.Reset();
        m_CardType = CardType.Minion;
    }

}

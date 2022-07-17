using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice: MonoBehaviour
{ 
   public string buttonDesc;
   public Action choiceSelected;
   public virtual void OnExecute()
   {
      
      choiceSelected?.Invoke();
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base choice class
/// </summary>
public class Choice : MonoBehaviour
{
   public string buttonDesc;
   public Action choiceSelected;

   /// <summary>
   /// This is called every time player makes this choice. This is overriden by its inheritors.
   /// </summary>
   public virtual void OnExecute()
   {
      choiceSelected?.Invoke();
   }
}
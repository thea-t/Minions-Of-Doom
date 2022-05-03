using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElementPicker : MonoBehaviour
{
    [SerializeField] private Image[] m_ElementImagesToGet;
    [SerializeField] private Image m_ElementImageToSet;

  //  private PlayerElement m_CurrentElement;
  
    public void OnElementSelected(int elementId)
    {
    //    m_CurrentElement = (PlayerElement)elementId;
    m_ElementImageToSet.sprite = m_ElementImagesToGet[elementId].sprite;
    }

    public void OnConfirm()
    {
        SceneManager.LoadScene(1);
    }
}

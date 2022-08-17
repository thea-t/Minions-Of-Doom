using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Picking an element, setting its image accordingly, and loading the next scene
//See the Menu Scene -> ElementPicker
//See the Menu Scene -> Canvas -> 1. Campaign Panel -> Content -> Magic/Aqua/Nature/Demon -> Button (OnCLick)
public class ElementPicker : MonoBehaviour
{
    [SerializeField] private Image[] m_ElementImagesToGet;
    [SerializeField] private Image m_ElementImageToSet;

    public void OnElementSelected(int elementId)
    {
    m_ElementImageToSet.sprite = m_ElementImagesToGet[elementId].sprite;
    }

    public void OnConfirm()
    {
        SceneManager.LoadScene("Intro");
    }
}

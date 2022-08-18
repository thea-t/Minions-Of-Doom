using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Controls menu element selection window. Picking an element for the menu UI, setting its image accordingly, and loading the next scene when confirmed.
/// </summary>
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastManager : MonoBehaviour
{
    private EventSystem eventSystem;
    private RaycastHit hit;
    private bool isHit = false;
    private LayerMask hitLayer;
        
    void Awake () 
    {
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.LogError("Event System not found!!");
        }
    }

    public void SetLayer(LayerMask hitLayer)
    {
        this.hitLayer = hitLayer;
    }

    private void Update()
    { 
        // Ignore any input while the mouse is over a UI element
        if (eventSystem.IsPointerOverGameObject()) {
            return;
        }
        // TODO: CREATE CAMERA CONTROLLER
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        isHit = Physics.Raycast(ray, out hit, float.PositiveInfinity, hitLayer);
    }

    public T GetByRay<T>() where T : class 
    {
        if (isHit) 
        {
            return hit.transform.GetComponentInParent<T>();
        }
        return null;
    }
}

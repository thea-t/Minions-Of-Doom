using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinionUiPopup : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI costTMP;
    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI descriptionTMP;

    private void LateUpdate()
    {
        transform.LookAt(GameManager.Instance.Player.centerEye.transform);
    }
}

using TMPro;
using UnityEngine;

/// <summary>
/// This class holds ui references to the minion popup window. It also rotates the ui to always face the player.
/// </summary>
public class MinionUiPopup : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI costTMP;
    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI descriptionTMP;
    public TextMeshProUGUI specialPowerTMP;
    public TextMeshProUGUI boosterTMP;

    private void LateUpdate()
    {
        transform.LookAt(GameManager.Instance.Player.centerEye.transform);
    }
}



/// <summary>
/// The special power of the elemental minions is to gain strength.
/// </summary>
public class ElementalMinionBase : MinionBase
{
    /// <summary>
    /// Setting the protected animation which is played by the base class.
    /// </summary>
    void Awake()
    {
        m_MinionType = MinionType.Elemental;
        m_MinionPowerAnimation = "GainStrength";
    }

    /// <summary>
    /// Resetting automatically sets minion type to elemental by default.
    /// </summary>
    protected override void Reset()
    {
        base.Reset();
        m_MinionType = MinionType.Elemental;
    }

    /// <summary>
    /// Animation event. Increases the player's strength.
    /// </summary>
    protected override void GainStrength()
    {
        GameManager.Instance.Player.Strength += m_MinionData.strength;
        GameManager.Instance.UiManager.UpdateStrengthUI(GameManager.Instance.Player.Strength);
        base.GainStrength();

    }
}

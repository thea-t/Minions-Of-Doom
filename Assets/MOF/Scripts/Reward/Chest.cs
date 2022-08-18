using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// This class controls the chest object which is rewarded to the player after killing an enemy.
/// </summary>
public class Chest : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private ParticleSystem m_GlowParticle;
    [SerializeField] private RewardData m_RewardData;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private Vector3 m_EndPos;
    [SerializeField] private EnemyType m_EnemyType;

    /// <summary>
    /// Animate the chest to jump towards player.
    /// </summary>
    private void Start()
    {
        gameObject.transform.DOJump(m_EndPos, 0.2f, 3, 2).onComplete = () => { PlayAnimation(); };
    }

    /// <summary>
    /// Type of the enemy determines what rarity of minion spawns.
    /// </summary>
    public void SetEnemyType(EnemyType enemyType)
    {
        m_EnemyType = enemyType;
    }

    private void PlayAnimation()
    {
        m_Animator.Play("Full");
    }

    /// <summary>
    /// Spawning rewards based on the enemy type. The higher enemy type it is, the more rewards will be spawned
    /// Animation event
    /// </summary>
    [UsedImplicitly]
    private void PlayParticle()
    {
        m_AudioSource.Play();
        m_GlowParticle.Play();

        StartCoroutine(SpawnMinionReward());
    }

    /// <summary>
    /// Coroutine that spawns minion reward after some time and animates the minion.
    /// </summary>
    IEnumerator SpawnMinionReward()
    {
        float spawnDuration = 5;
        MinionBase minionReward =
            Instantiate(m_RewardData.MinionReward(m_EnemyType), transform.position, Quaternion.identity);

        Vector3 endScale = minionReward.SetMinionScale(1.5f);
        minionReward.transform.localScale = Vector3.zero;
        minionReward.transform.LookAt(GameManager.Instance.Player.transform);

        yield return null;
        minionReward.PrepareForDisplaying();

        minionReward.transform.DOMove(new Vector3(0, 0.9f, minionReward.transform.position.z), spawnDuration);

        minionReward.transform.DOScale(endScale, spawnDuration).onComplete = () =>
        {
            Player.WonMinions.Add(minionReward);
            Player.CurrentLevel++;
        };

        yield return new WaitForSeconds(3);

    }
}

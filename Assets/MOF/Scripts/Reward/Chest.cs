using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Animator m_Animator;
    [SerializeField] ParticleSystem m_GlowParticle;
    [SerializeField] RewardData m_RewardData;
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] Vector3 m_EndPos;
    
    public EnemyType EnemyType { get; set; }
    private void Start()
    {
        gameObject.transform.DOJump(m_EndPos, 0.2f, 3, 2).onComplete = () => { PlayAnimation(); };
    }

    private void PlayAnimation()
    {
        m_Animator.Play("Full");
    }

    //spawning rewards based on the enemy type. The higher enemy type it is, the more rewards will be spawned
    // animation event
    private void PlayParticle()
    {
        m_AudioSource.Play();
        m_GlowParticle.Play();
        StartCoroutine(SpawningReward((int) EnemyType));
        
        //TODO THEA CONTINUE FROM HERE / FIND ME
    }

    IEnumerator SpawningReward(int count)
    {            Debug.Log("count");

        float spawnDuration = 0.5f;

        for (int i = 0; i < count; i++)
        {
            Debug.Log("spawned reward!!!!!!!!!!1");
            MinionBase minionReward = Instantiate(m_RewardData.MinionReward(EnemyType), transform.position, Quaternion.identity);

            Vector3 midPos = (minionReward.transform.position + GameManager.Instance.Player.transform.position) / 1.5f;
            midPos = Quaternion.Euler(0, -25 + i * 25, 0) * midPos;

            Vector3 startScale = minionReward.transform.localScale;
            minionReward.transform.localScale = Vector3.zero;
            minionReward.transform.DOScale(startScale, spawnDuration);

            Tween t = minionReward.transform.DOMove(midPos, spawnDuration);
            t.SetEase(Ease.InOutCirc);
            t.onComplete = () =>
            {
                //loot.PlayParticle();
            };
            yield return new WaitForSeconds(spawnDuration);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private Reward m_RewardPrefab;

    public void SpawnRewardPrefab()
    {
        Instantiate(m_RewardPrefab);
    }
}

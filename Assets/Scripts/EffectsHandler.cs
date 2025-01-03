using System.Collections.Generic;
using UnityEngine;

public class EffectsHandler : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particlesList;

    public void Play()
    {
        foreach (ParticleSystem particleSystem in _particlesList)
        {
            particleSystem.Play();
        }
    }
}

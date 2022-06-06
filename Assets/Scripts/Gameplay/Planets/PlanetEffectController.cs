using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fightEffect;

    public void SetFightEffect(bool value)
    {
        if (value)
        {
            fightEffect.Play();
        }
        else
        {
            fightEffect.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource capturedAudioSignal;
    [SerializeField] private AudioClip playerSignal;
    [SerializeField] private AudioClip enemySignal;


    public void PlayCapturedCignal(ShipSide shipSide)
    {
        if (shipSide == ShipSide.Enemy)
        {
            capturedAudioSignal.PlayOneShot(enemySignal);
        }
        else if(shipSide == ShipSide.Player)
        {
            capturedAudioSignal.PlayOneShot(playerSignal);
        }
    }
}

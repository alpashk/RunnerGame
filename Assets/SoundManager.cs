using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    AudioSource aS;

    [SerializeField] AudioClip deathSound = null;
    [SerializeField] AudioClip jumpSound = null;
    [SerializeField] AudioClip landSound = null;
    [SerializeField] AudioClip coinPickup = null;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        playerMove.OnDeath += PlayDeathSound;
        playerMove.OnCoin += PlayCoinSound;
        playerMove.OnJump += PlayJumpSound;
    }

    void PlayDeathSound()
    {
        playerMove.OnDeath -= PlayDeathSound;
        aS.PlayOneShot(deathSound);
    }

    void PlayJumpSound(int dir)
    {
        if(dir>0)
            aS.PlayOneShot(jumpSound);
        else
            aS.PlayOneShot(landSound);
    }

    void PlayCoinSound()
    {
        aS.PlayOneShot(coinPickup);
    }

}

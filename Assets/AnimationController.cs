using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove.OnJump += JumpAnim;
        playerMove.OnDeath+= DeathAnim;
    }

    void JumpAnim(int condition)
    {
        if(condition>0)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }

    void DeathAnim()
    {
        anim.SetBool("Dead", true);
    }
}

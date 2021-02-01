using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMove : MonoBehaviour
{
    public delegate void Death();
    public static event Death OnDeath;

    public delegate void CoinPickup();
    public static event CoinPickup OnCoin;

    public delegate void ScoreUpdate(float score);
    public static event ScoreUpdate Score;

    public static event Action<float> ScoreUpdated = delegate { };
    
    public delegate void JumpManager(int condition);
    public static event JumpManager OnJump;



    [SerializeField] float JumpTime = 1f;
    [SerializeField] float JumpCooldown = .05f;
    [SerializeField] float Speed = .5f;

    SpriteRenderer sr;
    bool jump = false;
    bool canJump = true;
    void Start()
    {
        DragHandler.OnJump += JumpHandler;
        DragHandler.OnMove += MoveHandler;
    }

    IEnumerator Jump()
    {
        canJump = false;
        jump = true;
        OnJump(1);
        yield return new WaitForSeconds(JumpTime);
        if(OnJump != null)
        OnJump(-1);
        jump = false;
        yield return new WaitForSeconds(JumpCooldown);
        canJump = true;
    }


    private void JumpHandler()
    {
        if (canJump)
        {
            StartCoroutine(Jump());
        }
    }

    public void MoveHandler(int dir)
    {
        if (transform.position.x + 1.5f * dir > -1.6 && transform.position.x + 1.5f * dir < 1.6)
        {
            transform.Translate(new Vector3(1.5f * dir, 0, 0));
        }
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, Speed, 0));
        Score(transform.position.y+4);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!jump)
        {
            if (col.tag == "Coin")
            {
                OnCoin();
                col.gameObject.SetActive(false);
            }
            else
            {

                death();
                OnDeath();
            }
        }
        else if(col.tag == "UnjumpableObstacle")
        {
            death();
            OnDeath();
        }
    }

    private void death()
    {
        
        OnCoin = null;
        OnJump = null;
        Score = null;
        this.enabled = false;

    }

    private void OnDestroy()
    {
        OnDeath = null;
    }
}

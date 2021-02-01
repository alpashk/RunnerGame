using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMove : MonoBehaviour
{

    [SerializeField] float JumpTime = 1f;
    [SerializeField] float JumpCooldown = .05f;
    [SerializeField] float Speed = .5f;

    SpriteRenderer sr;
    public bool jump = false;
    public bool canJump = true;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        DragHandler.OnJump += JumpHandler;
        DragHandler.OnMove += MoveHandler;
    }

    IEnumerator Jump()
    {
        canJump = false;
        jump = true;
        sr.color = Color.yellow;
        yield return new WaitForSeconds(JumpTime);
        jump = false;
        sr.color = Color.red;
        yield return new WaitForSeconds(JumpCooldown);
        canJump = true;
        sr.color = Color.white;
    }


    public void JumpHandler()
    {
        if (canJump)
        {
            StartCoroutine(Jump());
        }
    }

    public void MoveHandler(int dir)
    {
        Debug.Log(dir);
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, Speed,0));
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!jump)
        {
            Debug.Log("ded");
        }
        else if(col.tag == "UnjumpableObstacle")
        {
            Debug.Log("ded");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("ded1");
    }void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("ded2");
    }


}

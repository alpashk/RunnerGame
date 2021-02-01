using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public delegate void JumpAction();
    public static event JumpAction OnJump;

    public delegate void MoveAction(int dir);
    public static event MoveAction OnMove;

    private void Start()
    {
        playerMove.OnDeath += Death;
    }

    private void OnDestroy()
    {
        OnJump = null;
        OnMove = null;
    }

    private void Death()
    {
        this.enabled = false;
    }


    public void OnDrag(PointerEventData data)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        int dir =(int) GetDragDirection(dragVectorDirection);
        if(dir == 2)
        {
            OnMove(1);
        }
        else if(dir ==3)
        {
            OnMove(-1);
        }
        else if(dir == 0)
        {
            OnJump();
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (!data.dragging)
        {
            OnJump();
        }
    }



    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        return draggedDir;
    }

}

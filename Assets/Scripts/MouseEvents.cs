using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE { opened, closed }

public class MouseEvents : MonoBehaviour
{
    public Transform Hole;
    public MeshCollider ground;
    private Vector3 initialPosition, closedPos;
    private STATE state = STATE.opened; //holds the state of the hole

    private void Start()
    {
        initialPosition = Hole.position;

        //defines the position of the hole when it is closed.
        closedPos = initialPosition;
        closedPos.y -= 1f;
    }

    private void Update()
    {
        OnTouch();
    }

    //makes the hole opened until the player hits the mouse or touches.
    public void OpenTheHole()
    {
        Hole.position = initialPosition;
        GetComponent<MeshCollider>().enabled = true;
        ground.enabled = false; //when hole is open, there is no need for ground.
    }

    //makes the hole closed until the player gets their finger off.
    public void CloseTheHole()
    {
        Hole.position = closedPos;
        GetComponent<MeshCollider>().enabled = false;
        ground.enabled = true; //the ground is there to handle collisions and prevent objects from falling when the hole is closed.
    }


    //MobileInputs
    private void OnTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                state = STATE.closed;
                CloseTheHole();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                state = STATE.opened;
                OpenTheHole();
            }
        }
    }



    //Desktop Inputs
    void OnMouseDown()
    {
        if (!GameScript.gameOver && state == STATE.opened)
        {
            state = STATE.closed;
            CloseTheHole();
        }
    }

    void OnMouseUp()
    {
        if (state == STATE.closed)
        {
            state = STATE.opened;
            OpenTheHole();
        }
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollider : MonoBehaviour
{

    //handles the score and gameover when objects entered the hole.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "okay")
        {
            GameScript.score++;
        }
        else
        {
            GameScript.gameOver = true;
        }

        GameScript.updateScore = true;

        GameScript.transforms.Remove(collision.gameObject.transform);
        Destroy(collision.gameObject);
        
    }
}

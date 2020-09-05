using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    //takes care of the objects passed behind while hole is closed
    private void OnCollisionEnter(Collision collision)
    {
        GameScript.transforms.Remove(collision.gameObject.transform);
        Destroy(collision.gameObject);
    }
}

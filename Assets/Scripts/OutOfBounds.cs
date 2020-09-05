using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameScript.transforms.Remove(collision.gameObject.transform);
        Destroy(collision.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x > 5f)
        {
            transform.position = new Vector3(5f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -5f)
        {
            transform.position = new Vector3(-5f, transform.position.y, transform.position.z);
        }
        if (transform.position.z > 5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 5f);
        }
        if (transform.position.z < -5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
        }
    }
}

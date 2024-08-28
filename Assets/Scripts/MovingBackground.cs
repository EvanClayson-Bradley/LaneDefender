using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    public int BackgroundMoveSpeed;
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime);

        if (transform.position.x <= -22.3f)
        {
            transform.position = new Vector3(22.3f, transform.position.y, transform.position.z);
        }
    }
}
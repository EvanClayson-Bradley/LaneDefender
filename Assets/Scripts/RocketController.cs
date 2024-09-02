using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private int rocketSpeed;
    [SerializeField] private GameObject explosion;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //moves rocket
        transform.Translate(Vector2.right * rocketSpeed * Time.deltaTime);

        //deletes rocket if off screen to the right
        if (transform.position.x > 11)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //destroy rocket when collides
        Destroy(gameObject);
    }
}

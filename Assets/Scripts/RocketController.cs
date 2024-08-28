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
        transform.Translate(Vector2.right * rocketSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Explode(collision));
    }
    public IEnumerator Explode(Collision2D collision)
    {
        GameObject temp = Instantiate(explosion, collision.transform.position, Quaternion.identity);
        Destroy(gameObject);
        temp.GetComponent<SpriteRenderer>().sortingOrder = 100;
        yield return new WaitForSeconds(1f);
        Destroy(temp);
    }
}

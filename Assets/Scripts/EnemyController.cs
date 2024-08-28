using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int score;
    private Animator anim;
    private int health;
    private bool shouldMove = true;
    private bool hasCollision = true;
    void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        anim.SetTrigger("Walk");
    }
    void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
            // SUBTRACT LIVES
        }
    }

    public IEnumerator Hit()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("Hit");
            shouldMove = false;
            yield return new WaitForSeconds(1.2f);
            anim.SetTrigger("Walk");
            shouldMove = true;
        }
    }

    public IEnumerator Die()
    {
        hasCollision = false;
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}

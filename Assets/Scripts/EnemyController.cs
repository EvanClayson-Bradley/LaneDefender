using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int score;

    [SerializeField] private GameObject explosion;

    private Animator anim;
    private GameController gameController;
    private int health;
    private bool shouldMove = true;
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
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
            gameController.LoseLife();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 3:
                Destroy(gameObject);
                gameController.LoseLife();
                break;
            case 6:
                Hit();
                StartCoroutine(Explosion(collision.transform.position));
                break;
            default: break;
        }
    }

    /// <summary>
    ///     Handles all hit function, starting either die or hit coroutines
    /// </summary>
    void Hit()
    {
        health--;
        if (health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(HitAnim());
        }
        
    }

    /// <summary>
    ///     removes collision, death animation, stops moving, destroys object
    /// </summary>
    /// <returns></returns>
    public IEnumerator Die()
    {
        gameController.AddScore(100);
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("Die");
        shouldMove = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    /// <summary>
    ///     stops movement and plays hit animation, then begins moving again
    /// </summary>
    /// <returns></returns>
    public IEnumerator HitAnim()
    {
        anim.SetTrigger("Hit");
        shouldMove = false;
        yield return new WaitForSeconds(1.2f);
        anim.SetTrigger("Walk");
        shouldMove = true;
    }
    /// <summary>
    ///     creates explosion object, waits for animation, then destroys
    /// </summary>
    /// <param name="pos">position to spawn explosion</param>
    /// <returns></returns>
    public IEnumerator Explosion(Vector3 pos)
    {
        GameObject temp = Instantiate(explosion, pos, Quaternion.identity);
        temp.GetComponent<SpriteRenderer>().sortingOrder = 100;
        yield return new WaitForSeconds(0.3f);
        Destroy(temp);
    }
}

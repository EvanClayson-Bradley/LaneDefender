using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject explosion;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int moveSpeed;
    private float moveDirection;
    private bool canShoot = true;
    private bool isShooting = false;

    [SerializeField] private AudioClip shootSound;

    void Update()
    {
        if (gameController.isPlaying)
        {
            transform.Translate(moveSpeed * Time.deltaTime * moveDirection * Vector2.up);

            if (isShooting && canShoot)
            {
                Vector2 barrelPos = new Vector2(transform.position.x + 0.6f, transform.position.y + 0.3f);
                //spawns rocket & starts cooldown
                Instantiate(rocket, barrelPos, Quaternion.identity);
                AudioSource.PlayClipAtPoint(shootSound, barrelPos);
                StartCoroutine(Explosion(barrelPos));
                StartCoroutine(ShootCooldown());
            }
        }
    }

    /// <summary>
    ///     Called from inputaction, sets move direction to player input
    /// </summary>
    /// <param name="inputValue"></param>
    void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<float>();
    }
    /// <summary>
    ///     Called from inputaction, sets isShooting to if player is holding input
    /// </summary>
    /// <param name="inputValue"></param>
    void OnShoot(InputValue inputValue)
    {
        isShooting = Convert.ToBoolean(inputValue.Get<float>());
    }

    /// <summary>
    ///     prevents player from shooting rockets in too quick succession
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
    public IEnumerator Explosion(Vector3 pos)
    {
        GameObject temp = Instantiate(explosion, pos, Quaternion.identity);
        temp.GetComponent<SpriteRenderer>().sortingOrder = 100;
        yield return new WaitForSeconds(0.3f);
        Destroy(temp);
    }

    void OnDebugRestart()
    {
        SceneManager.LoadScene(0);
    }
}

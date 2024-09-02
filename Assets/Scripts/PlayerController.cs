using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject rocket;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int moveSpeed;
    private float moveDirection;
    private bool canShoot = true;
    private bool isShooting = false;

    void Update()
    {
        if (gameController.isPlaying)
        {
            transform.Translate(moveSpeed * Time.deltaTime * moveDirection * Vector2.up);

            if (isShooting && canShoot)
            {
                //spawns rocket & starts cooldown
                Instantiate(rocket, transform.position, Quaternion.identity);
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator gunAnim;
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDistance = 1.2f;
    private bool gunFacingRight = true;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        gun.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        GunFlipController(mousePos);
    }

    private void GunFlipController(Vector3 mousePos)
    {
        if(mousePos.x<gun.position.x && gunFacingRight)
        {
            FlipGun();
        }
        else if(mousePos.x>gun.position.x && !gunFacingRight)
        {
            FlipGun();
        }   
    }

    private void FlipGun()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);   
    }

    public void Shoot()
    {
        Console.Write("Shoot");
        gunAnim.SetTrigger("Shoot");
    }
}

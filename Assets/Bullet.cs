using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Target")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            UI.instance.AddScore();
        }
    }
}

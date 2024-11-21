using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Sprite[] targetSprite;

    [SerializeField] private BoxCollider2D cd;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float cooldown;
    private float timer;

    private int sushiCreated = 0;
    private int millestone = 5;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = cooldown;
            sushiCreated++;

            if(sushiCreated == millestone && cooldown > 0.5f)
            {
                millestone += 5;
                cooldown -= 0.3f;
            }   

            GameObject newTarget = Instantiate(targetPrefab);

            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);

            newTarget.transform.position = new Vector2(randomX, transform.position.y);

            int randomIndex = Random.Range(0, targetSprite.Length);
            newTarget.GetComponent<SpriteRenderer>().sprite = targetSprite[randomIndex];
        }
    }
}

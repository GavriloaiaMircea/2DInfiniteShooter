using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator gunAnim;
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDistance = 1.2f;
    private bool gunFacingRight = true;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    public int currentBullets;
    public int maxBullets = 15;

    private void Start()
    {
        ReloadGun();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = (mousePos - gun.position).normalized;


        gun.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(direction);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadGun();
        }

        GunFlipController(mousePos);
    }

    private void GunFlipController(Vector3 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
        {
            FlipGun();
        }
        else if (mousePos.x > gun.position.x && !gunFacingRight)
        {
            FlipGun();
        }
    }

    private void FlipGun()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    public void Shoot(Vector3 direction)
    {
        if (currentBullets > 0)
        {
            currentBullets--;
            gunAnim.SetTrigger("Shoot");

            GameObject newBullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            Destroy(newBullet, 7);
        }
        else
        {
            Debug.Log("No bullets left!");
        }
    }

    private void ReloadGun()
    {
        currentBullets = maxBullets;
    }

}

using UnityEngine;

public class gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public float bulletSpeed = 10f;
    public bool equipped = false;
    public int playaNumber;
    public float xOffset = 0f;
    public float yOffset = 0f;
    // time betweem shots in seconds
    public float fireRate = 0.1f;
    private float nextFire = 0.0f;
    public bool automatic = false;
    public int ammo;
    private float throwForce = 3f;
    private float spinForce = 5f;
    public bool thrown = false;
    public bool meele = false;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "playa hitbox" && collider.transform.parent.gameObject.GetComponent<playa>().playaNumber != playaNumber)
        {
            // ouch
            if (thrown)
            {
                collider.transform.parent.gameObject.GetComponent<playa>().health -= 5;
                thrown = false;
                GetComponent<BoxCollider>().isTrigger = false;
                Destroy(gameObject, 5f);
            }

            // big ouch - make sure its equipped bruh
            if (meele && equipped)
            {
                collider.transform.parent.gameObject.GetComponent<playa>().health -= 100;
                GetComponent<BoxCollider>().isTrigger = false;
                drop();
                Destroy(gameObject, 5f);
            }
        }
        else if (collider.gameObject.tag == "ground" && thrown)
        {
            thrown = false;
            GetComponent<BoxCollider>().isTrigger = false;
            Destroy(gameObject, 5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (automatic)
        {
            if (Input.GetKey(KeyCode.R) && equipped && playaNumber == 1 && Time.time > nextFire)
            {
                shoot();
            }
            if (Input.GetKey(KeyCode.I) && equipped && playaNumber == 2 && Time.time > nextFire)
            {
                shoot();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R) && equipped && playaNumber == 1 && Time.time > nextFire)
            {
                shoot();
            }
            if (Input.GetKeyDown(KeyCode.I) && equipped && playaNumber == 2 && Time.time > nextFire)
            {
                shoot();
            }
        }
    }

    // not as cool as throwing it but it is what it is
    void drop()
    {
        transform.parent = null;
        rb.isKinematic = false;
        equipped = false;
    }

    // throw is a taken keyword bruh
    public void yeet()
    {
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(transform.right * throwForce + transform.up * throwForce / 2.8f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * spinForce, ForceMode.Impulse);
        thrown = true;
        equipped = false;
    }

    void shoot()
    {
        if (meele)
        {
            // LMAO such epic coding
            transform.parent.transform.parent.GetComponent<arm>().swing();
        }
        else if (bulletPrefab && bulletSpawn)
        {
            // out of ammo, throw that bish
            if (ammo == 0 && !thrown)
            {
                yeet();
                return;
            }

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bullet.GetComponent<bullet>().playaFired = playaNumber;
            if (bulletRb)
            {
                bulletRb.velocity = bulletSpawn.transform.up * bulletSpeed;
            }
            ammo--;
            nextFire = Time.time + fireRate;
        }
    }
}

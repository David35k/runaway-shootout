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
    public bool shotgun = false;
    // only if applies if shotgun is set to true
    public int shellCount = 0;
    // also only applies when shotgun is set to true
    public float spreadAmount = 1f;
    public float recoilForce = 0f;
    private ParticleSystem muzzleFlash;
    public GameObject blud;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        muzzleFlash = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "playa hitbox" && collider.transform.parent.gameObject.GetComponent<playa>().playaNumber != playaNumber)
        {
            // ouch
            if (thrown)
            {
                collider.transform.parent.gameObject.GetComponent<playa>().health -= 5;
                collider.transform.parent.gameObject.GetComponent<playa>().updateHealthBar();
                thrown = false;
                GetComponent<BoxCollider>().isTrigger = false;
                Destroy(gameObject, 5f);
            }

            // big ouch - make sure its equipped bruh
            if (meele && equipped)
            {
                if (blud)
                {
                    Vector3 triggerPoint = (transform.position + collider.transform.position) / 2;
                    Instantiate(blud, triggerPoint, Quaternion.identity, collider.transform);
                }

                collider.transform.parent.gameObject.GetComponent<playa>().health -= 100;
                collider.transform.parent.gameObject.GetComponent<playa>().updateHealthBar();
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

        // epic mechanic
        if (collider.gameObject.GetComponent<bullet>() && collider.gameObject.GetComponent<bullet>().missile && meele && equipped)
        {
            collider.gameObject.GetComponent<bullet>().kaboom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent)
        {
            // Debug.Log((nextFire - Time.time) / fireRate);
            transform.parent.transform.parent.transform.parent.GetComponent<playa>().updateShootBar(fireRate, nextFire - Time.time);
        }


        if (automatic)
        {
            if (Input.GetKey(KeyCode.R) && equipped && playaNumber == 1 && Time.time > nextFire)
            {
                shoot();
            }
            if (Input.GetKey(KeyCode.P) && equipped && playaNumber == 2 && Time.time > nextFire)
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
            if (Input.GetKeyDown(KeyCode.P) && equipped && playaNumber == 2 && Time.time > nextFire)
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
            return;
        }

        if (bulletPrefab && bulletSpawn)
        {
            // out of ammo, throw that bish
            if (ammo == 0 && !thrown)
            {
                yeet();
                return;
            }
            else
            {
                // some people just wont appreciate this coding
                transform.parent.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce(transform.parent.transform.parent.transform.forward * -1 * recoilForce);
            }

            if (shotgun)
            {
                // this code is whack and doesnt work that well but whatever bruh its just a silly game
                for (int i = 0; i < shellCount; i++)
                {
                    // Offset the bullet's spawn position slightly to avoid immediate collision
                    Vector3 spawnPositionOffset = bulletSpawn.transform.position + Vector3.up * i * 0.1f;

                    GameObject bullet = Instantiate(bulletPrefab, spawnPositionOffset, bulletSpawn.transform.rotation * Quaternion.Euler(Random.insideUnitSphere));
                    bullet.GetComponent<bullet>().playaFired = playaNumber;
                    bullet.GetComponent<bullet>().shoot(bulletSpawn, bulletSpeed, transform.parent.transform.parent.GetComponent<arm>().currentTarget, transform.parent.transform.parent.transform.parent.gameObject);
                }
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bullet.GetComponent<bullet>().playaFired = playaNumber;
                bullet.GetComponent<bullet>().shoot(bulletSpawn, bulletSpeed, transform.parent.transform.parent.GetComponent<arm>().currentTarget, transform.parent.transform.parent.transform.parent.gameObject);
            }

            if (muzzleFlash)
            {
                muzzleFlash.Play();
            }

            nextFire = Time.time + fireRate;
            ammo--;
        }
    }
}

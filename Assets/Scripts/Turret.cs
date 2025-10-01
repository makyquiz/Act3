using UnityEngine;

public class Turret : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public Transform target;

    [Header("Turret Settings")]
    public float fireRange = 5f;
    public float fireCooldown = 2f;
    public float aimThreshold = 0.95f;

    [Header("Projectile")]
    public Bullet bulletPrefab;
    public Transform firePoint;

    private float cooldownTimer = 0f;

    void Update()
    {
        this.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;

        var direction = target.position - transform.position;
        direction.Normalize();
        var targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        var rot = Quaternion.Euler(0, 0, -targetAngle);
        var newRot = Quaternion.Lerp(this.transform.rotation, rot, rotSpeed * Time.deltaTime);
        this.transform.rotation = newRot;

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            float alignment = Vector3.Dot(this.transform.up, direction);

            if (distance < fireRange && alignment > aimThreshold)
            {
                Fire(direction);
                cooldownTimer = fireCooldown;
            }
            else
            {
                Debug.Log("Player not detected");
            }
        }
        Debug.Log($"Distance {Vector3.Distance(target.position, transform.position):F2}, Dot {Vector3.Dot(this.transform.up, direction):F2}.");
    }

    void Fire(Vector3 dir)
    {
        var proj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        proj.direction = dir.normalized;
        proj.target = target;
    }
}
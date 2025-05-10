using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireInterval = 0.5f;
    public float yMin = 1.1f;
    public float yMax = 2.2f;

    private bool isFiring = false;

    public void StartFiring()
    {
        if (!isFiring)
        {
            InvokeRepeating(nameof(FireBullet), 1f, fireInterval);
            isFiring = true;
        }
    }

    void FireBullet()
    {
        float yPos = Random.Range(yMin, yMax);
        Vector3 spawnPos = new Vector3(0f, yPos, 0f);  // ⬅️ Use exact start X
        Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
    }

}

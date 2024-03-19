using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Basic Gun Mechanics")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    private int bulletCount = 10;
    private float reloadTime = 2f;
    private bool canShoot = true; // used to not allow spam of reload
    private float damageMultiplier = 5f;
    private bool damageMultiplierEnabled = false;
    public bool canActivateDamageBoost { get; set; } = false;
    public bool canDamageBoost { get; set; } = false;

    [Header("Gunshot Drawing")]
    [SerializeField] Transform gunTip;
    [SerializeField] Material lineMat;
    private LineRenderer lr;

    RaycastHit hit;

    [Header("Controls")]
    [SerializeField] KeyCode reloadKey = KeyCode.R;
    [SerializeField] KeyCode damageMultiplierKey = KeyCode.E;

    private void Update()
    {
        if (Input.GetKeyDown(reloadKey))
        {
            if (bulletCount < 10 && canShoot) { StartCoroutine(Reload()); }
        }
        if (Input.GetKeyDown(damageMultiplierKey) && canDamageBoost)
        {
            if (canActivateDamageBoost) { StartCoroutine(DamageMultiplierDuration()); }
        }
    }

    private void LateUpdate()
    {
        DrawShot();
    }

    public void Shoot(Transform shootPoint)
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range))
        {
            if (bulletCount > 0 && canShoot)
            {
                if (gameObject.GetComponent<LineRenderer>() == null)
                {
                    lr = gameObject.AddComponent<LineRenderer>();
                }
                lr.positionCount = 2;

                Target currentTarget = hit.transform.GetComponent<Target>();
                if (currentTarget != null)
                {
                    if (damageMultiplierEnabled) { currentTarget.TakeDamage(damage * damageMultiplier); }
                    else { currentTarget.TakeDamage(damage); }
                }

                bulletCount--;
                Debug.Log("Bullets remaining: " + bulletCount);
                if (bulletCount <= 0 && canShoot) { StartCoroutine(Reload()); }
            } else if (canShoot) { StartCoroutine(Reload()); }
        }
    }

    // Draw bullet line
    private void DrawShot()
    {
        if (!lr) return;
        lr.material = lineMat;
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, hit.point);
        StartCoroutine(DeleteShotLine());
    }

    private IEnumerator DeleteShotLine()
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(lr);
    }

    private IEnumerator Reload()
    {
        Debug.Log("Reloading");
        canShoot = false;

        yield return new WaitForSeconds(reloadTime);

        bulletCount = 10;
        Debug.Log("Reload Complete");
        canShoot = true;
    }

    private IEnumerator DamageMultiplierDuration()
    {
        damageMultiplierEnabled = true;
        canActivateDamageBoost = false;
        Debug.Log("Damage boost ENABLED");

        yield return new WaitForSeconds(5f);

        damageMultiplierEnabled = false;
        Debug.Log("Damage boost DISABLED");
        StartCoroutine(DamageMultiplierCooldown());
    }

    public IEnumerator DamageMultiplierCooldown()
    {
        Debug.Log("Damage boost NOT READY");

        yield return new WaitForSeconds(10f);

        canActivateDamageBoost = true;
        Debug.Log("Damage boost READY");
    }
}

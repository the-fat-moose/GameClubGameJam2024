using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Basic Gun Mechanics")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    public int currentBulletCount { get; private set; } = 10;
    public int maxBulletCount { get; private set; } = 10;
    private float reloadTime = 2f;
    private bool canShoot = true; // used to not allow spam of reload
    private float damageMultiplier = 2.5f;
    public bool damageMultiplierEnabled { get; private set; } = false;
    public bool canActivateDamageBoost { get; set; } = false;
    public bool canDamageBoost { get; set; } = false;
    public float damageBoostMaxCooldown { get; private set; } = 10f;
    public float damageBoostCooldown { get; private set; } = 10f;

    [Header("Gunshot Drawing")]
    [SerializeField] Transform gunTip;
    [SerializeField] Material lineMat;
    private LineRenderer lr;

    RaycastHit hit;

    [Header("Controls")]
    [SerializeField] KeyCode reloadKey = KeyCode.R;
    [SerializeField] KeyCode damageMultiplierKey = KeyCode.E;

    [Header("Audio Parameters")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip bulletIncreaseSound;
    [SerializeField] private AudioClip abilityCooldownStart;
    [SerializeField] private AudioClip abilityCooldownEnd;
    AudioSource oneShotSource;
    OptionsManager optionsManager;

    PlayerUIManager playerUIManager;

    private void Start()
    {
        playerUIManager = GetComponentInChildren<PlayerUIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(reloadKey))
        {
            if (currentBulletCount < 10 && canShoot) { StartCoroutine(Reload()); }
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
            if (currentBulletCount > 0 && canShoot)
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

                if (gameObject.GetComponent<FirstPersonController>() != null)
                {
                    optionsManager = gameObject.GetComponent<FirstPersonController>().optionsManager;
                    oneShotSource = gameObject.GetComponent<FirstPersonController>().playerAudioSourceOneShots;
                    if (oneShotSource != null && optionsManager != null) { oneShotSource.PlayOneShot(shootSound, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }
                }

                currentBulletCount--;
                playerUIManager.GetAmmoCount(currentBulletCount, maxBulletCount);
                if (currentBulletCount <= 0 && canShoot) { StartCoroutine(Reload()); }
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
        canShoot = false;
        playerUIManager.DisplayReloading();

        yield return new WaitForSeconds(reloadTime);

        currentBulletCount = maxBulletCount;
        playerUIManager.GetAmmoCount(currentBulletCount, maxBulletCount);
        canShoot = true;
    }

    private IEnumerator DamageMultiplierDuration()
    {
        damageMultiplierEnabled = true;
        canActivateDamageBoost = false;

        oneShotSource = gameObject.GetComponent<FirstPersonController>().playerAudioSourceOneShots;
        optionsManager = gameObject.GetComponent<FirstPersonController>().optionsManager;
        if (oneShotSource != null && optionsManager != null) { oneShotSource.PlayOneShot(bulletIncreaseSound, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }

        Debug.Log("Damage boost ENABLED");

        yield return new WaitForSeconds(5f);

        damageMultiplierEnabled = false;
        Debug.Log("Damage boost DISABLED");
        StartCoroutine(DamageMultiplierCooldown());
    }

    public IEnumerator DamageMultiplierCooldown()
    {
        damageBoostCooldown = damageBoostMaxCooldown;

        if (oneShotSource != null && optionsManager != null) { oneShotSource.PlayOneShot(abilityCooldownStart, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }

        while (damageBoostCooldown > 0) 
        {
            damageBoostCooldown -= Time.deltaTime;

            yield return null;
        }

        canActivateDamageBoost = true;

        if (oneShotSource != null && optionsManager != null) { oneShotSource.PlayOneShot(abilityCooldownEnd, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }
    }
}

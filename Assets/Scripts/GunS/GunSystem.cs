using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class GunSystem : MonoBehaviour
{
    public event EventHandler OnGunShoot;
    public event EventHandler OnGunReload;
    [SerializeField] public WeaponManagerScript.AllGuns gunType;
    [SerializeField] public KeyCode weaponKey;
    [SerializeField] private int damage;
    private int baseDamage;
    [SerializeField] private float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    private float baseTimeBetweenShooting, baseReloadTime;
    private List<float> upgradableStatistics = new List<float>();
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowToHold;
    private int bulletsLeft, bulletsShot;

    private bool shooting, readyToShot, reloading;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private RaycastHit rayHit;
    [SerializeField] private LayerMask whatIsEnemy;

    [SerializeField] private GameObject muzzleFlash, bulletHoleGraphics;
    [SerializeField] private TrailRenderer bulletTrail;
    public Transform _rightHandPos, _leftHandPos;

    #region No Reload Time Consumable
    [SerializeField] private bool noReload = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        readyToShot = true;
        bulletsLeft = magazineSize;

        GameInput.Instance.OnReloadPerformed += OnReloadPerformed;
    }

    private void OnReloadPerformed(object sender, EventArgs e)
    {
        if (bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

    }

    private void MyInput()
    {
        if (GetComponentInParent<PlayerController>().cameraCanMove)
        {
            if (allowToHold)
            {
                shooting = GameInput.Instance.GetMouseLeftButtonPressed();
            }
            else
            {
                shooting = GameInput.Instance.GetMouseLeftButton();
            }


            if (readyToShot && shooting && !reloading && bulletsLeft > 0)
            {
                bulletsShot = bulletsPerTap;
                Shoot();

            }
        }

    }

    private void Reload()
    {
        reloading = true;

        OnGunReload?.Invoke(this, EventArgs.Empty);

        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        reloading = false;
        bulletsLeft = magazineSize;
    }

    private void Shoot()
    {
        WeaponUpgradeSystem weaponUpgrade = GetComponent<WeaponUpgradeSystem>();

        if (readyToShot)
        {
            OnGunShoot?.Invoke(this, EventArgs.Empty);
        }

        readyToShot = false;

        //spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);



        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        Debug.DrawRay(fpsCam.transform.position, direction * range, Color.red, 10f);

        ShootConsumableRaycast();

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            TrailRenderer trail = Instantiate(bulletTrail, shotPoint.position, Quaternion.identity);


            StartCoroutine(SpawnTrail(trail, rayHit));

            if (rayHit.collider.TryGetComponent(out IDamageable idamageable) && rayHit.collider.GetComponent<ConsumableScript>() == null)
            {
                idamageable.TakeDamage(damage);

                if (rayHit.collider.GetComponentInParent<EnemyStatusAligements>() != null && weaponUpgrade.HasAliment() != WeaponManagerScript.RoundAlimentUpgrades.normalRounds)
                {
                    EnemyStatusAligements statusAligements = rayHit.collider.GetComponentInParent<EnemyStatusAligements>();
                    statusAligements.GiveAliment(weaponUpgrade.ReturnTickAmount(), weaponUpgrade.ReturnTickDamage(), weaponUpgrade.HasAliment());
                }
            }
        }

        if (!noReload)
        {
            bulletsLeft--;
        }

        bulletsShot--;


        Invoke("ResetShoot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

    }

    private void ResetShoot()
    {
        readyToShot = true;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }

        Trail.transform.position = hit.point;

        Destroy(Trail.gameObject, Trail.time);
    }

    /*
        private Vector3 GetMousePos()
        {
            Vector3 aim;
            if (GameCameraScript.Instance.ReturnIsLookingFront())
            {
                mousePos = Input.mousePosition;
                mousePos += fpsCam.transform.right * 5000;
                aim = fpsCam.ScreenToWorldPoint(mousePos);
            }
            else
            {
                mousePos = Input.mousePosition;
                mousePos += fpsCam.transform.right * -5000;
                aim = fpsCam.ScreenToWorldPoint(mousePos);
            }


            return aim;
        }
        */

    public float ReturnMagazineSize()
    {
        return magazineSize;
    }

    public float ReturnCurrentAmmo()
    {
        return bulletsLeft;
    }

    public void SetBaseParameters()
    {
        baseDamage = damage;
        baseReloadTime = reloadTime;
        baseTimeBetweenShooting = timeBetweenShooting;
    }

    public void SetUpgradableParameters()
    {
        upgradableStatistics.Add(damage);
        upgradableStatistics.Add(timeBetweenShooting);
        upgradableStatistics.Add(reloadTime);
        upgradableStatistics.Add(magazineSize);
    }

    public void UpgradeUpgradableParameters()
    {
        damage = (int)upgradableStatistics[0];
        timeBetweenShooting = upgradableStatistics[1];
        reloadTime = upgradableStatistics[2];
        magazineSize = (int)upgradableStatistics[3];

        baseDamage = damage;
        baseReloadTime = reloadTime;
        baseTimeBetweenShooting = timeBetweenShooting;
    }

    public List<float> ReturnUpgradableParameters()
    {
        return upgradableStatistics;
    }

    public void InstaReload()
    {
        bulletsLeft = magazineSize;
    }

    #region Consumable Raycast
    private void ShootConsumableRaycast()
    {
        Vector3 direction = fpsCam.transform.forward;
        Debug.DrawRay(fpsCam.transform.position, direction * range, Color.cyan, 10f);

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            if (rayHit.collider.TryGetComponent(out ConsumableScript component))
            {
                component.TakeDamage(damage);
            }
        }
    }




    #endregion

    #region FuryTime
    public void SetNoReload(bool set)
    {
        noReload = set;
    }

    public void FuryTimeParametersUpgrade()
    {
        damage += (int)(damage * 0.5f);
        SetNoReload(true);
        timeBetweenShooting -= (timeBetweenShooting * 0.4f);
    }

    public void FuryTimeEndParameters()
    {
        damage = baseDamage;
        SetNoReload(false);
        timeBetweenShooting = baseTimeBetweenShooting;
    }
    #endregion

    #region PlayerAliment Insta Unload

    public void AlimentInstaUnload()
    {
        bulletsLeft = 0;
    }

    #endregion

    #region Animator Ready to shoot
    public void NotReadyToShoot()
    {
        readyToShot = false;
    }

    public void ReadyToShoot()
    {
        readyToShot = true;
    }

    #endregion

    public WeaponManagerScript.AllGuns ReturnGunType()
    {
        return gunType;
    }
}

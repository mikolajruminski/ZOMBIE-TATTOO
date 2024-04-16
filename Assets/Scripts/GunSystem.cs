using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunSystem : MonoBehaviour
{
    [SerializeField] public WeaponManagerScript.AllGuns gunType;
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
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
    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        readyToShot = true;
        bulletsLeft = magazineSize;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        transform.LookAt(GetMousePos());
    }

    private void MyInput()
    {
        if (allowToHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if (readyToShot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();

        }
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        reloading = false;
        bulletsLeft = magazineSize;
    }

    private void Shoot()
    {
        readyToShot = false;

        //spread
        float x = Random.Range(3 * -spread, 3 * spread);
        float y = Random.Range(-spread, spread);

        // Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        Debug.DrawRay(fpsCam.transform.position, GetMousePos() + new Vector3(x, y, 0), Color.red, 10f);


        if (Physics.Raycast(fpsCam.transform.position, GetMousePos() + new Vector3(x, y, 0), out rayHit, range))
        {
            TrailRenderer trail = Instantiate(bulletTrail, shotPoint.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, rayHit));

            if (rayHit.collider.TryGetComponent(out IDamageable idamageable))
            {
                idamageable.TakeDamage(damage);
            }
        }


        bulletsLeft--;
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

    private Vector3 GetMousePos()
    {
        mousePos = Input.mousePosition;
        mousePos += fpsCam.transform.right * 5000;
        Vector3 aim = fpsCam.ScreenToWorldPoint(mousePos);

        return aim;
    }

    public float ReturnMagazineSize()
    {
        return magazineSize;
    }

    public float ReturnCurrentAmmo()
    {
        return bulletsLeft;
    }


}

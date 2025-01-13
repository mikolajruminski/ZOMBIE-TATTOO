using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;

public class LimbFragmentationScript : MonoBehaviour, IDamageable
{
    [SerializeField] private LimbsMissingScript.Limb limb;
    [SerializeField] private int limbHealth = 4;
    private int damageToEnemyHealthDivision = 10;
    [SerializeField] private bool isDeattachable;
    [SerializeField] private bool isHead;
    [SerializeField] private GameObject armPrefab;
    [SerializeField] private GameObject limbMesh;

    private bool isAlive;
    private int limbKnockbackVelocity = 5;

    private EnemyScript enemyScript;
    private Rigidbody rb;
    private LimbsMissingScript limbsMissingScript;

    //Animations


    public void TakeDamage(int damage, RaycastHit hit)
    {

        if (isAlive)
        {
            if (isDeattachable == false && isHead == false)
            {
                Debug.Log("dealing " + damage + " damage to thorax");
                limbsMissingScript.SentLimbHitInfo(limb);

                enemyScript.TakeDamage(damage, hit);
            }
            else
            {
                limbHealth -= damage;

                limbsMissingScript.SentLimbHitInfo(limb);


                if (limbHealth > 1)
                {
                    enemyScript.TakeDamage(damage - 1, hit);

                    Debug.Log("limb health is more than 1, giving " + damage + " points of damage to the limb, leaving it with " + limbHealth + " and giving " + (damage - 1) + " damage to main body");
                }
                else
                {
                    if (isDeattachable)
                    {
                        Debug.Log("limb health is less than 1, and is detachable. Detaching limb and giving full " + damage + " points of damage to the body");
                        enemyScript.TakeDamage(damage, hit);


                        CheckForLowerLimb();


                        LoseLimbStatistics();
                    }
                    else if (isHead)
                    {
                        enemyScript.Death();
                        GetComponentInParent<LimbsMissingScript>().RevokeKinematicRigidbodies();
                        enemyScript.PlayHeadExplodeParticles();
                        SetLimbLoose();
                    }
                }
            }
        }
        /*
        else
        {
            
            if (isDeattachable)
            {
                SetLimbLoose();
            }
            else if (isHead)
            {
                Destroy(gameObject);
            }
        }
       
 */

    }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        enemyScript = GetComponentInParent<EnemyScript>();
        rb = GetComponent<Rigidbody>();
        limbsMissingScript = GetComponentInParent<LimbsMissingScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetLimbLoose()
    {
        gameObject.SetActive(false);
        Destroy(limbMesh);
        GameObject newLimb = Instantiate(armPrefab, transform.position, transform.rotation);
        AddVelocityToTheLimb(newLimb);
        gameObject.GetComponent<LimbFragmentationScript>().enabled = false;
    }

    private void AddVelocityToTheLimb(GameObject limb)
    {
        limb.GetComponent<Rigidbody>().AddForce(-transform.forward * 3, ForceMode.Impulse);
    }

    public void SwitchOnDeath()
    {
        isAlive = false;
    }

    private void CheckForLowerLimb()
    {
        if (transform.GetChild(0).gameObject.GetComponent<LimbFragmentationScript>() != null)
        {
            LimbFragmentationScript limbFragmentationScript = transform.GetChild(0).gameObject.GetComponent<LimbFragmentationScript>();

            if (limbFragmentationScript.limb == limb)
            {
                DestroyBothLimbs();
            }
        }
        else
        {
            SetLimbLoose();
        }

    }

    private void LoseLimbStatistics()
    {
        limbsMissingScript.LoseLimbStatistics(limb);
    }

    public void DestroyBothLimbs()
    {
        transform.GetChild(0).gameObject.GetComponent<LimbFragmentationScript>().SetLimbLoose();
        SetLimbLoose();
    }

}

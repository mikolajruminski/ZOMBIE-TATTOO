using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbFragmentationScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int limbHealth = 4;
    private int damageToEnemyHealthDivision = 10;
    [SerializeField] private bool isDeattachable;
    [SerializeField] private bool isHead;
    [SerializeField] private GameObject armPrefab;

    private EnemyScript enemyScript;
    private Rigidbody rb;

    public void TakeDamage(int damage)
    {
        if (isDeattachable == false && isHead == false)
        {
            Debug.Log("dealing " + damage + " damage to thorax");
            enemyScript.TakeDamage(damage);
        }
        else
        {
            limbHealth -= damage;

            if (limbHealth > 1)
            {
                enemyScript.TakeDamage(damage - 1);

                Debug.Log("limb health is more than 1, giving " + damage + " points of damage to the limb, leaving it with " + limbHealth + " and giving " + (damage - 1) + " damage to main body");
            }
            else
            {
                if (isDeattachable)
                {
                    Debug.Log("limb health is less than 1, and is detachable. Detaching limb and giving full " + damage + " points of damage to the body");
                    enemyScript.TakeDamage(damage);

                    SetLimbLoose();
                    gameObject.GetComponent<LimbFragmentationScript>().enabled = false;
                }
                else if (isHead)
                {
                    enemyScript.NoHeadDeath();
                    Debug.Log("head destroyed, insta death");
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponentInParent<EnemyScript>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetLimbLoose()
    {
        Destroy(gameObject);
        GameObject newArm = Instantiate(armPrefab, transform.position, transform.rotation);
        newArm.transform.localScale = transform.localScale;
    }
}
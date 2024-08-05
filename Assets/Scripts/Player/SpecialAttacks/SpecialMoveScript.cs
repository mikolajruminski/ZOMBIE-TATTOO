using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialMoveScript : MonoBehaviour
{
    [SerializeField] private int specialDamageAmount = 1;
    private Collider damageCollider;
    // Start is called before the first frame update

    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        PlayerUpgradeScript.Instance.onForceWaveAttackAnimationEnded += OnForceWaveAttackAnimationEnded;
        //GameArmsAnimatorScript.Instance.OnSpecialAttackAnimationEnded += OnSpecialAttackAnimationEnded;
        damageCollider.enabled = false;
    }


    private void OnForceWaveAttackAnimationEnded(object sender, EventArgs e)
    {
        StartCoroutine(EnableAttackColldier());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator EnableAttackColldier()
    {
        damageCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        damageCollider.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyScript enemyScript))
        {
            enemyScript.TakeDamage(specialDamageAmount);

            Vector3 direction = enemyScript.gameObject.transform.position - GetComponentInParent<PlayerController>().gameObject.transform.position;

            direction = direction.normalized;
            enemyScript.gameObject.GetComponent<Rigidbody>().AddForce(direction * 50, ForceMode.Impulse);
        }
    }
}

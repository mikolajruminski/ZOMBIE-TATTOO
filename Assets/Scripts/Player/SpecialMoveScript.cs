using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialMoveScript : MonoBehaviour
{
    [SerializeField] private int specialDamageAmount = 10;
    private Collider damageCollider;
    // Start is called before the first frame update

    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        damageCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void SpecialAttack()
    {
        StartCoroutine(EnableAttackColldier());
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
            enemyScript.TakeDamage(0);

            Vector3 direction = enemyScript.gameObject.transform.position - GetComponentInParent<PlayerController>().gameObject.transform.position;

            direction = direction.normalized;
            enemyScript.gameObject.GetComponent<Rigidbody>().AddForce(direction * 50, ForceMode.Impulse);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RngdBulletScript : MonoBehaviour, IDamageable
{
    private Rigidbody rb;
    Vector3 direction;
    Vector3 offset = new Vector3(0, 1, 0);
    [SerializeField] private int _damage = 2;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        direction = ((PlayerController.Instance.transform.position + offset) - transform.position).normalized;
        AddForce();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddForce()
    {
        rb.AddForce(direction * 10, ForceMode.Impulse);
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FotelHealthScript fotelHealthScript))
        {
            fotelHealthScript.TakeDamage(_damage);
            Debug.Log("dealt " + _damage + "of ranged damage");
            Destroy(gameObject);
        }
    }
}

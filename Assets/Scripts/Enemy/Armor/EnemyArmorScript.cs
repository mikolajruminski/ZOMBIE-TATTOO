using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyArmorScript : MonoBehaviour
{
    [SerializeField] bool isHeavyArmor;

    // Start is called before the first frame update
    private void Awake()
    {
        SetArmorParameters();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetArmorParameters()
    {
        if (isHeavyArmor)
        {
            EnemyArmorPlateScript[] plates = GetComponentsInChildren<EnemyArmorPlateScript>();

            foreach (EnemyArmorPlateScript plate in plates)
            {
                plate.SetHeavyArmor();
            }

            EnemyScript enemyScript = GetComponentInParent<EnemyScript>();
            enemyScript.SetArmorSpeed();
        }
    }
}

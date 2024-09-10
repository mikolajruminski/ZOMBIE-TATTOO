using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyArmorScript : MonoBehaviour
{
    [SerializeField] private Armor armorType;

    [SerializeField] private GameObject thoraxObject;

    [SerializeField] private int headHeavyArmorPoints = 15;
    [SerializeField] private int chestHeavyArmorPoints = 30;
    [SerializeField] private int armsHeavyArmorPoints = 10;
    [SerializeField] private int legsHeavyArmorPoints = 10;

    [SerializeField] private int headLightArmorPoints = 8;
    [SerializeField] private int chestLighArmorPoints = 15;
    [SerializeField] private int armsLighArmorPoints = 5;
    [SerializeField] private int legsLighArmorPoints = 5;

    [SerializeField] private int lightArmorSpeed = 2;
    [SerializeField] private int heavyArmorSpeed = 1;


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
        switch (armorType)
        {
            case Armor.None:


                break;

            case Armor.LightArmor:
                SetLightArmor();

                break;

            case Armor.HeavyArmor:
                SetHeavyArmor();

                break;

        }
    }

    public enum Armor
    {
        None, LightArmor, HeavyArmor,
    }

    private void SetLightArmor()
    {
        EnemyArmorPlateScript[] plates = thoraxObject.GetComponentsInChildren<EnemyArmorPlateScript>();

        foreach (EnemyArmorPlateScript plate in plates)
        {
            switch (plate.GetArmorType())
            {
                case EnemyArmorPlateScript.ArmorType.Head:
                    plate.SetArmor(headLightArmorPoints);
                    break;
                case EnemyArmorPlateScript.ArmorType.Leg:
                    plate.SetArmor(legsLighArmorPoints);
                    break;

                case EnemyArmorPlateScript.ArmorType.Arm:
                    plate.SetArmor(armsLighArmorPoints);
                    break;

                case EnemyArmorPlateScript.ArmorType.Chest:
                    plate.SetArmor(chestLighArmorPoints);
                    break;
            }
        }

        EnemyScript enemyScript = GetComponentInParent<EnemyScript>();
        enemyScript.SetArmorSpeed(lightArmorSpeed);
    }

    private void SetHeavyArmor()
    {
        EnemyArmorPlateScript[] plates = thoraxObject.GetComponentsInChildren<EnemyArmorPlateScript>();

        foreach (EnemyArmorPlateScript plate in plates)
        {
            switch (plate.GetArmorType())
            {
                case EnemyArmorPlateScript.ArmorType.Head:
                    plate.SetArmor(headHeavyArmorPoints);
                    break;
                case EnemyArmorPlateScript.ArmorType.Leg:
                    plate.SetArmor(legsHeavyArmorPoints);
                    break;

                case EnemyArmorPlateScript.ArmorType.Arm:
                    plate.SetArmor(armsHeavyArmorPoints);
                    break;

                case EnemyArmorPlateScript.ArmorType.Chest:
                    plate.SetArmor(chestHeavyArmorPoints);
                    break;
            }
        }

        EnemyScript enemyScript = GetComponentInParent<EnemyScript>();
        enemyScript.SetArmorSpeed(heavyArmorSpeed);
    }

}

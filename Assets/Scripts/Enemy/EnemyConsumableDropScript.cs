using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConsumableDropScript : MonoBehaviour
{
    [SerializeField] private List<ConsumableScript> consumables = new List<ConsumableScript>();
    [SerializeField] private List<ConsumableScript> specialConsumables = new List<ConsumableScript>();
    [SerializeField] private float chanceToDropConsumable = .15f;
    [SerializeField] private float chanceToDropSpecialConsumable = .05f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropConsumable()
    {
        float randValue = Random.value;

        if (randValue < chanceToDropSpecialConsumable)
        {
            Debug.Log("dropped special consumable with: " + randValue + " chance");
            int x = Random.Range(0, specialConsumables.Count);
            Instantiate(specialConsumables[x], transform.position, Quaternion.identity);
        }
        else if (randValue < chanceToDropConsumable)
        {
            Debug.Log("dropped consumable with: " + randValue + " chance");
            int x = Random.Range(0, consumables.Count);
            Instantiate(consumables[x], transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("dropped nothing with chance of: " + randValue);
        }

    }
}

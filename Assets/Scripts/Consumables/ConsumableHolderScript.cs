using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ConsumableHolderScript : MonoBehaviour
{
    public static ConsumableHolderScript Instance { get; private set; }
    [SerializeField] private GameObject ammoConPrefab;
    [SerializeField] private GameObject healthConPrefab;

    public event EventHandler OnConsumableShot;

    private GameObject currentlySpawnedConsumable;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        GameArmsAnimatorScript.Instance.OnConsumableAnimationEnded += OnConsumableAnimationEnded;
    }

    private void OnConsumableAnimationEnded(object sender, EventArgs e)
    {
        UseCurrentlySpawnedConsumable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConsumableShot(GameObject consumable)
    {
        if (consumable.GetComponent<ConAmmoPack>() != null)
        {
            SpawnConsuamble(ammoConPrefab);
            OnConsumableShot?.Invoke(this, EventArgs.Empty);
        }
        else if (consumable.GetComponent<ConHealthPack>() != null)
        {
            SpawnConsuamble(healthConPrefab);
            OnConsumableShot?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SpawnConsuamble(GameObject consuamble)
    {
        currentlySpawnedConsumable = Instantiate(consuamble, gameObject.transform);
    }

    public void DeleteConsumable()
    {
        Destroy(currentlySpawnedConsumable);
    }

    public void UseCurrentlySpawnedConsumable()
    {
        currentlySpawnedConsumable.gameObject.GetComponent<ConsumableScript>().OnUse();
    }
}

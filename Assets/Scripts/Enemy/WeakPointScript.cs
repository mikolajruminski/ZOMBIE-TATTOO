using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeakPointScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int _baseWeakPointHealth = 3;
    private int _weakPointHealth;
    public void TakeDamage(int damage)
    {
        EnemyScript enemyScript = GetComponentInParent<EnemyScript>();
        enemyScript.TakeDamage(damage);

        _weakPointHealth--;
        WeakPointColorChange();

        if (_weakPointHealth < 1)
        {
            NewWeakPoint();
            _weakPointHealth = _baseWeakPointHealth;
            WeakPointColorChange();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _weakPointHealth = _baseWeakPointHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NewWeakPoint()
    {
        BossEnemyScript bossEnemyScript = GetComponentInParent<BossEnemyScript>();
        bossEnemyScript.NewWeakPoint();
    }

    private void WeakPointColorChange()
    {
        UnityEngine.UI.Image image = GetComponentInChildren<UnityEngine.UI.Image>();

        switch (_weakPointHealth)
        {
            case 3:
                image.color = Color.green;
                break;
            case 2:
                image.color = Color.yellow;
                break;
            case 1:
                image.color = Color.red;
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbsMissingScript : MonoBehaviour
{
    private List<Limb> missingLimbs = new List<Limb>();
    private EnemyScript enemyScript;
    private MeeleEnemyScript meeleEnemyScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponentInParent<EnemyScript>();
        meeleEnemyScript = GetComponentInParent<MeeleEnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoseLimbStatistics(Limb limb)
    {
        switch (limb)
        {
            case Limb.Arm:
                if (missingLimbs.Contains(limb))
                {
                    Debug.Log("already lost an arm, not lowering the damage");
                }
                else
                {
                    meeleEnemyScript.SetLimbedDamage();
                    missingLimbs.Add(limb);
                }

                break;

            case Limb.Leg:

                if (missingLimbs.Contains(limb))
                {
                    Debug.Log("already lost a leg, not lowering the speed");
                }
                else
                {
                    meeleEnemyScript.SetLostLegSpeed();
                    missingLimbs.Add(limb);
                }
                break;

            case Limb.Else:

                break;
        }
    }

    public bool isLegMissing()
    {
        return missingLimbs.Contains(Limb.Leg);
    }

    public enum Limb
    {
        Leg, Arm, Else
    }
}

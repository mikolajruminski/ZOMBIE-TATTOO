using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbsMissingScript : MonoBehaviour
{
    private List<Limb> missingLimbs = new List<Limb>();
    private EnemyScript enemyScript;
    private MeeleEnemyScript meeleEnemyScript;

    public EventHandler<OnShotEventArgs> OnShot;

    public class OnShotEventArgs : EventArgs
    {
        public Limb eventLimb;
    }
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
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
            case Limb.Larm:
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
            case Limb.Rarm:
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

            case Limb.Lleg:

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

            case Limb.Rleg:

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
        if (missingLimbs.Contains(Limb.Lleg) || missingLimbs.Contains(Limb.Rleg))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RevokeKinematicRigidbodies()
    {
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.isKinematic = false;
        }
    }

    public void SentLimbHitInfo(Limb limb)
    {
        OnShot?.Invoke(this, new OnShotEventArgs
        {
            eventLimb = limb
        });
    }

    public enum Limb
    {
        Lleg, Rleg, Larm, Rarm, Else
    }
}

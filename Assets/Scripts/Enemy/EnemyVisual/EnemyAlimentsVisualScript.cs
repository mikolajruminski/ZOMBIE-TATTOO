using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlimentsVisualScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] alimentsEmitter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void emitParticles(int particleNumber)
    {
        alimentsEmitter[particleNumber].Play();
    }

    public void StopEmittingParticles()
    {
        for (int i = 0; i < alimentsEmitter.Length; i++)
        {
            alimentsEmitter[i].Stop();
        }
    }

    public ParticleSystem[] ReturnAlimentsEmitter()
    {
        return alimentsEmitter;
    }
}

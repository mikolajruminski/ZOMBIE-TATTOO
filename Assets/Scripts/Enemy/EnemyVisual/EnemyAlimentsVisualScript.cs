using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlimentsVisualScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem alimentEmitter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void emitParticles()
    {
        alimentEmitter.Play();
    }

    public void StopEmittingParticles() 
    {
        alimentEmitter.Stop();
    }
}

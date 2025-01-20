using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    Stack<ParticleSystem> particles = new Stack<ParticleSystem>();
    [SerializeField]
    ParticleSystem managedParticles;
    public void InstantiateParticle(Vector3 position)
    {
        Transform particlePos = particles.Count == 0 ? Instantiate(managedParticles).transform : particles.Pop().transform;
        particlePos.position = position;
    }
    public void ReturnParticle(ParticleSystem particle)
    {
        particles.Push(particle);
    }
}

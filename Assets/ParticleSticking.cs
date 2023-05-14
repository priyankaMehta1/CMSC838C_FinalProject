using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSticking : MonoBehaviour
{
    public float stickDistance = 0.1f; // Distance threshold for particles to stick together
    public float stickForce = 1f; // Force applied to particles to bring them together

    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        int particleCount = particleSystem.particleCount;

        if (particles == null || particles.Length < particleCount)
        {
            particles = new ParticleSystem.Particle[particleCount];
        }

        particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            ParticleSystem.Particle currentParticle = particles[i];

            for (int j = i + 1; j < particleCount; j++)
            {
                ParticleSystem.Particle otherParticle = particles[j];

                float distance = Vector3.Distance(currentParticle.position, otherParticle.position);

                if (distance < stickDistance)
                {
                    // Apply force to bring particles closer together
                    Vector3 forceDirection = otherParticle.position - currentParticle.position;
                    Vector3 force = forceDirection.normalized * stickForce;

                    currentParticle.velocity += force * Time.deltaTime;
                    otherParticle.velocity -= force * Time.deltaTime;
                }
            }

            particles[i] = currentParticle;
        }

        particleSystem.SetParticles(particles, particleCount);
    }
}
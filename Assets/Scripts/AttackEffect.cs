using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public ParticleEffect particleEffect;  // 파티클 효과 설정
    ParticleSystem projectileParticle;
    private void Awake()
    {
        projectileParticle = GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>();
    }
    public void ApplyAttackEffect(Vector2 position)
    {
        gameObject.transform.position = position;
        // projectileParticle이 null이 아닌지 확인
        if (projectileParticle == null)
        {
            Debug.LogError("Projectile Particle is not assigned.");
            return;
        }

        // EmissionModule을 설정하여 방출량을 지정합니다.
        ParticleSystem.EmissionModule em = projectileParticle.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(particleEffect.particleSize * particleEffect.sizeMultiplier)));

        // MainModule을 설정하여 파티클 속도를 조절합니다.
        ParticleSystem.MainModule mm = projectileParticle.main;
        mm.startSpeedMultiplier = particleEffect.particleSize * particleEffect.speedMultiplier;

        // 파티클을 재생합니다.
        projectileParticle.Play();
    }
} 
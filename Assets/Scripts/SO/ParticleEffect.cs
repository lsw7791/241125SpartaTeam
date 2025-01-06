using UnityEngine;

[CreateAssetMenu(fileName = "ParticleEffect", menuName = "ScriptableObjects/ParticleEffect")]
public class ParticleEffect : ScriptableObject
{
    public ParticleSystem particleSystemPrefab;
    public float speedMultiplier = 10f;  // 속도 배율
    public float sizeMultiplier = 5f;    // 크기 배율
    public float particleSize = 5; //파티클 크기
}
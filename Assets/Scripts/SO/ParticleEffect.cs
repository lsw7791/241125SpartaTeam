using UnityEngine;

[CreateAssetMenu(fileName = "ParticleEffect", menuName = "ScriptableObjects/ParticleEffect")]
public class ParticleEffect : ScriptableObject
{
    public ParticleSystem particleSystemPrefab;
    public float speedMultiplier = 10f;  // �ӵ� ����
    public float sizeMultiplier = 5f;    // ũ�� ����
    public float particleSize = 5; //��ƼŬ ũ��
}
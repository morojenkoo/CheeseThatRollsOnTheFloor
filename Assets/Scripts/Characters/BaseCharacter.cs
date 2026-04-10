using UnityEngine;

namespace Characters
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField] public float hp { get; set; } = 100f;
        [SerializeField] public float speedAcceleration { get; set; } = 1f;
    }
}
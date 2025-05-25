using UnityEngine;

namespace Moving
{
    public abstract class CharacterLookDirectionController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon; 
        public Vector3 LookDirection { get; set; }
        private readonly float _rotationSpeed = 10f;
        private void Update()
        {
            if (LookDirection != Vector3.zero)
                Rotate();
        }
        private void Rotate()
        {
            float sqrMagnitude = (transform.rotation * Vector3.forward - LookDirection).sqrMagnitude;
            if (sqrMagnitude > SqrEpsilon)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookDirection, Vector3.up), 
                    _rotationSpeed * Time.deltaTime);
            }
        }
    }
}
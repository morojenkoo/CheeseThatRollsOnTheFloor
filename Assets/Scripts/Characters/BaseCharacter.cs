using Moving;
using UnityEngine;
namespace Characters
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        protected CharacterMovementController _characterMovementController;
        protected void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
            
        }
    }
}
using Characters;
using UnityEngine;

namespace Environment.Obstacles
{
    public class Dust : Obstacle
    {
        [SerializeField] private float slowMultiplier = 0.5f;

        protected override void affect(BaseCharacter bc)
        {
            bc.speedAcceleration = slowMultiplier;
        }
        private void OnTriggerEnter(Collider other)
        {
            var bc = other.GetComponent<BaseCharacter>();
            affect(bc);
        }

        private void OnTriggerExit(Collider other)
        {
            var bc = other.GetComponent<BaseCharacter>();
            ResetEffect(bc);
        }

        private void ResetEffect(BaseCharacter bc)
        {
            bc.speedAcceleration = 1f;
        }

    }
}
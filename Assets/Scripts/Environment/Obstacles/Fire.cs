using System;
using Characters;
using UnityEngine;

namespace Environment.Obstacles
{
    public class Fire : Obstacle
    {
        [SerializeField] private float damage;
        private float time;

        private void Update()
        {
            time += Time.deltaTime;
        }
        protected override void affect(BaseCharacter bc)
        {
            bc.hp -= damage * time;
        }

        private void OnTriggerEnter(Collider other)
        {
            var bc = other.GetComponent<BaseCharacter>();
            if (1.0f - time < 0.000000001f) affect(bc);
        }

        private void OnTriggerExit(Collider other)
        {
            var bc = other.GetComponent<BaseCharacter>();
            if (1.0f - time < 0.000000001f) affect(bc);
        }
    }
}
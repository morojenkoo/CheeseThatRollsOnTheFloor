using Characters;
using UnityEngine;

namespace Environment.Obstacles
{
    public abstract class Obstacle : MonoBehaviour
    {
        protected abstract void affect(BaseCharacter bc);
    }
}
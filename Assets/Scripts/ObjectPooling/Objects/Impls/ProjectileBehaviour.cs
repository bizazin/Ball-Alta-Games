using UnityEngine;

namespace Scripts.ObjectPooling.Objects.Impls
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;

        public void AddForce(Vector3 direction) => 
            rigidbody.AddForce(direction, ForceMode.Impulse);
    }
}
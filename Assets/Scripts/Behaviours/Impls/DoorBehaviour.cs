using DG.Tweening;
using UnityEngine;

namespace Scripts.Behaviours.Impls
{
    public class DoorBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform doorHingeTransform;
        public void Open(float openDuration, float openAngleDeg) => doorHingeTransform.DORotate(new Vector3(0, openAngleDeg, 0), openDuration).SetEase(Ease.InOutQuad);
    }
}
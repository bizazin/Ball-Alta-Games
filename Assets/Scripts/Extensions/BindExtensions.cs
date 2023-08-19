using Scripts.Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Scripts.Extensions
{
    public static class BindExtensions
    {
        public static void BindView<T, TU>(this DiContainer container, Object viewPrefab, Transform parent)
            where TU : IView
            where T : IController
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle();
            container.BindInterfacesAndSelfTo<TU>()
                .FromComponentInNewPrefab(viewPrefab)
                .UnderTransform(parent).AsSingle();
        }

    }
}
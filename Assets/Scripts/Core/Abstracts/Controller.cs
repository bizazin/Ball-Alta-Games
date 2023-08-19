using Scripts.Core.Interfaces;
using Zenject;

namespace Scripts.Core.Abstracts
{
    public abstract class Controller<T> : IController where T : IView
    {

        [Inject] protected readonly T View;

    }
}
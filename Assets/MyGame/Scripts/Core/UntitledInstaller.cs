using UnityEngine;
using Zenject;

namespace MyGame.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Draggable>().FromComponentInHierarchy().AsSingle();
        }
    }
}
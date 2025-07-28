using MyGame.Scripts.Core;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ShapeSettings shapeSettings;
    [SerializeField] private Transform[] spawnPoints;

    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShapeSettings>().FromInstance(shapeSettings).AsSingle();
        
        Container.Bind<IShapeFactory>().To<ShapeFactory>().AsSingle()
            .WithArguments(shapeSettings, spawnPoints);
        
        Container.Bind<ShapeSpawner>().FromComponentInHierarchy().AsSingle();
    
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
    }
}
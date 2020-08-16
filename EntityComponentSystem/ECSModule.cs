using Autofac;
using EntityComponentSystem.Components;
using EntityComponentSystem.Entities;
using EntityComponentSystem.Systems;
using System.Diagnostics.CodeAnalysis;

namespace EntityComponentSystem
{
  [ExcludeFromCodeCoverage]
  public class ECSModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<Context>()
             .As<IContext>()
             .SingleInstance();
      builder.RegisterType<EntityManager>()
             .As<IEntityManager>()
             .SingleInstance();
      builder.RegisterType<EntityElementFactory>()
             .As<IElementFactory<Entity>>()
             .SingleInstance();
      builder.RegisterType<ComponentElementFactory>()
             .As<IComponentElementFactory>()
             .SingleInstance();
      builder.RegisterType<Executor>()
             .As<IExecutor>()
             .SingleInstance();
    }
  }
}

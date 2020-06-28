using Autofac;
using EntityComponentSystem.Caching;
using EntityComponentSystem.Storages;
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
      builder.RegisterType<CacheManager>()
             .As<ICacheManager>()
             .SingleInstance();
      builder.RegisterType<StorageManager>()
             .As<IStorageManager>()
             .SingleInstance();
      builder.RegisterType<StorageFactory>()
             .As<IStorageFactory>()
             .SingleInstance();
    }
  }
}

using System.Reflection;
using Ardalis.SharedKernel;
using Autofac;
using core.ProductAggregate;
using infrastrucrue.Database;
using MediatR;
using MediatR.Pipeline;
using usecases.Product;

namespace infrastrucrue;

public class InfrastructureModule() : Autofac.Module
{
    private readonly List<Assembly> _assemblies = new();

    protected override void Load(ContainerBuilder builder)
    {
        LoadAssemblies();
        RegisterEf(builder);
        RegisterMediatR(builder);
    }

    private void LoadAssemblies()
    {
        var coreAssembly = Assembly.GetAssembly(typeof(ProductEntity));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(InfrastructureModule));
        var useCasesAssembly = Assembly.GetAssembly(typeof(CreateProductCommand));
        
        AddToAssembliesIfNotNull(coreAssembly);
        AddToAssembliesIfNotNull(infrastructureAssembly);
        AddToAssembliesIfNotNull(useCasesAssembly);
    }

    private void AddToAssembliesIfNotNull(Assembly? assembly)
    {
        if (assembly != null)
            _assemblies.Add(assembly);
    }

    private static void RegisterUploader(ContainerBuilder builder)
    {
        
    }

    private static void RegisterEf(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
            .As(typeof(IRepository<>))
            .As(typeof(IReadRepository<>))
            .InstancePerLifetimeScope();
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        builder
            .RegisterGeneric(typeof(LoggingBehavior<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerLifetimeScope();

        builder
            .RegisterType<MediatRDomainEventDispatcher>()
            .As<IDomainEventDispatcher>()
            .InstancePerLifetimeScope();

        var mediatrOpenTypes = new[]
        {
            typeof(IRequestHandler<,>),
            typeof(IRequestExceptionHandler<,,>),
            typeof(IRequestExceptionAction<,>),
            typeof(INotificationHandler<>),
        };

        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
                .RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
        }
    }
}

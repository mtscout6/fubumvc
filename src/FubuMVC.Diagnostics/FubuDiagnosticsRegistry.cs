using FubuMVC.Core;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Core.Configuration;
using FubuMVC.Diagnostics.Core.Configuration.Policies;
using FubuMVC.Diagnostics.Core.Grids;
using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Core.Infrastructure;
using FubuMVC.Diagnostics.Features;
using FubuMVC.Diagnostics.Features.Html.Preview;
using FubuMVC.Diagnostics.Features.Html.Preview.Decorators;
using FubuMVC.Diagnostics.Features.Requests;
using FubuMVC.Diagnostics.Models;
using FubuMVC.Diagnostics.Navigation;
using FubuMVC.Diagnostics.Notifications;
using FubuMVC.Diagnostics.Partials;
using FubuMVC.Spark;

namespace FubuMVC.Diagnostics
{
    public class FubuDiagnosticsRegistry : FubuPackageRegistry
    {
        public FubuDiagnosticsRegistry()
        {
            setupDiagnosticServices();

            Applies
                .ToAssemblyContainingType<FubuDiagnosticsRegistry>()
                .ToAssemblyContainingType<BehaviorStart>()
                .ToAllPackageAssemblies();

            ApplyHandlerConventions(markers => new DiagnosticsHandlerUrlPolicy(markers), typeof(DiagnosticsFeatures));

            Actions
                .FindWith<PartialActionSource>();

            Routes
                .UrlPolicy<DiagnosticsAttributeUrlPolicy>();

            Views
                .TryToAttachWithDefaultConventions()
                .TryToAttachViewsInPackages()
                .RegisterActionLessViews(token => token.ViewModelType.IsDiagnosticsReport())
                .RegisterActionLessViews(token => typeof (IPartialModel).IsAssignableFrom(token.ViewModelType));

            this.UseSpark();

            Output
                .ToJson
                .WhenCallMatches(call => call.OutputType().Name.ToLower().Contains("json"));
        }

        private void setupDiagnosticServices()
        {
            Services(x =>
                         {
                             // Typically you'd do this in your container but we're keeping this IoC-agnostic
                             x.SetServiceIfNone<IHttpRequest, HttpRequest>();
                             x.SetServiceIfNone<IHttpConstraintResolver, HttpConstraintResolver>();
                             x.SetServiceIfNone<IRequestCacheModelBuilder, RequestCacheModelBuilder>();
                             x.SetServiceIfNone<INavigationMenuBuilder, NavigationMenuBuilder>();
                             x.SetServiceIfNone<IAuthorizationDescriptor, AuthorizationDescriptor>();
                             x.SetServiceIfNone(typeof (IGridService<,>), typeof (GridService<,>));
                             x.SetServiceIfNone(typeof (IGridRowBuilder<,>), typeof (GridRowBuilder<,>));
                             x.SetServiceIfNone(typeof (IGridColumnBuilder<>), typeof (GridColumnBuilder<>));
                             x.SetServiceIfNone
                                 <IGridRowProvider<BehaviorGraph, BehaviorChain>, BehaviorGraphRowProvider>();
                             x.SetServiceIfNone
                                 <IGridRowProvider<RequestCacheModel, RecordedRequestModel>, RequestCacheRowProvider>();
                             x.SetServiceIfNone<IHtmlConventionsPreviewContextFactory, HtmlConventionsPreviewContextFactory>();
                             x.SetServiceIfNone<IPreviewModelActivator, PreviewModelActivator>();
                             x.SetServiceIfNone<IPreviewModelTypeResolver, PreviewModelTypeResolver>();
                             x.SetServiceIfNone<IPropertySourceGenerator, PropertySourceGenerator>();
                             x.SetServiceIfNone<IModelPopulator, ModelPopulator>();
                             x.SetServiceIfNone<ITagGeneratorFactory, TagGeneratorFactory>();

                             x.Scan(scan =>
                                        {
                                            scan
                                                .Applies
                                                .ToThisAssembly()
                                                .ToAllPackageAssemblies();

                                            scan
                                                .AddAllTypesOf<INavigationItemAction>()
                                                .AddAllTypesOf<INotificationPolicy>()
                                                .AddAllTypesOf<IPreviewModelDecorator>();

                                            scan
                                                .ConnectImplementationsToTypesClosing(typeof (IPartialDecorator<>))
                                                .ConnectImplementationsToTypesClosing(typeof (IGridColumnBuilder<>))
                                                .ConnectImplementationsToTypesClosing(typeof (IGridColumn<>))
                                                .ConnectImplementationsToTypesClosing(typeof (IGridFilter<>))
                                                .ConnectImplementationsToTypesClosing(typeof(IModelBuilder<>));
                                        });
                         });
        }
    }
}
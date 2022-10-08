global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using System.Threading;

namespace nArchitectureExtension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.nArchitectureExtensionString)]
    public sealed class nArchitectureExtensionPackage : AsyncPackage
    {

        public static DTE2 _dte;
        public static IServiceCollection Services;
        protected async override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            _dte = await GetServiceAsync(typeof(DTE)) as DTE2;
            Assumes.Present(_dte);

            Services = new ServiceCollection();
            Services.AddServices();

            await this.RegisterCommandsAsync();
        }
    }
}
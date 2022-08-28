using CalcWpfApp.Contract;
using CalcWpfApp.Services;
using CalcWpfApp.ViewModels;
//using Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace CalcWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;
        public static IHost _Host;
        public static IHost Host => _Host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();//создаем хост
        protected override async void OnStartup(StartupEventArgs e) //запуск хоста при старте приложения
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(false);
        }
        protected override async void OnExit(ExitEventArgs e) //остановка хоста
        {
            base.OnExit(e);
            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _Host = null;
        }
        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>(); //регистрируем вьюмодель как сервис
            services.AddSingleton<ExtensionImport>();
        }
        public static string CurrentDirectory => IsDesignMode ? Path.GetDirectoryName(GetSourcesCodePath()) : Environment.CurrentDirectory;
        private static string GetSourcesCodePath([CallerFilePath] string Path = null) => Path;

    }
}

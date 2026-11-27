using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuanLy.Data;
using QuanLy.Services;
using QuanLy.ViewModels;
using QuanLy.Views;
using QuanLy.Views.Pages;
using Serilog;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
namespace QuanLy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider? _services;
        public static ILoggerFactory Loggers { get; } =
    LoggerFactory.Create(builder =>
    {
        builder.AddDebug();
        builder.SetMinimumLevel(LogLevel.Debug);
    });
        protected override void OnExit(ExitEventArgs e)
        {
            Loggers.Dispose(); // flush providers
            base.OnExit(e);
            _services?.Dispose();
            Log.CloseAndFlush(); // ghi hết buffer
            base.OnExit(e);

        }


        protected override void OnStartup(StartupEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Migrate();
              
            }
            var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");

            Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(
    Path.Combine(logDir, "app-.log"),            // → app-20261120.log
    rollingInterval: RollingInterval.Day,       // một file mỗi ngày
    retainedFileCountLimit: 31,                 // mặc định: giữ 31 file
    fileSizeLimitBytes: 10 * 1024 * 1024,        // mặc định: 1 GB
    rollOnFileSizeLimit: true,                // đầy → app-20261120_001.log
    outputTemplate:
    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] " +
    "{SourceContext}: {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

            base.OnStartup(e);
            var services = new ServiceCollection();
            services.AddLogging(b =>
                b.AddSerilog(dispose: true));


            RegisteredGlobalExceptionHandler();
            services.AddSingleton<IWindowService, WindowService>();

            services.AddSingleton<Authentication>();
            services.AddSingleton<AppointmentService>();
            //services.AddTransient<ValidatableViewModel>();
            services.AddTransient<CustomerDashboardViewModel>();
            services.AddTransient<AppointmentDetailViewModel>();
            services.AddTransient<AppopintmentDetailWindow>();
            services.AddTransient<MainMenuViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginWindow>();
            services.AddTransient<MainMenu>();
            services.AddTransient<AppointmentPage>();
            services.AddTransient<DashboardPage>();
            services.AddTransient<PetPage>();
            _services = services.BuildServiceProvider();
            _services.GetRequiredService<ILogger<App>>()
            .LogInformation("Application started.");

            var loginWindow =
        _services.GetRequiredService<LoginWindow>();

            MainWindow = loginWindow;
            loginWindow.Show();




        }
        private void RegisteredGlobalExceptionHandler()
        {
            var log = Loggers.CreateLogger("GlobalErrors");
            DispatcherUnhandledException += (s, a) =>
            {
                log.LogError(a.Exception, "Unhandled UI error.");
                a.Handled = true; // chỉ khi phục hồi được
            };
            AppDomain.CurrentDomain.UnhandledException += (s, a) =>
            log.LogCritical(a.ExceptionObject as Exception,
            "Fatal unhandled exception.");
            TaskScheduler.UnobservedTaskException += (s, a) =>
            {
                log.LogError(a.Exception, "Unobserved task error.");
                a.SetObserved();
            };
        }

    }

}

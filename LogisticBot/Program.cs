using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogisticBot.DataAccess.DBContext;
using System;
using System.IO;
using LogisticBot.DataAccess.Repositories.IRepository;
using LogisticBot.DataAccess.Repositories.Repository;
using LogisticBot;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Polling;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using MediatR;

class Program
{
    public static IConfiguration Configuration { get; set; }

    //public static TelegramBotClient client = new TelegramBotClient("7436488266:AAFymsI2RwTk6RpGw0fow1K-QA7jmkhu7yo");
    static async Task Main(string[] args)
    {
       var connectionString = "Host=localhost;Database=your_database;Username=your_username;Password=your_password";
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection, connectionString);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<AppDbContext>();

        // IMediator va UserMediatRService DI konteyneridan olish
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var userMediatRService = serviceProvider.GetRequiredService<UserMediatRService>();
        var cargoMediatRService=serviceProvider.GetService<CargoMediatRService>();



        var botToken = "7436488266:AAFymsI2RwTk6RpGw0fow1K-QA7jmkhu7yo";
        var botClient = new TelegramBotClient(botToken);
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // Receive all update types
        };

        var cts = new CancellationTokenSource();

        Console.WriteLine("Starting bot...");

        var updateHandler = new AdvancedBotUpdateHandler(userMediatRService,cargoMediatRService);

            botClient.StartReceiving(
            updateHandler: updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        Console.WriteLine("Bot is running. Press any key to exit.");
        Console.ReadKey();
        cts.Cancel();








        
    }

    private static Task Client_OnMessage(Telegram.Bot.Types.Message message, Telegram.Bot.Types.Enums.UpdateType type)
    {
        throw new NotImplementedException();
    }

    private static void ConfigureServices(IServiceCollection services,string connectionString)
    {
        connectionString = "host=localhost;port=5432;database=logisticBotdb;user id=postgres;password=1234;";
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICargoRepository, CargoRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LogisticBot.Services.MediatR.Commands.UserCommad.UserCreateCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LogisticBot.Services.MediatR.Commands.UserCommad.UserGetAllQuery).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LogisticBot.Services.MediatR.Commands.UserCommad.UserGetByIdQuery).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LogisticBot.Services.MediatR.Commands.CargoCommand.CargoCreateCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LogisticBot.Services.MediatR.Commands.CargoCommand.CargoGetAllQuery).Assembly));


        services.AddScoped<UserMediatRService>();
        services.AddScoped<CargoMediatRService>();

        services.AddScoped<AdvancedBotUpdateHandler>();
    }
}

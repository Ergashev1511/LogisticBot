using LogisticBot.Services.DTOs;
using LogisticBot.Services.MediatR.Commands.CargoCommand;
using LogisticBot.Services.MediatR.Commands.UserCommad;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LogisticBot
{
    public class AdvancedBotUpdateHandler : IUpdateHandler
    {

        private static Dictionary<long, string> userState = new Dictionary<long, string>(); 
        private static Dictionary<long, string> userFirstName = new Dictionary<long, string>();
        private static Dictionary<long, string> userLastName = new Dictionary<long, string>(); 
        private static Dictionary<long, string> PhoneNumber = new Dictionary<long, string>(); 
        private static Dictionary<long, string> TelegramUserName = new Dictionary<long, string>();
        private static Dictionary<long, string> CargoType = new Dictionary<long, string>();
        private static Dictionary<long, string> CargoWeight = new Dictionary<long, string>();
        private static Dictionary<long, string> CargoVolume = new Dictionary<long, string>();
        private static Dictionary<long, string> StartLocation = new Dictionary<long, string>();
        private static Dictionary<long, string> DestinationLocation = new Dictionary<long, string>();
        private static Dictionary<long, string> NumberOfTruck = new Dictionary<long, string>();
        private static Dictionary<long, string> TypeOfTruck = new Dictionary<long, string>();
        private static Dictionary<long, string> Price = new Dictionary<long, string>();
        private static Dictionary<long, string> AvailableForm = new Dictionary<long, string>();
        private static Dictionary<long, string> AvailableUntil = new Dictionary<long, string>();


        private readonly List<CargoDto> cargoDtos=new();
        private readonly List<UserDto> userDtos=new();  

        private readonly UserMediatRService _userMediatRService;
        private readonly CargoMediatRService _cargoMediatRService;

        public AdvancedBotUpdateHandler(UserMediatRService userMediatR,CargoMediatRService cargoMediatRService)
        {
            _userMediatRService=userMediatR;
            _cargoMediatRService = cargoMediatRService;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
            {
                string messageText = update.Message.Text?.Trim() ?? string.Empty;
                long chatId = update.Message.Chat.Id;

                if (userState.ContainsKey(chatId))
                {
                    await ProcessUserState(botClient, chatId, messageText, cancellationToken);
                }
                else
                {
                    // Bosh kelgan holatda komandalarni ishlash
                    switch (messageText)
                    {
                        case "/start":
                            var replyKeyboard = new ReplyKeyboardMarkup(new[]
                            {
                                new KeyboardButton[] { "Elon yaratish", "Elonlar ro'yxati" }
                            })
                            {
                                ResizeKeyboard = true
                            };

                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Welcome to Advanced Bot! Choose an option:",
                                replyMarkup: replyKeyboard,
                                cancellationToken: cancellationToken
                            );
                            break;

                        case "Elon yaratish":
                            await HandleElonYaratishLoop(botClient, chatId, cancellationToken);
                            break;

                        case "Elonlar ro'yxati":
                            await HandleElonlarRuyxati(botClient, chatId, cancellationToken);
                            break;

                        default:
                            await botClient.SendTextMessageAsync(chatId, "Unknown command. Type /help for available commands.", cancellationToken: cancellationToken);
                            break;
                    }
                }
            }
        }

        private async Task ProcessUserState(ITelegramBotClient botClient, long chatId, string messageText, CancellationToken cancellationToken)
        {
            if (messageText == "/start")
            {
                userState[chatId] = string.Empty;

                var replyKeyboard = new ReplyKeyboardMarkup(new[]
                {
                   new KeyboardButton[] { "Elon yaratish", "Elonlar ro'yxati" }
                 })
                {
                    ResizeKeyboard = true
                };

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Welcome to Advanced Bot! Choose an option:",
                    replyMarkup: replyKeyboard,
                    cancellationToken: cancellationToken
                );

                return;
            }
            else if(messageText =="Elon yaratish")
            {
                await HandleElonYaratishLoop(botClient, chatId, cancellationToken);
                return;
            }
            else if(messageText =="Elonlar ro'yxati")
            {
                await HandleElonlarRuyxati(botClient, chatId, cancellationToken);
                return;
            }

            switch (userState[chatId])
            {
                case "waitingForFirstName":
                    userFirstName[chatId] = messageText;
                    userState[chatId] = "waitingForLastName";
                    await botClient.SendTextMessageAsync(chatId, "Please enter your last name (e.g: Ergashev):", cancellationToken: cancellationToken);
                    break;

                case "waitingForLastName":
                    userLastName[chatId] = messageText;
                    userState[chatId] = "waitingForPhoneNumber";
                    await botClient.SendTextMessageAsync(chatId, "Please enter your PhoneNumber (e.g: +998908832603):", cancellationToken: cancellationToken);
                    break;

                case "waitingForPhoneNumber":
                    PhoneNumber[chatId] = messageText;
                    userState[chatId] = "waitingForTelegramUserName";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  TelegramUserName (e.g: @dotnet_dev1511):", cancellationToken: cancellationToken);
                    break;

                case "waitingForTelegramUserName":
                    TelegramUserName[chatId] = messageText;
                    userState[chatId] = "waitingForCargoType";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Cargo Type (e.g: Temir):", cancellationToken: cancellationToken);
                    break;

                case "waitingForCargoType":
                    CargoType[chatId] = messageText;
                    userState[chatId] = "waitingForCargoWeight";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Cargo Weight (e.g: 2 tonna):", cancellationToken: cancellationToken);
                    break;

                case "waitingForCargoWeight":
                    CargoWeight[chatId] = messageText;
                    userState[chatId] = "waitingForCargoVolume";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Volume (e.g: 5 metr kub):", cancellationToken: cancellationToken);
                    break;

                case "waitingForCargoVolume":
                    CargoVolume[chatId] = messageText;
                    userState[chatId] = "waitingForStartLocation";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Start Location (e.g: Qashqadaryo):", cancellationToken: cancellationToken);
                    break;

                case "waitingForStartLocation":
                    StartLocation[chatId] = messageText;
                    userState[chatId] = "waitingForDestinationLocation";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Destination Location (e.g: Toshkent):", cancellationToken: cancellationToken);
                    break;

                case "waitingForDestinationLocation":
                    DestinationLocation[chatId] = messageText;
                    userState[chatId] = "waitingForTypeOfTruck";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Type of Truck (e.g: Isuzu):", cancellationToken: cancellationToken);
                    break;

                case "waitingForTypeOfTruck":
                    TypeOfTruck[chatId] = messageText;
                    userState[chatId] = "waitingForNumberOfTruck";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Number of Truck (e.g: 2):", cancellationToken: cancellationToken);
                    break;
                case "waitingForNumberOfTruck":
                    NumberOfTruck[chatId] = messageText;
                    userState[chatId] = "waitingForPrice";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Price (e.g: 300 $):", cancellationToken: cancellationToken);
                    break;

                case "waitingForPrice":
                    Price[chatId] = messageText;
                    userState[chatId] = "waitingForAvailableForm";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Available Form (e.g: 10/11/2024):", cancellationToken: cancellationToken);
                    break;

                case "waitingForAvailableForm":
                    AvailableForm[chatId] = messageText;
                    userState[chatId] = "waitingForAvailableUntil";
                    await botClient.SendTextMessageAsync(chatId, "Please enter  Available Until (e.g: 15/11/2024):", cancellationToken: cancellationToken);
                    break;

                case "waitingForAvailableUntil":
                    AvailableUntil[chatId] = messageText;
                    userState[chatId] = "completed";

                    {
                        UserCreateCommand command = new UserCreateCommand()
                        {
                            UserDto = new UserDto
                            {
                                FirstName = userFirstName[chatId],
                                LastName = userLastName[chatId],
                                PhoneNumber = PhoneNumber[chatId],
                                TelegramUsername = TelegramUserName[chatId],
                            }
                        };
                        var ownerId = await _userMediatRService.CreateUserService(command);

                        CargoCreateCommand cargoCommand = new CargoCreateCommand()
                        {
                            CargoDto=new CargoDto
                            {
                                Type = CargoType[chatId],
                                Weight=CargoWeight[chatId],
                                Volume=CargoVolume[chatId],
                                Price = Price[chatId],
                                StartLocation=StartLocation[chatId],
                                DestinationLocation=DestinationLocation[chatId],
                                NumberOfTruck=int.Parse(NumberOfTruck[chatId]),
                                TypeOfTruck=TypeOfTruck[chatId],
                                AvailableForm=AvailableForm[chatId],
                                AvailableUntil=AvailableUntil[chatId],
                                OwnerId=ownerId,
                            }
                            
                        };
                        await _cargoMediatRService.CreateCargoService(cargoCommand);
                    }


                    await botClient.SendTextMessageAsync(
                               chatId,
                               $"From {StartLocation[chatId]}           To {DestinationLocation[chatId]}\n" +
                               $"Cargo Type                          {CargoType[chatId]}\n" +
                               $"Cargo Weight                       {CargoWeight[chatId]}\n" +
                               $"Cargo Volume                      {CargoVolume[chatId]}\n" +
                               $"Truck Type                         {TypeOfTruck[chatId]}\n" +
                               $"Number of Truck                 {NumberOfTruck[chatId]}\n" +
                               $"Price                                    {Price[chatId]}\n" +
                               $"From {AvailableForm[chatId]}              To {AvailableUntil[chatId]}\n" +
                               $"Owner                                  {userFirstName[chatId]}  {userLastName[chatId]}\n" +
                               $"Phone Number                     {PhoneNumber[chatId]}\n" +
                               $"Telegram UserName              {TelegramUserName[chatId]}",
                               cancellationToken: cancellationToken
                           );
                    userState.Remove(chatId);
                    break;
            }
        }

        public async Task HandleElonYaratishLoop(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            userState[chatId] = "waitingForFirstName"; 
            await botClient.SendTextMessageAsync(chatId, "Please enter your first name (e.g: Ali):", cancellationToken: cancellationToken);
        }

        private async Task HandleElonlarRuyxati(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {

          
            var cargoQuery=await _cargoMediatRService.GetAllCargoService();
           



            foreach (var item in cargoQuery)
            {
                await botClient.SendTextMessageAsync(
                               chatId,
                               $"From {item.StartLocation}           To {item.DestinationLocation}\n" +
                               $"Cargo Type                          {item.Type}\n" +
                               $"Cargo Weight                       {item.Weight}\n" +
                               $"Cargo Volume                      {item.Volume}\n" +
                               $"Truck Type                         {item.TypeOfTruck}\n" +
                               $"Number of Truck                 {item.NumberOfTruck}\n" +
                               $"Price                                    {item.Price}\n" +
                               $"From {item.AvailableForm}              To {item.AvailableUntil}\n" +
                               $"Owner                                  {item.UserDto.FirstName}  {item.UserDto.LastName}\n" +
                               $"Phone Number                     {item.UserDto.PhoneNumber}\n" +
                               $"Telegram UserName              {item.UserDto.TelegramUsername}",
                               cancellationToken: cancellationToken
                           );
            }
           
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }


       
    }

}

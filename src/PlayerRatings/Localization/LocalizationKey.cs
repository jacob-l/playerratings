using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlayerRatings.Localization
{
    //To sort static fields you can use resharper, for example.
    //Go to Resharper->Options->Code Editing->C#->File Layout
    //Choose Static Fields and Constants
    //Select "Sort By" - Access. And then by Name
    //Call Resharper->Tools->Clean up
    //http://stackoverflow.com/questions/1509244/resharper-clean-up-code-how-to-affect-sorting-of-methods#answer-31734205
    public class LocalizationKey
    {
        public static readonly LocalizationKey AddAnotherServiceToLogin =
            new LocalizationKey("Add another service to log in", "Добавить привязку к внешнему сервису");

        public static readonly LocalizationKey AddNewResult = new LocalizationKey("Add new result", "Добавить результат");

        public static readonly LocalizationKey AddThisAndAnotherOne = new LocalizationKey("Add this and another one",
            "Добавить этот и еще один");

        public static readonly LocalizationKey AddThisAndGoToRating = new LocalizationKey("Add and go to the rating",
            "Добавить и посмотреть рейтинг");

        public static readonly LocalizationKey AgainstFor = new LocalizationKey("Goals Against / For",
            "Голы Забито / Пропущено");

        public static readonly LocalizationKey AreYouSureDelete =
            new LocalizationKey("Are you sure you want to delete this", "Вы уверены, что хотите удалить");

        public static readonly LocalizationKey AssociateForm = new LocalizationKey("Association Form",
            "Связать аккаунты");

        public static readonly LocalizationKey AssociateYourAccount = new LocalizationKey("Associate your {0} account",
            "Привязать ваш аккаунт: {0}");

        public static readonly LocalizationKey BackToList = new LocalizationKey("Back to list", "Назад");
        public static readonly LocalizationKey Block = new LocalizationKey("Block", "Заблокировать");
        public static readonly LocalizationKey ChangePassword = new LocalizationKey("Change Password", "Изменить пароль");

        public static readonly LocalizationKey ChangePasswordForm = new LocalizationKey("Change Password Form",
            "Форма изменения пароля");

        public static readonly LocalizationKey ChangeYourAccountSettings =
            new LocalizationKey("Change your account settings", "Изменить настройки аккаунта");

        public static readonly LocalizationKey CheckEmailToReset =
            new LocalizationKey("Please check your email to reset your password",
                "Проверьте ваш почтовый ящик. Мы отправили вам ссылку для восстановления");

        public static readonly LocalizationKey ClickHereToLogin = new LocalizationKey("Click here to Log in",
            "Кликнете здесь чтобы залогиниться");

        public static readonly LocalizationKey ConfirmAccount =
            new LocalizationKey("Please confirm your account by clicking this link: <a href=\"{0}\">{0}</a>",
                "Пожалуйста, подтвердите свой аккаунт: <a href=\"{0}\">{0}</a>");

        public static readonly LocalizationKey ConfirmEmail = new LocalizationKey("Confirm Email",
            "Подтверждение адреса");

        public static readonly LocalizationKey ConfirmPassword = new LocalizationKey("Confirm password",
            "Подтверждение пароля");

        public static readonly LocalizationKey Create = new LocalizationKey("Create", "Добавить");
        public static readonly LocalizationKey CreateNew = new LocalizationKey("Create New", "Добавить");

        public static readonly LocalizationKey CreateNewAccount = new LocalizationKey("Create a new account",
            "Создание нового аккаунта");

        public static readonly LocalizationKey CreateYourLeague = new LocalizationKey("Create your league",
            "Создай свою лигу");

        public static readonly LocalizationKey Date = new LocalizationKey("Date", "Дата");

        public static readonly LocalizationKey DateIndex = new LocalizationKey("Index of Date column",
            "Номер колонки с датой");

        public static readonly LocalizationKey DateTimeFormat = new LocalizationKey("Date time format",
            "Формат даты и времени");

        public static readonly LocalizationKey Delete = new LocalizationKey("Delete", "Удалить");
        public static readonly LocalizationKey Details = new LocalizationKey("Details", "Подробности");

        public static readonly LocalizationKey DisplayName = new LocalizationKey("Display name", "Имя");
        public static readonly LocalizationKey Edit = new LocalizationKey("Edit", "Редактировать");
        public static readonly LocalizationKey Elo = new LocalizationKey("Elo", "Elo");
        public static readonly LocalizationKey Email = new LocalizationKey("Email", "Email");

        public static readonly LocalizationKey EnterYourEmail = new LocalizationKey("Enter your email",
            "Введите ваш емеил");

        public static readonly LocalizationKey Error = new LocalizationKey("Error", "Ошибка");

        public static readonly LocalizationKey ErrorOccurred = new LocalizationKey("An error has occurred",
            "Возникла ошибка");

        public static readonly LocalizationKey ErrorOccurredWhileProcessing =
            new LocalizationKey("An error occurred while processing your request",
                "Возникла ошибка во время обработки вашего запроса");

        public static readonly LocalizationKey ExternalLoginAdded = new LocalizationKey("The external login was added",
            "Логин добавлен");

        public static readonly LocalizationKey ExternalLoginRemoved =
            new LocalizationKey("The external login was removed", "Логин удален");

        public static readonly LocalizationKey ExternalLogins = new LocalizationKey("External Logins", "Внешние сервисы");

        public static readonly LocalizationKey ExternalRegisterSuccessInstuction =
            new LocalizationKey(
                "Please enter a name for this site below and click the Register button to finish logging in",
                "Пожалуйста, введите ваше имя и нажмите кнопку Зарегистрироваться");

        public static readonly LocalizationKey FactorIndex = new LocalizationKey("Index of Factor column",
            "Номер колонки с фактором");

        public static readonly LocalizationKey File = new LocalizationKey("File", "Файл");
        public static readonly LocalizationKey FirstPlayer = new LocalizationKey("First player", "Первый игрок");

        public static readonly LocalizationKey FirstPlayerEmailIndex =
            new LocalizationKey("Index of First Player Email column", "Номер колонки с email первого игрока");

        public static readonly LocalizationKey FirstPlayerScore = new LocalizationKey("First player score",
            "Счет первого игрока");

        public static readonly LocalizationKey FirstPlayerScoreIndex =
            new LocalizationKey("Index of First Player Score column", "Номер колонки со счетом первого игрока");

        public static readonly LocalizationKey Forecast = new LocalizationKey("Forecast", "Предсказание");

        public static readonly LocalizationKey ForgotPasswordConfirmation =
            new LocalizationKey("Forgot Password Confirmation", "Подтверждение");

        public static readonly LocalizationKey ForgotYourPassword = new LocalizationKey("Forgot your password",
            "Забыли ваш пароль");

        public static readonly LocalizationKey HasNoConfiguredServices =
            new LocalizationKey("Authentication services was not configured", "Нету настроенных сервисов");

        public static readonly LocalizationKey Hello = new LocalizationKey("Hello", "Привет");

        public static readonly LocalizationKey Import = new LocalizationKey("Import", "Импортировать");

        public static readonly LocalizationKey ImportData =
            new LocalizationKey(
                "You can import your matches in csv format. Each record must contain Date, First Player Email, Second Player Email, First Player Score, Second Player Score and optionally Factor. Unknown users will be invited to league automatically",
                "Вы можете импортировать ваши матчи в csv формате. Каждая запись должна содержать дату, email первого игрока, email второго игрока, счет первого игрока, счет второго игрока и опционально фактор. Неизвестные игроки будут автоматически приглашены в лигу");

        public static readonly LocalizationKey ImportMatches = new LocalizationKey("Import matches",
            "Импортировать матчи");

        public static readonly LocalizationKey InvitationForm = new LocalizationKey("Invintation Form",
            "Форма приглашения");

        public static readonly LocalizationKey Invite = new LocalizationKey("Invite", "Пригласить");

        public static readonly LocalizationKey InvitedYou =
            new LocalizationKey("{0} invited you to join the rating system",
                "{0} пригласил Вас присоединиться к рейтингу");

        public static readonly LocalizationKey InviteNew =
            new LocalizationKey("Invite new player", "Пригласить нового пользователя");

        public static readonly LocalizationKey Invites = new LocalizationKey("Invites", "Приглашения");
        public static readonly LocalizationKey LastMatches = new LocalizationKey("Last matches", "Последние матчи");
        public static readonly LocalizationKey League = new LocalizationKey("League", "Лига");

        public static readonly LocalizationKey LeagueNotFound =
            new LocalizationKey("Can not find league or you don't have access",
                "Не могу найти лигу или у вас нет доступа");

        public static readonly LocalizationKey Leagues = new LocalizationKey("Leagues", "Лиги");
        public static readonly LocalizationKey LockedOut = new LocalizationKey("Locked out", "Заблокировано");

        public static readonly LocalizationKey LockedOutTryLater =
            new LocalizationKey("This account has been locked out, please try again later",
                "Этот аккаунт заблокирован. Попробуйте позднее");

        public static readonly LocalizationKey LogIn = new LocalizationKey("Log in", "Войти");
        public static readonly LocalizationKey LoginFailure = new LocalizationKey("Login Failure", "Неудачный логин");

        public static readonly LocalizationKey LogInUsingExternal = new LocalizationKey(
            "Log in using your {0} account", "Войти используя ваш {0} аккаунт");

        public static readonly LocalizationKey LogOff = new LocalizationKey("Log off", "Выйти");
        public static readonly LocalizationKey LooseStreak = new LocalizationKey("Loose streak", "Поражения подряд");
        public static readonly LocalizationKey Manage = new LocalizationKey("Manage", "Управлять");

        public static readonly LocalizationKey ManageYourAccount = new LocalizationKey("Manage your account",
            "Управление аккаунтом");

        public static readonly LocalizationKey ManageYourExternalLogins =
            new LocalizationKey("Manage your external logins", "Управление внешними аккаунтами");

        public static readonly LocalizationKey Match = new LocalizationKey("Match", "Матч");
        public static readonly LocalizationKey Matches = new LocalizationKey("Matches", "Матчи");
        public static readonly LocalizationKey NewPassword = new LocalizationKey("New password", "Новый пароль");

        public static readonly LocalizationKey NoLeagues =
            new LocalizationKey("You have no leagues", "У вас нету ни одной лиги");

        public static readonly LocalizationKey OldPassword = new LocalizationKey("Old password", "Старый пароль");

        public static readonly LocalizationKey Password = new LocalizationKey("Password", "Пароль");

        public static readonly LocalizationKey PasswordChanged = new LocalizationKey("Your password has been changed",
            "Ваш пароль изменен");

        public static readonly LocalizationKey PasswordSet = new LocalizationKey("Your password has been set",
            "Пароль установлен");

        public static readonly LocalizationKey PlayerNotFound =
            new LocalizationKey("Can not find player or you don't have access",
                "Не могу найти игрока или у вас нет доступа");

        public static readonly LocalizationKey Players =
            new LocalizationKey("Players", "Игроки");

        public static readonly LocalizationKey Rating = new LocalizationKey("Rating", "Рейтинг");

        public static readonly LocalizationKey RatingSource = new LocalizationKey("Source of rating",
            "Источник рейтинга");

        public static readonly LocalizationKey Register = new LocalizationKey("Register", "Зарегистрироваться");

        public static readonly LocalizationKey RegisteredLogins = new LocalizationKey("Registered Logins",
            "Добавленные аккаунты");

        public static readonly LocalizationKey RegisterNewUser = new LocalizationKey("Register as a new user",
            "Зарегистрировать нового пользователя");

        public static readonly LocalizationKey RememberMe = new LocalizationKey("Remember me", "Запомнить меня");
        public static readonly LocalizationKey Remove = new LocalizationKey("Remove", "Удалить");

        public static readonly LocalizationKey RemoveExternalFrom =
            new LocalizationKey("Remove this {0} login from your account", "Удалить привязку к {0}");

        public static readonly LocalizationKey ResendInvitation = new LocalizationKey("Resend invitation again",
            "Отправить приглашение еще раз");

        public static readonly LocalizationKey ResetPassword = new LocalizationKey("Reset Password",
            "Восстановить пароль");

        public static readonly LocalizationKey ResetPasswordConfirmation =
            new LocalizationKey("Reset password confirmation", "Подтверждение сброса пароля");

        public static readonly LocalizationKey ResetPasswordInstruction =
            new LocalizationKey("Please reset your password by clicking here: <a href=\"{0}\">link</a>",
                "Чтобы сбросить пароль передйдите по <a href=\"{0}\">ссылке</a>");

        public static readonly LocalizationKey Save = new LocalizationKey("Save", "Сохранить");
        public static readonly LocalizationKey SecondPlayer = new LocalizationKey("Second player", "Второй игрок");

        public static readonly LocalizationKey SecondPlayerEmailIndex =
            new LocalizationKey("Index of Second Player Email column", "Номер колонки с email второго игрока");

        public static readonly LocalizationKey SecondPlayerScore = new LocalizationKey("Second player score",
            "Счет второго игрока");

        public static readonly LocalizationKey SecondPlayerScoreIndex =
            new LocalizationKey("Index of Second Player Score column", "Номер колонки со счетом второго игрока");

        public static readonly LocalizationKey SelectOne = new LocalizationKey("Please select one", "Выберите игрока");
        public static readonly LocalizationKey SetPassword = new LocalizationKey("Set Password", "Установить пароль");
        public static readonly LocalizationKey Submit = new LocalizationKey("Submit", "Отправить");

        public static readonly LocalizationKey ThankYouForConfirm =
            new LocalizationKey("Thank you for confirming your email", "Спасибо за подтверждение вашего адреса");

        public static readonly LocalizationKey ToggleNavigation = new LocalizationKey("Toggle navigation",
            "Переключение навигации");

        public static readonly LocalizationKey Unblock = new LocalizationKey("Unblock", "Разблокировать");

        public static readonly LocalizationKey UnsuccessfulLoginWithService =
            new LocalizationKey("Unsuccessful login with service", "Не получилось войти через сервис");

        public static readonly LocalizationKey UseAnotherService = new LocalizationKey("Use another service to log in",
            "Использовать сторонние сервисы");

        public static readonly LocalizationKey UseLocalAccountToLogin =
            new LocalizationKey("Use a local account to log in", "Использовать аккаунт для входа");

        public static readonly LocalizationKey WinRate = new LocalizationKey("Win rate", "Соотношение побед");
        public static readonly LocalizationKey WinStreak = new LocalizationKey("Win streak", "Победы подряд");

        public static readonly LocalizationKey YouCanInviteNewPlayer =
            new LocalizationKey("You can invite new player to the league", "Вы можете добавить нового игрока в лигу");

        public static readonly LocalizationKey YouDontHaveLocalAccount =
            new LocalizationKey(
                "You do not have a local username/password for this site. Add a local account so you can log in without an external login",
                "У вас нету локального аккаунта в системе. Добавьте аккаунт, чтобы заходить в систему без внешних сервисов");

        public static readonly LocalizationKey YourPasswordHasBeenReset =
            new LocalizationKey("Your password has been reset", "Ваш пароль был сброшен");

        public static readonly LocalizationKey YouSuccessfullyAuthenticatedWith =
            new LocalizationKey("You've successfully authenticated with", "Вы успешно зашли через");

        private const string En = "en";

        private const string Ru = "ru";

        private readonly Dictionary<string, string> _values;

        private LocalizationKey(string en, string ru)
        {
            _values = new Dictionary<string, string>
            {
                {En, en},
                {Ru, ru}
            };
        }


        public static string GetLocalization(string name, string preferedLanguage)
        {
            var property = typeof (LocalizationKey).GetField(name,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

            if (property == null)
            {
                return name;
            }

            var localization = property.GetValue(null) as LocalizationKey;

            if (localization == null)
            {
                return name;
            }

            if (localization._values.ContainsKey(preferedLanguage) &&
                !string.IsNullOrEmpty(localization._values[preferedLanguage]))
            {
                return localization._values[preferedLanguage];
            }

            return localization._values.FirstOrDefault(kvp => !string.IsNullOrEmpty(kvp.Value)).Value;
        }
    }
}
using BusStation.DAL;
using BusStation.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BusStation.UI
{
    public class Actions
    {
        public static BusStationDbStorage storage = new BusStationDbStorage(new BusStationContext());



        public static string BusToString(Bus b)
        {
            string res = b.BusId + ". " + b.Model + " (" + b.RegNum + ")\n";
            return res;
        }
        public static string PassengerToString(Passenger p)
        {
            string res = p.PassengerId + ". " + p.FullName + ", " + p.IdType + ": " + p.IdNum + '\n';
            return res;
        }

        public static string RouteToString(Route r)
        {
            string res = r.RouteId + ". " + r.Name + ", Количество остановок: " + r.StopsNum + '\n';
            return res;
        }

        public static string RunToString(Run r)
        {
            string res = r.RunId + ". " + r.Name + '\n';
            res += "   " + BusToString(storage.GetBusById(r.BusId));
            res += "   " + RouteToString(storage.GetRouteById(r.RouteId));
            res += "   " + "Дата отправления: " + r.DepartureDate.ToShortDateString() + '\n';
            res += "   " + "Дата прибытия: " + r.ArrivalDate.ToShortDateString() + '\n';
            res += "   Пассажиры:\n";
            foreach (Passenger p in r.Passengers)
            {
                res += "      " + PassengerToString(p);
            }
            return res;
        }

        public static void PrintAllBuses()
        {
            var allBuses = storage.GetAllBuses();
            if (allBuses.Any())
            {
                foreach (Bus b in allBuses)
                {
                    Console.Write(BusToString(b));
                }
            }
            else
            {
                Console.WriteLine("Список автобусов пуст.");
            }
        }

        public static void PrintAllPassengers()
        {
            var allPassengers = storage.GetAllPassengers();
            if (allPassengers.Any())
            {
                foreach (Passenger p in allPassengers)
                {
                    Console.Write(PassengerToString(p));
                }
            }
            else
            {
                Console.WriteLine("Список пассажиров пуст.");
            }
        }

        public static void PrintAllRoutes()
        {
            var allRoutes = storage.GetAllRoutes();
            if (allRoutes.Any())
            {
                foreach (Route r in allRoutes)
                {
                    Console.Write(RouteToString(r));
                }
            }
            else
            {
                Console.WriteLine("Список маршрутов пуст.");
            }
        }

        public static void PrintAllRuns()
        {
            var allRuns = storage.GetAllRuns();
            if (allRuns.Any())
            {
                foreach (Run r in allRuns)
                {
                    Console.Write(RunToString(r));
                }
            }
            else
            {
                Console.WriteLine("Список рейсов пуст.");
            }
        }

        public static Bus GetBusFromUserInput()
        {
            Bus bus = new Bus();
            do
            {
                Console.WriteLine("Введите модель автобуса:");
                Console.Write(">>");
                bus.Model = Console.ReadLine();
            } while (string.IsNullOrEmpty(bus.Model));

            bool validRegNum = false;
            do
            {
                Console.WriteLine("Введите регистрационный номер (должен содержать хотя бы одну цифру):");
                Console.Write(">>");
                bus.RegNum = Console.ReadLine();
                validRegNum = ContainsDigit(bus.RegNum);
                if (!validRegNum)
                {
                    Console.WriteLine("Регистрационный номер должен содержать хотя бы одну цифру.");
                }
            } while (string.IsNullOrEmpty(bus.RegNum) || !validRegNum);

            return bus;
        }

        private static bool ContainsDigit(string input)
        {
            return input.Any(char.IsDigit);
        }

        public static Passenger GetPassengerFromUserInput()
        {
            Passenger passenger = new Passenger();
            do
            {
                Console.WriteLine("Введите ФИО пассажира:");
                Console.Write(">>");
                passenger.FullName = Console.ReadLine();
            } while (string.IsNullOrEmpty(passenger.FullName) || IsTextOnly(passenger.FullName));

            do
            {
                Console.WriteLine("Введите тип документа:");
                Console.Write(">>");
                passenger.IdType = Console.ReadLine();
            } while (string.IsNullOrEmpty(passenger.IdType) || !IsTextOnly(passenger.IdType));

            string idNum;
            bool validIdNum;
            do
            {
                Console.WriteLine("Введите номер документа (только цифры и специальные символы):");
                Console.Write(">>");
                idNum = Console.ReadLine();
                validIdNum = IsAlphaNumeric(idNum);
                if (!validIdNum)
                {
                    Console.WriteLine("Номер документа должен содержать только цифры и специальные символы.");
                }
            } while (string.IsNullOrEmpty(idNum) || !validIdNum);

            passenger.IdNum = idNum;
            return passenger;
        }

        public static bool IsTextOnly(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter);
        }

        private static bool IsAlphaNumeric(string str)
        {
            Regex regex = new Regex("^[0-9\\s\\-_.]+$");
            return regex.IsMatch(str);
        }

        public static Route GetRouteFromUserInput()
        {
            Route route = new Route();
            bool validName;
            do
            {
                Console.WriteLine("Введите название маршрута:");
                Console.Write(">>");
                route.Name = Console.ReadLine();
                validName = !string.IsNullOrWhiteSpace(route.Name) && !route.Name.All(char.IsDigit);
                if (!validName)
                {
                    Console.WriteLine("Название маршрута не может состоять только из цифр или быть пустым.");
                }
            } while (!validName);

            int stopsNum;
            bool validInput;
            do
            {
                Console.WriteLine("Введите количество остановок:");
                Console.Write(">>");
                validInput = int.TryParse(Console.ReadLine(), out stopsNum);
                if (!validInput)
                {
                    Console.WriteLine("Неверный ввод для количества остановок. Пожалуйста, введите целое число.");
                }
            } while (!validInput);

            route.StopsNum = stopsNum;
            return route;
        }

        public static Run GetRunFromUserInput()
        {
            Run run = new Run();
            bool validRunName;
            do
            {
                Console.WriteLine("Введите название рейса:");
                Console.Write(">>");
                run.Name = Console.ReadLine();
                validRunName = !string.IsNullOrWhiteSpace(run.Name) && !run.Name.All(char.IsDigit);
                if (!validRunName)
                {
                    Console.WriteLine("Название рейса не может состоять только из цифр или быть пустым.");
                }
            } while (!validRunName);

            Console.WriteLine("Установка автобуса на рейс:");
            Bus selectedBus = GetUserChoiceForBus();
            if (selectedBus != null)
            {
                run.BusId = selectedBus.BusId;
            }
            else
            {
                Console.WriteLine("Автобус не выбран.");
                return null;
            }

            Console.WriteLine("Установка маршрута рейса:");
            Route selectedRoute = GetUserChoiceForRoute();
            if (selectedRoute != null)
            {
                run.RouteId = selectedRoute.RouteId;
            }
            else
            {
                Console.WriteLine("Маршрут не выбран.");
                return null;
            }

            DateTime departureDate;
            bool validDepartureDate;
            do
            {
                Console.WriteLine("Введите дату и время отправления (гггг-мм-дд ЧЧ:мм:сс):");
                Console.Write(">>");
                validDepartureDate = DateTime.TryParse(Console.ReadLine(), out departureDate);
                if (!validDepartureDate)
                {
                    Console.WriteLine("Неверный ввод для даты отправления.");
                }
            } while (!validDepartureDate);

            run.DepartureDate = departureDate;

            DateTime arrivalDate;
            bool validArrivalDate;
            do
            {
                Console.WriteLine("Введите дату и время прибытия (гггг-мм-дд ЧЧ:мм:сс):");
                Console.Write(">>");
                validArrivalDate = DateTime.TryParse(Console.ReadLine(), out arrivalDate);
                if (!validArrivalDate)
                {
                    Console.WriteLine("Неверный ввод для даты прибытия.");
                }
            } while (!validArrivalDate);

            run.ArrivalDate = arrivalDate;

            return run;
        }
        public static Bus GetBusFromList()
        {
            if (storage.GetAllBuses().Count == 0)
            {
                Console.WriteLine("Список автобусов пуст. Пожалуйста, добавьте новый автобус.");
                return GetBusFromUserInput();
            }
            Bus selectedBus = null;
            bool validInput = false;

            while (!validInput)
            {
                PrintAllBuses();
                Console.WriteLine("Введите ID автобуса:");
                Console.Write(">>");
                if (int.TryParse(Console.ReadLine(), out int busId))
                {
                    try
                    {
                        selectedBus = storage.GetBusById(busId);
                        validInput = true;
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Автобус с указанным ID не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод ID автобуса.");
                }
            }

            return selectedBus;
        }

        public static Passenger GetPassengerFromList()
        {
            if (storage.GetAllPassengers().Count == 0)
            {
                Console.WriteLine("Список пассажиров пуст. Пожалуйста, добавьте нового пассажира.");
                return GetPassengerFromUserInput();
            }
            Passenger selectedPassenger = null;
            bool validInput = false;

            while (!validInput)
            {
                PrintAllPassengers();
                Console.WriteLine("Введите ID пассажира:");
                Console.Write(">>");
                if (int.TryParse(Console.ReadLine(), out int passengerId))
                {
                    try
                    {
                        selectedPassenger = storage.GetPassengerById(passengerId);
                        validInput = true;
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Пассажир с указанным ID не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод ID пассажира.");
                }
            }

            return selectedPassenger;
        }

        public static Route GetRouteFromList()
        {
            if (storage.GetAllRoutes().Count == 0)
            {
                Console.WriteLine("Список маршрутов пуст. Пожалуйста, добавьте новый маршрут.");
                return GetRouteFromUserInput();
            }
            Route selectedRoute = null;
            bool validInput = false;

            while (!validInput)
            {
                PrintAllRoutes();
                Console.WriteLine("Введите ID маршрута:");
                Console.Write(">>");
                if (int.TryParse(Console.ReadLine(), out int routeId))
                {
                    try
                    {
                        selectedRoute = storage.GetRouteById(routeId);
                        validInput = true;
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Маршрут с указанным ID не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод ID маршрута.");
                }
            }

            return selectedRoute;
        }

        public static Run GetRunFromList()
        {
            if (storage.GetAllRuns().Count == 0)
            {
                Console.WriteLine("Список рейсов пуст. Пожалуйста, добавьте новый рейс.");
                return GetRunFromUserInput();
            }
            Run selectedRun = null;
            bool validInput = false;

            while (!validInput)
            {
                PrintAllRuns();
                Console.WriteLine("Введите ID рейса:");
                Console.Write(">>");
                if (int.TryParse(Console.ReadLine(), out int runId))
                {
                    try
                    {
                        selectedRun = storage.GetRunById(runId);
                        validInput = true;
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Рейс с указанным ID не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод ID рейса.");
                }
            }

            return selectedRun;
        }

        public static Bus GetUserChoiceForBus()
        {
            Bus selectedBus = null;
            bool validChoice = false;

            do
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать новый автобус");
                Console.WriteLine("2. Выбрать из списка существующих автобусов");
                Console.Write(">>");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        selectedBus = GetBusFromUserInput();
                        validChoice = true;
                        break;

                    case "2":
                        selectedBus = GetBusFromList();
                        validChoice = true;
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            } while (!validChoice);

            return selectedBus;
        }

        public static Passenger GetUserChoiceForPassenger()
        {
            Passenger selectedPassenger = null;
            bool validChoice = false;

            do
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать нового пассажира");
                Console.WriteLine("2. Выбрать из списка существующих пассажиров");
                Console.Write(">>");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        selectedPassenger = GetPassengerFromUserInput();
                        validChoice = true;
                        break;

                    case "2":
                        selectedPassenger = GetPassengerFromList();
                        validChoice = true;
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            } while (!validChoice);

            return selectedPassenger;
        }

        public static Route GetUserChoiceForRoute()
        {
            Route selectedRoute = null;
            bool validChoice = false;

            do
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать новый маршрут");
                Console.WriteLine("2. Выбрать из списка существующих маршрутов");
                Console.Write(">>");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        selectedRoute = GetRouteFromUserInput();
                        validChoice = true;
                        break;

                    case "2":
                        selectedRoute = GetRouteFromList();
                        validChoice = true;
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            } while (!validChoice);

            return selectedRoute;
        }

        public static Run GetUserChoiceForRun()
        {
            Run selectedRun = null;
            bool validChoice = false;

            do
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать новый рейс");
                Console.WriteLine("2. Выбрать из списка существующих рейсов");
                Console.Write(">>");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        selectedRun = GetRunFromUserInput();
                        validChoice = true;
                        break;

                    case "2":
                        selectedRun = GetRunFromList();
                        validChoice = true;
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            } while (!validChoice);

            return selectedRun;
        }

        public static void AddBus()
        {
            Console.WriteLine("Добавление автобуса в список:");
            Bus bus = GetBusFromUserInput();
            storage.AddBus(bus);
            Console.WriteLine("Автобус добавлен");
        }

        public static void AddPassenger()
        {
            Console.WriteLine("Добавление пассажира в список:");
            Passenger passenger = GetPassengerFromUserInput();
            storage.AddPassenger(passenger);
            Console.WriteLine("Пассажир добавлен");
        }

        public static void AddRoute()
        {
            Console.WriteLine("Добавление маршрута в список:");
            Route route = GetRouteFromUserInput();
            storage.AddRoute(route);
            Console.WriteLine("Маршрут добавлен");
        }

        public static void AddRun()
        {
            Console.WriteLine("Добавление рейса в список:");
            Run run = GetRunFromUserInput();
            storage.AddRun(run);
            Console.WriteLine("Рейс добавлен");
        }

        public static void RemoveBus()
        {
            Console.WriteLine("Удаление автобуса:");
            Bus busToRemove = GetBusFromList();

            if (busToRemove != null)
            {
                storage.RemoveBus(busToRemove);
                Console.WriteLine("Автобус удален");
            }
        }

        public static void RemovePassenger()
        {
            Console.WriteLine("Удаление пассажира:");
            Passenger passengerToRemove = GetPassengerFromList();

            if (passengerToRemove != null)
            {
                storage.RemovePassenger(passengerToRemove);
                Console.WriteLine("Пассажир удален");
            }
        }

        public static void RemoveRoute()
        {
            Console.WriteLine("Удаление маршрута:");
            Route routeToRemove = GetRouteFromList();

            if (routeToRemove != null)
            {
                storage.RemoveRoute(routeToRemove);
                Console.WriteLine("Маршрут удален");
            }
        }

        public static void RemoveRun()
        {
            Console.WriteLine("Удаление рейса:");
            Run runToRemove = GetRunFromList();

            if (runToRemove != null)
            {
                storage.RemoveRun(runToRemove);
                Console.WriteLine("Рейс удален");
            }
        }

        public static void AddPassengerToRunById(int RunId, int PassangerId)
        {
            storage.GetRunById(RunId).Passengers.Add(storage.GetPassengerById(PassangerId));
            storage.SaveChanges();
        }

        public static void AddPassengerToRun()
        {
            Console.WriteLine("Добавление пассажира к рейсу:");

            Run selectedRun = GetUserChoiceForRun();

            if (selectedRun != null)
            {
                Passenger selectedPassenger = GetUserChoiceForPassenger();

                if (selectedPassenger != null)
                {
                    AddPassengerToRunById(selectedRun.RunId, selectedPassenger.PassengerId);
                    Console.WriteLine("Пассажир добавлен к рейсу");
                }
                else
                {
                    Console.WriteLine("Пассажир не выбран.");
                }
            }
            else
            {
                Console.WriteLine("Рейс не выбран.");
            }
        }

        public static void ShowMainMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("Выберите объект для взаимодействия:");
                Console.WriteLine("1. Автобусы");
                Console.WriteLine("2. Пассажиры");
                Console.WriteLine("3. Маршруты");
                Console.WriteLine("4. Рейсы");
                Console.WriteLine("0. Выйти");
                Console.Write(">>");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = -1;
                }

                switch (choice)
                {
                    case 1:
                        ShowBusesMenu();
                        break;
                    case 2:
                        ShowPassengersMenu();
                        break;
                    case 3:
                        ShowRoutesMenu();
                        break;
                    case 4:
                        ShowRunsMenu();
                        break;
                    case 0:
                        Console.WriteLine("Завершение работы.");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, выберите один из вариантов.");
                        break;
                }
            } while (choice != 0);
        }

        public static void ShowBusesMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("Доступные действия:");
                Console.WriteLine("1. Удалить автобус из списка");
                Console.WriteLine("2. Добавить автобус в список");
                Console.WriteLine("3. Вывести список автобусов");
                Console.WriteLine("0. Вернуться в предыдущее меню");
                Console.Write(">>");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = -1;
                }

                switch (choice)
                {
                    case 1:
                        Actions.RemoveBus();
                        break;
                    case 2:
                        Actions.AddBus();
                        break;
                    case 3:
                        Actions.PrintAllBuses();
                        break;
                    case 0:
                        Console.WriteLine("Возврат в предыдущее меню.");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, выберите один из вариантов.");
                        break;
                }
            } while (choice != 0);
        }

        
        public static void ShowPassengersMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("Доступные действия:");
                Console.WriteLine("1. Удалить пассажира из списка");
                Console.WriteLine("2. Добавить пассажира в список");
                Console.WriteLine("3. Вывести список пассажиров");
                Console.WriteLine("0. Вернуться в предыдущее меню");
                Console.Write(">>");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = -1;
                }

                switch (choice)
                {
                    case 1:
                        Actions.RemovePassenger();
                        break;
                    case 2:
                        Actions.AddPassenger();
                        break;
                    case 3:
                        Actions.PrintAllPassengers();
                        break;
                    case 0:
                        Console.WriteLine("Возврат в предыдущее меню.");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, выберите один из вариантов.");
                        break;
                }
            } while (choice != 0);
        }


        public static void ShowRoutesMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("Доступные действия:");
                Console.WriteLine("1. Удалить маршрут из списка");
                Console.WriteLine("2. Добавить маршрут в список");
                Console.WriteLine("3. Вывести список маршрутов");
                Console.WriteLine("0. Вернуться в предыдущее меню");
                Console.Write(">>");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = -1;
                }

                switch (choice)
                {
                    case 1:
                        Actions.RemoveRoute();
                        break;
                    case 2:
                        Actions.AddRoute();
                        break;
                    case 3:
                        Actions.PrintAllRoutes();
                        break;
                    case 0:
                        Console.WriteLine("Возврат в предыдущее меню.");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, выберите один из вариантов.");
                        break;
                }
            } while (choice != 0);
        }

        public static void ShowRunsMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("Доступные действия:");
                Console.WriteLine("1. Удалить рейс из списка");
                Console.WriteLine("2. Добавить рейс в список");
                Console.WriteLine("3. Вывести список рейсов");
                Console.WriteLine("4. Добавить пассажира на рейс");
                Console.WriteLine("0. Вернуться в предыдущее меню");
                Console.Write(">>");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    choice = -1;
                }

                switch (choice)
                {
                    case 1:
                        Actions.RemoveRun();
                        break;
                    case 2:
                        Actions.AddRun();
                        break;
                    case 3:
                        Actions.PrintAllRuns();
                        break;
                    case 4:
                        Actions.AddPassengerToRun();
                        break;
                    case 0:
                        Console.WriteLine("Возврат в предыдущее меню.");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, выберите один из вариантов.");
                        break;
                }
            } while (choice != 0);
        }

    }
}

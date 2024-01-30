//==========================================================
// Student Number : S10257575
// Student Name : Chen Jingyuan
// Partner Name : Aarence
//==========================================================

using System.Data;
using System.Drawing;
using Classes;

class Program
{
    public class InvalidOptionException : Exception
    {
        public InvalidOptionException()
            : base("Invalid option. Must be between 0 and 8 inclusive.") { }
    }

    public class InvalidScoopsException : Exception
    {
        public InvalidScoopsException()
            : base("There must be at least one scoop") { }
    }

    public class InvalidYearException : Exception
    {
        public InvalidYearException()
            : base("Invalid year. Year must be between 2013 and 2034 inclusive.") { }
    }

    static void readCustomerData(List<Customer> CustomerList, List<PointCard> PointCardsList)
    {
        string filePath = "C:\\Codes For Days\\C#\\customers.csv";
        string[] lines = File.ReadAllLines(filePath);

        bool firstLineSkipped = false;
        foreach (string line in lines)
        {
            if (!firstLineSkipped)
            {
                firstLineSkipped = true;
                continue;
            }

            string[] data = line.Split(',');

            if (data.Length == 6)
            {
                string Name = data[0].Trim();
                int MemberID = Convert.ToInt32(data[1].Trim());
                DateTime DOB = Convert.ToDateTime(data[2].Trim());
                string MemberTier = data[3].Trim();
                int Points = Convert.ToInt32(data[4].Trim());
                int PunchCard = Convert.ToInt32(data[5].Trim());

                PointCard newPointCard = new PointCard(Points, PunchCard);
                Customer newCustomer = new Customer(Name, MemberID, DOB);

                newCustomer.rewards = newPointCard;
                newPointCard.Tier = MemberTier;

                CustomerList.Add(newCustomer);
                PointCardsList.Add(newPointCard);
            }
            else
            {
                Console.WriteLine($"Invalid data format: {line}");
            }
        }

        foreach (var customer in CustomerList)
        {
            System.Console.WriteLine($"{customer}, {customer.rewards}");
        }
    }

    static void RegisterNewCustomer(List<Customer> CustomerList, List<PointCard> PointCardsList)
    {
        string Input1 = "";
        string Name = "";
        int MemberID = 0;
        DateTime DOB = DateTime.MinValue;
        string Input2 = " ";
        string MemberTier = " ";
        int Points = 0;
        int PunchCard = 0;

        bool inputValid = false;

        while (!inputValid)
        {
            try
            {
                Console.WriteLine("Enter customer name: ");
                Input1 = Console.ReadLine();
                Name = char.ToUpper(Input1[0]) + Input1.Substring(1).ToLower();

                Console.WriteLine("Enter customer member ID: ");
                MemberID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter customer DOB(dd/mm/yyyy): ");
                DOB = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Enter Member tier: ");
                Input2 = Console.ReadLine();
                MemberTier = char.ToUpper(Input2[0]) + Input2.Substring(1).ToLower();

                Console.WriteLine("Enter number of points: ");
                Points = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter PunchCard number: ");
                PunchCard = Convert.ToInt32(Console.ReadLine());

                inputValid = true;
            }
            catch (FormatException a)
            {
                Console.WriteLine("Invalid input format. Please enter valid data.");
                System.Console.WriteLine("customers registration unsuccessful");
            }
            catch (IndexOutOfRangeException b)
            {
                System.Console.WriteLine("Data entered cannot be null.");
                System.Console.WriteLine("customers registration unsuccessful");
            }
            catch (Exception c)
            {
                System.Console.WriteLine("Something went wrong");
                System.Console.WriteLine("customers registration unsuccessful");
            }
        }

        PointCard newPointCard = new PointCard(Points, PunchCard);
        Customer newCustomer = new Customer(Name, MemberID, DOB);

        newCustomer.rewards = newPointCard;
        newPointCard.Tier = MemberTier;

        CustomerList.Add(newCustomer);
        PointCardsList.Add(newPointCard);

        using (
            StreamWriter writer = new StreamWriter("C:\\Codes For Days\\C#\\customers.csv", true)
        )
        {
            string DOBwithoutTime = DOB.ToString("dd/MM/yyyy");
            string patientData =
                $"{Name},{MemberID},{DOBwithoutTime},{MemberTier},{Points},{PunchCard}";
            writer.WriteLine("");
            writer.Write(patientData);

            System.Console.WriteLine("Customer registration successful!");
        }
    }

    static void CreateOrder(
        List<Customer> CustomerList,
        List<PointCard> PointCardsList,
        List<Flavour> FlavourList,
        List<Topping> ToppingList,
        List<IceCream> IceCreamList,
        Queue<Order> regularOrderQueue,
        Queue<Order> goldOrderQueue
    )
    {
        bool cont = true;
        readCustomerData(CustomerList, PointCardsList);

        int memberId;
        do
        {
            Console.Write("Enter the MemberId of the customer: ");
        } while (
            !int.TryParse(Console.ReadLine(), out memberId)
            || !CustomerList.Any(c => c.MemberID == memberId)
        );

        Customer selectedCustomer = CustomerList.First(c => c.MemberID == memberId);

        Console.WriteLine($"Selected Customer: {selectedCustomer.name}");

        Console.Write("Enter Order Id: ");
        int orderId;
        while (!int.TryParse(Console.ReadLine(), out orderId))
        {
            Console.WriteLine("Invalid input. Please enter a valid Order Id.");
            Console.Write("Enter Order Id: ");
        }

        DateTime timeReceived = DateTime.Now;
        Order newOrder = new Order(orderId, timeReceived);
        selectedCustomer.CurrentOrder = newOrder;

        string option = "";

        try
        {
            System.Console.WriteLine("Enter Ice cream option(waffle, cone or cup): ");
            option = Console.ReadLine().ToLower();
        }
        catch (FormatException a)
        {
            System.Console.WriteLine("Invalid data format entered.");
        }
        catch (Exception b)
        {
            System.Console.WriteLine("Something went wrong.");
        }
        while (cont)
        {
            if (option == "cup")
            {
                string topping = "";
                int number2 = 0;
                int number = 0;
                string flavour = "";
                int scoops = 0;
                bool dipped = false;
                while (number < 3)
                {
                    System.Console.WriteLine(
                        "Regular ice cream flavours: Chocolate, Vanilla, Strawberry"
                    );
                    System.Console.WriteLine("Premium ice cream flavours: Ube, Sea salt, Durian");
                    System.Console.WriteLine("Enter flavour: ");
                    flavour = Console.ReadLine().ToLower();

                    if (string.IsNullOrEmpty(flavour) && number < 1)
                    {
                        continue;
                    }
                    else if (string.IsNullOrEmpty(flavour) && number >= 1)
                    {
                        System.Console.WriteLine(
                            "Are you sure you have finished choosing your flavour?(Y/N): "
                        );
                        string yn = Console.ReadLine().ToLower();

                        if (yn == "y")
                        {
                            break;
                        }
                        else
                        {
                            number -= 1;
                            continue;
                        }
                    }

                    try
                    {
                        System.Console.WriteLine("Enter number of scoops: ");
                        scoops = Convert.ToInt32(Console.ReadLine());
                        if (scoops < 1)
                        {
                            throw new InvalidScoopsException();
                        }
                    }
                    catch (FormatException)
                    {
                        System.Console.WriteLine(
                            "Scoops must not be null, there must be at least one scoop"
                        );
                        continue;
                    }
                    catch (InvalidOptionException b)
                    {
                        Console.WriteLine($"InvalidOptionException: {b.Message}");
                        continue;
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("Something went wrong");
                        continue;
                    }

                    if (flavour == "durian" || flavour == "ube" || flavour == "sea salt")
                    {
                        Flavour newflavour = new Flavour(flavour, true, scoops);
                        FlavourList.Add(newflavour);
                    }
                    else if (
                        flavour == "vanilla"
                        || flavour == "chocolate"
                        || flavour == "strawberry"
                    )
                    {
                        Flavour newflavour = new Flavour(flavour, false, scoops);
                        FlavourList.Add(newflavour);
                    }

                    number += 1;
                }

                while (number2 < 4)
                {
                    System.Console.WriteLine("Available toppings: sprinkles, mochi, sago, oreos");
                    System.Console.WriteLine("Enter topping: ");
                    topping = Console.ReadLine().ToLower();

                    Topping newTopping = new Topping(topping);
                    ToppingList.Add(newTopping);

                    if (string.IsNullOrEmpty(topping))
                    {
                        string input2 = "";
                        if (number2 < 1)
                        {
                            System.Console.WriteLine(
                                "Are you sure you don't want any topping?(y/n): "
                            );
                            input2 = Console.ReadLine().ToLower();
                        }
                        else if (number2 >= 1)
                        {
                            System.Console.WriteLine(
                                "Are you sure you don't want anymore toppings?(y/n): "
                            );
                            input2 = Console.ReadLine().ToLower();
                        }

                        if (input2 == "y")
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    number2 += 1;
                }

                IceCream newIceCream = new Cup(option, scoops, FlavourList, ToppingList);
                IceCreamList.Add(newIceCream);
                selectedCustomer.CurrentOrder.AddIceCream(newIceCream);
            }
            else if (option == "cone")
            {
                string topping = "";
                int number2 = 0;
                int number = 0;
                string flavour = "";
                int scoops = 0;
                bool dipped = false;
                string input = "";
                while (number < 3)
                {
                    System.Console.WriteLine(
                        "Regular ice cream flavours: Chocolate, Vanilla, Strawberry"
                    );
                    System.Console.WriteLine("Premium ice cream flavours: Ube, Sea salt, Durian");
                    System.Console.WriteLine("Enter flavour: ");
                    flavour = Console.ReadLine().ToLower();

                    if (string.IsNullOrEmpty(flavour) && number < 1)
                    {
                        continue;
                    }
                    else if (string.IsNullOrEmpty(flavour) && number >= 1)
                    {
                        System.Console.WriteLine(
                            "Are you sure you have finished choosing your flavour?(Y/N): "
                        );
                        string yn = Console.ReadLine().ToLower();

                        if (yn == "y")
                        {
                            break;
                        }
                        else
                        {
                            number -= 1;
                            continue;
                        }
                    }

                    try
                    {
                        System.Console.WriteLine("Enter number of scoops: ");
                        scoops = Convert.ToInt32(Console.ReadLine());

                        System.Console.WriteLine("Dip the cone in chocolate?(y/n) :");
                        input = Console.ReadLine().ToLower();

                        if (scoops < 1)
                        {
                            throw new InvalidScoopsException();
                        }
                    }
                    catch (FormatException)
                    {
                        System.Console.WriteLine(
                            "Scoops must not be null, there must be at least one scoop"
                        );
                        continue;
                    }
                    catch (InvalidOptionException b)
                    {
                        Console.WriteLine($"InvalidOptionException: {b.Message}");
                        continue;
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("Something went wrong");
                        continue;
                    }

                    if (input == "y")
                    {
                        dipped = true;
                    }
                    else
                    {
                        dipped = false;
                    }

                    if (flavour == "durian" || flavour == "ube" || flavour == "sea salt")
                    {
                        Flavour newflavour = new Flavour(flavour, true, scoops);
                        FlavourList.Add(newflavour);
                    }
                    else if (
                        flavour == "vanilla"
                        || flavour == "chocolate"
                        || flavour == "strawberry"
                    )
                    {
                        Flavour newflavour = new Flavour(flavour, false, scoops);
                        FlavourList.Add(newflavour);
                    }

                    number += 1;
                }

                while (number2 < 4)
                {
                    System.Console.WriteLine("Available toppings: sprinkles, mochi, sago, oreos");
                    System.Console.WriteLine("Enter topping: ");
                    topping = Console.ReadLine().ToLower();

                    Topping newTopping = new Topping(topping);
                    ToppingList.Add(newTopping);

                    if (string.IsNullOrEmpty(topping))
                    {
                        string input2 = "";
                        if (number2 < 1)
                        {
                            System.Console.WriteLine(
                                "Are you sure you don't want any topping?(y/n): "
                            );
                            input2 = Console.ReadLine().ToLower();
                        }
                        else if (number2 >= 1)
                        {
                            System.Console.WriteLine(
                                "Are you sure you don't want anymore toppings?(y/n): "
                            );
                            input2 = Console.ReadLine().ToLower();
                        }

                        if (input2 == "y")
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    number2 += 1;
                }

                IceCream newIceCream = new Cone(option, scoops, FlavourList, ToppingList, dipped);
                IceCreamList.Add(newIceCream);
                selectedCustomer.CurrentOrder.AddIceCream(newIceCream);
            }
            else if (option == "waffle")
            {
                int number2 = 0;
                int number = 0;
                string flavour = "";
                string waffleflavour = "";
                int scoops = 0;
                bool dipped = false;
                string topping = "";
                while (number < 3)
                {
                    System.Console.WriteLine(
                        "Regular ice cream flavours: Chocolate, Vanilla, Strawberry"
                    );
                    System.Console.WriteLine("Premium ice cream flavours: Ube, Sea salt, Durian");
                    System.Console.WriteLine("Enter flavour: ");
                    flavour = Console.ReadLine().ToLower();

                    if (string.IsNullOrEmpty(flavour) && number < 1)
                    {
                        continue;
                    }
                    else if (string.IsNullOrEmpty(flavour) && number >= 1)
                    {
                        System.Console.WriteLine(
                            "Are you sure you have finished choosing your flavour?(Y/N): "
                        );
                        string yn = Console.ReadLine().ToLower();

                        if (yn == "y")
                        {
                            break;
                        }
                        else
                        {
                            number -= 1;
                            continue;
                        }
                    }

                    try
                    {
                        System.Console.WriteLine("Enter number of scoops: ");
                        scoops = Convert.ToInt32(Console.ReadLine());

                        System.Console.WriteLine(
                            "Available waffle flavours: Original, red velvet, pandan, charcoal"
                        );
                        System.Console.WriteLine("Enter waffle flavour :");
                        waffleflavour = Console.ReadLine();

                        if (scoops < 1)
                        {
                            throw new InvalidScoopsException();
                        }
                    }
                    catch (FormatException)
                    {
                        System.Console.WriteLine(
                            "Scoops must not be null, there must be at least one scoop"
                        );
                        continue;
                    }
                    catch (InvalidOptionException b)
                    {
                        Console.WriteLine($"InvalidOptionException: {b.Message}");
                        continue;
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("Something went wrong");
                        continue;
                    }

                    if (flavour == "durian" || flavour == "ube" || flavour == "sea salt")
                    {
                        Flavour newflavour = new Flavour(flavour, true, scoops);
                        FlavourList.Add(newflavour);
                    }
                    else if (
                        flavour == "vanilla"
                        || flavour == "chocolate"
                        || flavour == "strawberry"
                    )
                    {
                        Flavour newflavour = new Flavour(flavour, false, scoops);
                        FlavourList.Add(newflavour);
                    }

                    number += 1;
                }

                while (number2 < 4)
                {
                    System.Console.WriteLine("Available toppings: sprinkles, mochi, sago, oreos");
                    System.Console.WriteLine("Enter topping: ");
                    topping = Console.ReadLine().ToLower();

                    Topping newTopping = new Topping(topping);
                    ToppingList.Add(newTopping);

                    if (string.IsNullOrEmpty(topping))
                    {
                        string input2 = "";
                        if (number2 < 1)
                        {
                            System.Console.WriteLine(
                                "Are you sure you don't want any topping?(y/n): "
                            );
                            input2 = Console.ReadLine().ToLower();
                        }
                        else if (number2 >= 1)
                        {
                            System.Console.WriteLine(
                                "Are you sure you don't want anymore toppings?(y/n): "
                            );
                            input2 = Console.ReadLine().ToLower();
                        }

                        if (input2 == "y")
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    number2 += 1;
                }

                IceCream newIceCream = new Waffle(
                    option,
                    scoops,
                    FlavourList,
                    ToppingList,
                    waffleflavour
                );
                IceCreamList.Add(newIceCream);
                selectedCustomer.CurrentOrder.AddIceCream(newIceCream);
            }

            System.Console.WriteLine("Do you want to make another order?(y/n): ");
            string answer = Console.ReadLine();

            if (answer == "y")
            {
                continue;
            }
            else
            {
                break;
            }
        }

        if (selectedCustomer.rewards != null && selectedCustomer.rewards.Tier == "Gold")
        {
            goldOrderQueue.Enqueue(newOrder);
        }
        else
        {
            regularOrderQueue.Enqueue(newOrder);
        }

        System.Console.WriteLine("Order created successfully");
    }

    static void DisplayCharged(
        List<Flavour> FlavourList,
        List<Topping> ToppingList,
        List<IceCream> IceCreamList
    )
    {
        DateTime TimeFulfilled = DateTime.MinValue;
        string Option = "";
        int Scoops = 0;
        string Topping = "";
        string Flavour = "";
        int year = 0;
        bool dipped = false;
        string waffleFlavour = "";
        double price = 0.0;
        double JanPrice = 0.0;
        double FebPrice = 0.0;
        double MarPrice = 0.0;
        double AprPrice = 0.0;
        double MayPrice = 0.0;
        double JunePrice = 0.0;
        double JulyPrice = 0.0;
        double AugPrice = 0.0;
        double SepPrice = 0.0;
        double OctPrice = 0.0;
        double NovPrice = 0.0;
        double DecPrice = 0.0;
        double totalPrice = 0.0;
        int MonthOnly = 0;
        string Month = "";

        string filePath = "orders.csv";
        string[] lines = File.ReadAllLines(filePath);

        while (true)
        {
            try
            {
                System.Console.WriteLine("Enter the year: ");
                year = Convert.ToInt32(Console.ReadLine());

                if (year < 2013 || year > 2034)
                {
                    throw new InvalidYearException();
                }

                break;
            }
            catch (FormatException)
            {
                System.Console.WriteLine(
                    "FormatException a occurred, please do not enter a string, only enter a integer between 0 and 6 inclusive."
                );
                continue;
            }
            catch (InvalidOptionException a)
            {
                Console.WriteLine($"InvalidYearException: {a.Message}");
                continue;
            }
            catch (Exception)
            {
                System.Console.WriteLine("Something went wrong!");
                continue;
            }
        }

        for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
        {
            FlavourList.Clear();
            ToppingList.Clear();

            string line = lines[lineIndex];

            string[] data = line.Split(',');

            string option = data[4].Trim().ToLower();

            TimeFulfilled = Convert.ToDateTime(data[3].Trim());
            MonthOnly = Convert.ToDateTime(data[3].Trim()).Month;
            int time = TimeFulfilled.Year;

            switch (MonthOnly)
            {
                case 1:
                    Month = "Jan";
                    break;
                case 2:
                    Month = "Feb";
                    break;
                case 3:
                    Month = "Mar";
                    break;
                case 4:
                    Month = "Apr";
                    break;
                case 5:
                    Month = "May";
                    break;
                case 6:
                    Month = "June";
                    break;
                case 7:
                    Month = "July";
                    break;
                case 8:
                    Month = "Aug";
                    break;
                case 9:
                    Month = "Sep";
                    break;
                case 10:
                    Month = "Oct";
                    break;
                case 11:
                    Month = "Nov";
                    break;
                case 12:
                    Month = "Dec";
                    break;
            }
            if (time != year)
            {
                continue;
            }
            else
            {
                FlavourList.Clear();
                ToppingList.Clear();

                if (option == "cup")
                {
                    Scoops = Convert.ToInt32(data[5].Trim());

                    for (int i = 8; i <= 10; i++)
                    {
                        string flavour = data[i].Trim().ToLower();
                        if (!string.IsNullOrEmpty(flavour))
                        {
                            if (flavour == "durian" || flavour == "ube" || flavour == "sea salt")
                            {
                                Flavour newflavour = new Flavour(flavour, true, Scoops);
                                FlavourList.Add(newflavour);
                            }
                            else if (
                                flavour == "vanilla"
                                || flavour == "chocolate"
                                || flavour == "strawberry"
                            )
                            {
                                Flavour newflavour = new Flavour(flavour, false, Scoops);
                                FlavourList.Add(newflavour);
                            }
                        }
                    }

                    for (int i = 11; i <= 14; i++)
                    {
                        string topping = data[i].Trim();
                        if (!string.IsNullOrEmpty(topping))
                        {
                            Topping newTopping = new Topping(topping);
                            ToppingList.Add(newTopping);
                        }
                    }
                    IceCream newIceCream1 = new Cup(option, Scoops, FlavourList, ToppingList);
                    price = newIceCream1.CalculatePrice();
                }
                else if (option == "cone")
                {
                    dipped = Convert.ToBoolean(data[6].Trim().ToLower());
                    Scoops = Convert.ToInt32(data[5].Trim());

                    for (int i = 8; i <= 10; i++)
                    {
                        string flavour = data[i].Trim().ToLower();
                        if (!string.IsNullOrEmpty(flavour))
                        {
                            if (flavour == "durian" || flavour == "ube" || flavour == "sea salt")
                            {
                                Flavour newflavour = new Flavour(flavour, true, Scoops);
                                FlavourList.Add(newflavour);
                            }
                            else if (
                                flavour == "vanilla"
                                || flavour == "chocolate"
                                || flavour == "strawberry"
                            )
                            {
                                Flavour newflavour = new Flavour(flavour, false, Scoops);
                                FlavourList.Add(newflavour);
                            }
                        }
                    }

                    for (int i = 11; i <= 14; i++)
                    {
                        string topping = data[i].Trim();
                        if (!string.IsNullOrEmpty(topping))
                        {
                            Topping newTopping = new Topping(topping);
                            ToppingList.Add(newTopping);
                        }
                    }

                    IceCream newIceCream1 = new Cone(
                        option,
                        Scoops,
                        FlavourList,
                        ToppingList,
                        dipped
                    );
                    price = newIceCream1.CalculatePrice();
                }
                else if (option == "waffle")
                {
                    Scoops = Convert.ToInt32(data[5].Trim());
                    waffleFlavour = data[7].Trim();

                    for (int i = 8; i <= 10; i++)
                    {
                        string flavour = data[i].Trim().ToLower();
                        if (!string.IsNullOrEmpty(flavour))
                        {
                            if (flavour == "durian" || flavour == "ube" || flavour == "sea salt")
                            {
                                Flavour newflavour = new Flavour(flavour, true, Scoops);
                                FlavourList.Add(newflavour);
                            }
                            else if (
                                flavour == "vanilla"
                                || flavour == "chocolate"
                                || flavour == "strawberry"
                            )
                            {
                                Flavour newflavour = new Flavour(flavour, false, Scoops);
                                FlavourList.Add(newflavour);
                            }
                        }
                    }

                    for (int i = 11; i <= 14; i++)
                    {
                        string topping = data[i].Trim();
                        if (!string.IsNullOrEmpty(topping))
                        {
                            Topping newTopping = new Topping(topping);
                            ToppingList.Add(newTopping);
                        }
                    }

                    IceCream newIceCream1 = new Waffle(
                        option,
                        Scoops,
                        FlavourList,
                        ToppingList,
                        waffleFlavour
                    );
                    price = newIceCream1.CalculatePrice();
                }
            }

            switch (Month)
            {
                case "Jan":
                    JanPrice += price;
                    break;
                case "Feb":
                    FebPrice += price;
                    break;
                case "Mar":
                    MarPrice += price;
                    break;
                case "Apr":
                    AprPrice += price;
                    break;
                case "May":
                    MayPrice += price;
                    break;
                case "June":
                    JunePrice += price;
                    break;
                case "July":
                    JulyPrice += price;
                    break;
                case "Aug":
                    AugPrice += price;
                    break;
                case "Sep":
                    SepPrice += price;
                    break;
                case "Oct":
                    OctPrice += price;
                    break;
                case "Nov":
                    NovPrice += price;
                    break;
                case "Dec":
                    DecPrice += price;
                    break;
            }
            totalPrice += price;
        }

        for (int i = 1; i <= 12; i++)
        {
            switch (i)
            {
                case 1:
                    System.Console.WriteLine($"Jan {year}: ${JanPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 2:
                    System.Console.WriteLine($"Feb {year}: ${FebPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 3:
                    System.Console.WriteLine($"March {year}: ${MarPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 4:
                    System.Console.WriteLine($"Apr {year}: ${AprPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 5:
                    System.Console.WriteLine($"May {year}: ${MayPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 6:
                    System.Console.WriteLine($"June {year}: ${JunePrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 7:
                    System.Console.WriteLine($"July {year}: ${JulyPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 8:
                    System.Console.WriteLine($"Aug {year}: ${AugPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 9:
                    System.Console.WriteLine($"Sep {year}: ${SepPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 10:
                    System.Console.WriteLine($"Oct {year}: ${OctPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 11:
                    System.Console.WriteLine($"Nov {year}: ${NovPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
                case 12:
                    System.Console.WriteLine($"Dec {year}: ${DecPrice.ToString("F2")}");
                    System.Console.WriteLine();
                    break;
            }
        }
        System.Console.WriteLine($"Total: {totalPrice.ToString("F2")}");
    }

    static void Menu()
    {
        System.Console.WriteLine("------------------Menu------------------");
        System.Console.WriteLine("[1] List all customers");
        System.Console.WriteLine("[2] List all current orders");
        System.Console.WriteLine("[3] Register a new Customer");
        System.Console.WriteLine("[4] Create a customer's order");
        System.Console.WriteLine("[5] Display order detail of a customer");
        System.Console.WriteLine("[6] Modify Order details");
        System.Console.WriteLine("[7] Checkout");
        System.Console.WriteLine("[8] Display Charged");
        System.Console.WriteLine("[0] Exit");
        System.Console.WriteLine("----------------------------------------");
    }

    public static void Main(string[] args)
    {
        List<Customer> CustomerList = new List<Customer>();
        List<PointCard> PointCardsList = new List<PointCard>();
        List<Flavour> FlavourList = new List<Flavour>();
        List<Topping> ToppingList = new List<Topping>();
        List<IceCream> IceCreamList = new List<IceCream>();
        Queue<Order> regularOrderQueue = new Queue<Order>();
        Queue<Order> goldOrderQueue = new Queue<Order>();

        bool loop = true;
        while (loop)
        {
            CustomerList.Clear();
            PointCardsList.Clear();
            FlavourList.Clear();
            ToppingList.Clear();
            IceCreamList.Clear();
            Menu();
            int option = 0;

            try
            {
                System.Console.WriteLine("Enter option: ");
                option = Convert.ToInt32(Console.ReadLine());

                if (option < 0 || option > 8)
                {
                    throw new InvalidOptionException();
                }
            }
            catch (FormatException a)
            {
                System.Console.WriteLine(
                    "FormatException a occurred, please do not enter a string, only enter a integer between 0 and 6 inclusive."
                );
                continue;
            }
            catch (InvalidOptionException b)
            {
                Console.WriteLine($"InvalidOptionException: {b.Message}");
                continue;
            }
            catch (ArgumentNullException c)
            {
                System.Console.WriteLine("The option cannot be null");
                continue;
            }
            catch (Exception d)
            {
                System.Console.WriteLine("Something went wrong!");
                continue;
            }

            switch (option)
            {
                case 1:
                    readCustomerData(CustomerList, PointCardsList);
                    break;
                case 2:
                    break;
                case 3:
                    RegisterNewCustomer(CustomerList, PointCardsList);
                    break;
                case 4:
                    CreateOrder(
                        CustomerList,
                        PointCardsList,
                        FlavourList,
                        ToppingList,
                        IceCreamList,
                        regularOrderQueue,
                        goldOrderQueue
                    );
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    DisplayCharged(FlavourList, ToppingList, IceCreamList);
                    break;
                case 0:
                    loop = false;
                    break;
            }
        }
    }
}

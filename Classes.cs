using System.Collections.Generic;
using System;
using System.Drawing;
using System.Reflection.Emit;

namespace Class
{
    class Order
    {
        public int id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }

        public Order(int id, DateTime tr, DateTime? tf, List<IceCream> ic)
        {
            id = id;
            TimeReceived = tr;
            TimeFulfilled = tf;
            IceCreamList = ic;
        }

        public ModifyIceCream(int index, IceCream iceCream)
        {
            if (index >= 0 && index < IceCreamList.Count)
        {
            IceCreamList[index] = iceCream;
        }
        }

        public AddIceCream(IceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public DeleteIceCream(int id)
        {
             if (index >= 0 && index < IceCreamList.Count)
             {
            IceCreamList.RemoveAt(index);
            }
        }

        public double CalculateTotal()
        {
        double total = 0;
        foreach (var iceCream in IceCreamList)
        {
            total += iceCream.CalculatePrice();
        }
        return total;
        }

        public override string ToString()
        {
            return $"Order id: {id}, Time received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}, ice creams: {IceCreamList}";
        }
    }

    class Customer
    {
        public string name { get; set; }
        public int MemberID { get; set; }
        public DateTime DOB { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
        public PointCard rewards { get; set; }

        public Customer(string n, int mid, DateTime dob)
        {
            name = n;
            MemberID = mid;
            DOB = dob;
        }

        public Order MakeOrder()
        {
            return new Order();
        }

        public bool IsBirthday(DateTime dob)
        {
            return false;
        }

        public override string ToString()
        {
            return $"The customer name is {name}, his member id is {MemberID}, his date of birth is {DOB}";
        }
    }

    class PointCard
    {
        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        public PointCard(int ps, int pc, string tier)
        {
            Points = ps;
            PunchCard = pc;
            Tier = tier;
        }

        public void AddPoints(int points)
        {
            Points += points;
        }

        public void RedeemPoints(int points)
        {
            Points -= points;
        }

        public punch()
        {
            PunchCard++;
        }

        public override string ToString()
        {
            return $"Number of points {Points}, PunchCard number: {PunchCard}, PointCard tier: {Tier}";
        }
    }

    class Flavour
    {
        public string type { get; set; }
        public Boolean premium { get; set; }
        public int quantity { get; set; }

        public Flavour(string t, bool p, int q)
        {
            type = t;
            premium = p;
            quantity = q;
        }

        public override string ToString()
        {
            return $"Ice cream flavour type = {type}, premium = {premium}, quantity = {quantity}";
        }
    }

    class Topping
    {
        public string type { get; set; }

        public Topping(string t)
        {
            type = t;
        }

        public override string ToString()
        {
            return $"The chosen toppings are {type}";
        }
    }

    abstract class IceCream
    {
        public string option { get; set; }
        public int scoops { get; set; }
        public List<Flavour> flavours { get; set; }

        public List<Topping> toppings { get; set; }

        public IceCream(string o, int s, List<Flavour> f, List<Topping> t)
        {
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
        }

        public abstract double CalculatePrice();

        public override string ToString()
        {
            return $"The IceCream cone is: {option}, number of scoops = {scoops}, flavours = {flavours}, toppings = {toppings}";
        }
    }

    class Cup : IceCream
    {
        public Cup(string o, int s, List<Flavour> f, List<Topping> t)
            : base(o, s, f, t)
        {
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
        }

        public override double CalculatePrice()
        {
            double totalPrice = 0.0;

            foreach (Flavour flavor in flavours)
            {
                if (flavor.premium)
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price =
                        4.00
                        + (1.50 * scoopsAsDouble)
                        + (2.00 * scoopsAsDouble)
                        + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for {flavor.type} with {scoops} scoops and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
                else
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for premium {flavor.type} with {scoops} scoops and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
            }

            return totalPrice;
        }

        public override string ToString()
        {
            string baseToString = base.ToString();

            double totalPrice = CalculatePrice();

            return $"{baseToString} Price: {totalPrice}";
        }
    }

    class Cone : IceCream
    {
        public bool dipped { get; set; }

        public Cone(string o, int s, List<Flavour> f, List<Topping> t, bool d)
            : base(o, s, f, t)
        {
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
            dipped = d;
        }

        public override double CalculatePrice()
        {
            double totalPrice = 0.0;

            foreach (Flavour flavor in flavours)
            {
                if (flavor.premium && dipped == true)
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price =
                        (4.00 + (1.50 * scoopsAsDouble) + (2.00 * scoopsAsDouble))
                        + 2.00
                        + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for premium {flavor.type} with {scoops} scoops and chocolate dipped cone and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
                else if (flavor.premium && dipped == false)
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price =
                        4.00
                        + (1.50 * scoopsAsDouble)
                        + (2.00 * scoopsAsDouble)
                        + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for {flavor.type} with {scoops} scoops and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
                else if (!flavor.premium && dipped == true)
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + 2.00 + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for {flavor.type} with {scoops} scoops and chocolate dipped cone and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
                else
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for {flavor.type} with {scoops} scoops and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
            }
            return totalPrice;
        }

        public override string ToString()
        {
            string baseToString = base.ToString();

            double totalPrice = CalculatePrice();

            return $"{baseToString}, Chocolate dipped cone: {dipped}, Price: {totalPrice}";
        }
    }

    class Waffle : IceCream
    {
        public string waffleFlavour { get; set; }

        public Waffle(string o, int s, List<Flavour> f, List<Topping> t, string w)
            : base(o, s, f, t)
        {
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
            waffleFlavour = w;
        }

        public override double CalculatePrice()
        {
            double totalPrice = 0.0;

            foreach (Flavour flavor in flavours)
            {
                if (
                    flavor.premium && waffleFlavour == "Red Velvet"
                    || waffleFlavour == "Charcoal"
                    || waffleFlavour == "Pandan"
                )
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price =
                        (4.00 + (1.50 * scoopsAsDouble) + (2.00 * scoopsAsDouble))
                        + 3.00
                        + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for premium {flavor.type} with {scoops} scoops and the waffle flavour {waffleFlavour} and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
                else if (flavor.premium && waffleFlavour == "Original")
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price =
                        4.00
                        + (1.50 * scoopsAsDouble)
                        + (2.00 * scoopsAsDouble)
                        + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for {flavor.type} with {scoops} scoops and the waffle flavour {waffleFlavour} and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
                else if (
                    !flavor.premium && waffleFlavour == "Red Velvet"
                    || waffleFlavour == "Charcoal"
                    || waffleFlavour == "Pandan"
                )
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + 3.00 + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for {flavor.type} with {scoops} scoops and the waffle flavour {waffleFlavour} and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
                else
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + (toppings.Count * 1.00);
                    Console.WriteLine(
                        $"Price for {flavor.type} with {scoops} scoops and the waffle flavour {waffleFlavour} and these toppings{toppings}: {price.ToString("F2")}"
                    );
                    totalPrice += price;
                }
            }
            return totalPrice;
        }

        public override string ToString()
        {
            string baseToString = base.ToString();

            double totalPrice = CalculatePrice();

            return $"{baseToString}, Waffle flavour : {waffleFlavour} Price: {totalPrice}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.Emit;

namespace Classes
{
    class Order
    {
        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }

        public Order(int id, DateTime tr)
        {
            Id = id;
            TimeReceived = tr;
        }

        public void ModifyIceCream(int index, IceCream iceCream)
        {
            if (index >= 0 && index < IceCreamList.Count)
            {
                IceCreamList[index] = iceCream;
            }
        }

        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int index)
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
            return $"Order id: {Id}, Time received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}, ice creams: {IceCreamList}";
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

        public Order MakeOrder(int Id, DateTime timeReceived)
        {
            System.Console.WriteLine("Creating new Order");

            Order newOrder = new Order(Id, timeReceived);

            return newOrder;
        }

        public bool IsBirthday(DateTime dob)
        {
            return false;
        }

        public override string ToString()
        {
            return $"Name: {name}, MemberID: {MemberID}, Date of birth: {DOB}";
        }
    }

    class PointCard
    {
        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        public PointCard(int ps, int pc)
        {
            Points = ps;
            PunchCard = pc;
        }

        public void AddPoints(int points)
        {
            Points += points;
        }

        public void RedeemPoints(int points)
        {
            Points -= points;
        }

        public void punch()
        {
            PunchCard++;
        }

        public override string ToString()
        {
            return $"Tier: {Tier},  No. of points: {Points}, PunchCard: {PunchCard}";
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
            double basePrice = 4.00;
            double scoopsAsDouble = Convert.ToDouble(scoops);
            double price = 0.00;

            foreach (Flavour flavor in flavours)
            {
                if (flavor.premium)
                {
                    basePrice += 2.00;
                }
            }

            if (scoops == 1)
            {
                price = basePrice + (toppings.Count * 1.00);
            }
            else if (scoops == 2)
            {
                price = basePrice + ((1.50 * scoopsAsDouble) - 1.50) + (toppings.Count * 1.00);
            }
            else
            {
                price = basePrice + ((1.50 * scoopsAsDouble) - 2.00) + (toppings.Count * 1.00);
            }

            return Convert.ToDouble(price.ToString("F2"));
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
            double basePrice = 0.00;
            double scoopsAsDouble = Convert.ToDouble(scoops);
            double price = 0.00;

            if (dipped)
            {
                basePrice = 6.00;
            }
            else
            {
                basePrice = 4.00;
            }

            foreach (Flavour flavor in flavours)
            {
                if (flavor.premium)
                {
                    basePrice += 2.00;
                }
            }

            if (scoops == 1)
            {
                price = basePrice + (toppings.Count * 1.00);
            }
            else if (scoops == 2)
            {
                price = basePrice + ((1.50 * scoopsAsDouble) - 1.50) + (toppings.Count * 1.00);
            }
            else
            {
                price = basePrice + ((1.50 * scoopsAsDouble) - 2.00) + (toppings.Count * 1.00);
            }

            return Convert.ToDouble(price.ToString("F2"));
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
            double basePrice = 0.00;
            double scoopsAsDouble = Convert.ToDouble(scoops);
            double price = 0.00;

            if (
                waffleFlavour.ToLower() == "red velvet"
                || waffleFlavour.ToLower() == "charcoal"
                || waffleFlavour.ToLower() == "pandan"
            )
            {
                basePrice = 10.00;
            }
            else
            {
                basePrice = 7.00;
            }

            foreach (Flavour flavor in flavours)
            {
                if (flavor.premium)
                {
                    basePrice += 2.00;
                }
            }

            if (scoops == 1)
            {
                price = basePrice + (toppings.Count * 1.00);
            }
            else if (scoops == 2)
            {
                price = basePrice + ((1.50 * scoopsAsDouble) - 1.50) + (toppings.Count * 1.00);
            }
            else
            {
                price = basePrice + ((1.50 * scoopsAsDouble) - 2.00) + (toppings.Count * 1.00);
            }

            return Convert.ToDouble(price.ToString("F2"));
        }

        public override string ToString()
        {
            string baseToString = base.ToString();

            double totalPrice = CalculatePrice();

            return $"{baseToString}, Waffle flavour : {waffleFlavour} Price: {totalPrice}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pech
{
    interface Transoprt {
        
        int ReturnPrice();
    }
     abstract class DeliveryPrice
     {  
       // int CountPrice { get; }
        public abstract List<Transoprt>  CreateTransport(int CargoSize);
     }


    class DeliveryPriceTrain : DeliveryPrice
    {
        public List<Train> wagons { get; private  set; } = new List<Train>();
        public List<Locomotive> locomotives { get; private set; } = new List<Locomotive>();
        public override List<Transoprt> CreateTransport(int CargoSize)
        {
            if (CargoSize <= 0) return null;
            
            int amountWagons = CargoSize % Train.Size == 0 ? CargoSize / Train.Size : CargoSize / Train.Size + 1;
            wagons.AddRange(new Train[amountWagons]);
                    
            int amountLocom = wagons.Count % Locomotive.SizeOfWagons == 0 ? 
                wagons.Count / Locomotive.SizeOfWagons : wagons.Count / Locomotive.SizeOfWagons + 1;
            locomotives.AddRange(new Locomotive[amountLocom]);

            List<Transoprt> output = new List<Transoprt>();
            output.AddRange(wagons);
            output.AddRange(locomotives);
            return output;
            
        }
    }
    class DeliveryPriceCar : DeliveryPrice
    {
        List<Car> cars = new List<Car>();
        public override List<Transoprt> CreateTransport(int CargoSize)
        {
            if (CargoSize <= 0) return null;

            int amountCars = (CargoSize % Car.Size) == 0 ? CargoSize / Car.Size : CargoSize / Car.Size + 1;// нужна ли
            cars.AddRange(new Car[amountCars]);                                                              // доп. машина

            List<Transoprt> output = new List<Transoprt>();
            output.AddRange(cars);
            return output;
        }
    }


    class Train : Transoprt
    {
        public static int Size { get; } = 60;// кол - во товара
        public int ReturnPrice()
        {
            return 20;
        }
    }
    class Locomotive : Transoprt
    {
        public static int SizeOfWagons { get; } = 10;// кол-во вагонов, которые можно присоединить
        public int ReturnPrice()
        {
            return 15;
        }
    }
    class Car : Transoprt
    {
        public static int Size { get; } = 15;// кол - во товара
        public int ReturnPrice()
        {
            return 10;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DeliveryPrice obj = new DeliveryPriceTrain();
            Console.WriteLine((obj.CreateTransport(1)).Count);
            Console.WriteLine();
            Console.WriteLine( ((DeliveryPriceTrain)obj).locomotives.Count);
            Console.ReadLine();
        }
    }
    

}

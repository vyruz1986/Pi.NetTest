using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System;
using System.Device.Gpio;
using System.Collections.Generic;

namespace Pi.NetTest
{
   class Program
   {
      private static GpioController _io;
      static void Main(string[] args)
      {
         Console.WriteLine("Hello World!");
         Console.WriteLine($"Running in {RuntimeInformation.OSDescription}");

         _io = new GpioController();
         _io.OpenPin(17, PinMode.Output);
         _io.OpenPin(18, PinMode.Output);

         var tasks = new List<Task>();

         tasks.Add(Task.Factory.StartNew(() => blinkLed(17)));
         tasks.Add(Task.Factory.StartNew(() => blinkLed(18)));

         Task.WaitAll(tasks.ToArray());

      }

      static void blinkLed(int pin, int onTime = 1000, int offTime = 200)
      {
         while (true)
         {
            _io.Write(pin, PinValue.High);
            Thread.Sleep(onTime);
            _io.Write(pin, PinValue.Low);
            Thread.Sleep(offTime);
         }
      }
   }
}

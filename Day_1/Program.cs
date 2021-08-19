using System;

namespace OOP_revision
{ 
    // generics
    class DataStores<T>
    {
        public T[] data = new T[10];

        public void AddOrUpdate(int index, T item)
        {
            if(index >=0 && index <10)
            {
                data[index] = item;
            }
        }

        public T GetData(int index)
        {
            if (index >= 0 && index < 10)
                return data[index]; 
            else
                return default(T);
        }
    }
    // interface
    interface ISound
    {
        bool IsSound();
    }

    class Wavetable : ISound
    {
        public bool IsSound() => true;
        public virtual void SoundDescription()
        {
            Console.WriteLine("A wavetable is digital representation of a sound.\n");
        }
    }
     // abstract class
    abstract class Sine : Wavetable
    {
        protected int Variance;
        public abstract void PlayVariance();
        public abstract void VarianceCharacteristics();
        public override void SoundDescription()
        {
            Console.WriteLine("All of the Sine variances are high-pitched.\n");
        }
    }
 
    class SawSine : Sine
    {
        public SawSine(int InputVariance) => Variance = InputVariance;
        public int Length
        {
            get => Length;
            set => Length = value;
        }

        public override void PlayVariance()
        {
            Console.WriteLine("The sound goes brrr.\n");
        }
        public override void VarianceCharacteristics()
        {
            string toWrite = "";
            if(Variance == 1)
            {
                toWrite += "This Saw-Sine variance is buzzy.\n"; 
            }
            else if (Variance == 2)
            {
                toWrite += "This Saw-Sine variance is squeaky.\n";
            }
            else
            {
                toWrite += "Invalid Saw-Sine variance type.\n";
            }
            Console.WriteLine(toWrite);
        }
    }


    class SquareSine : Sine
    {
        public SquareSine(int InputVariance) => Variance = InputVariance;

        public override void PlayVariance()
        {
            Console.WriteLine("The sound goes prrr.\n");
        }
        public override void VarianceCharacteristics()
        {
            string toWrite = "";
            if (Variance == 3)
            {
                toWrite += "This Square-Sine variance is robotic.\n";
            }
            else if (Variance == 4)
            {
                toWrite += "This Square-Sine variance is sleek.\n";
            }
            else
            {
                toWrite += "Invalid Square-Sine variance type.\n";
            }
            Console.WriteLine(toWrite);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var SoundList = new DataStores<Sine>();

            Wavetable MyWave = new Wavetable();
            SawSine MySaw = new SawSine(2);
            SquareSine MySquare = new SquareSine(5);

            MyWave.SoundDescription();

            SoundList.AddOrUpdate(0, MySaw);
            SoundList.AddOrUpdate(1, MySquare);


            SoundList.GetData(0).SoundDescription();


            if (SoundList.GetData(0).IsSound())
            { Console.WriteLine("This saw is a sound.\n"); }

            SoundList.GetData(1).VarianceCharacteristics();
            SoundList.GetData(1).PlayVariance();
        }
    }
}
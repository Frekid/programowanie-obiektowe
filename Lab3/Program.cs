using System;

namespace ComplexNumbersExample
{
    // 2. Interfejs IModular
    public interface IModular
    {
        double Module();
    }

    // 1. Klasa ComplexNumber
    // 3. Implementacja IModular
    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
    {
        // prywatne pola
        private double re; // część rzeczywista
        private double im; // część urojona

        // publiczne właściwości
        public double Re
        {
            get => re;
            set => re = value;
        }

        public double Im
        {
            get => im;
            set => im = value;
        }

        // konstruktor
        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        // ToString() 
        public override string ToString()
        {

            string sign = im >= 0 ? " + " : " - ";
            double absIm = Math.Abs(im);
            return $"{re}{sign}{absIm}i";
        }

        // Operator + (dodawanie liczb zespolonych)
        public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2)
        {
            return new ComplexNumber(z1.re + z2.re, z1.im + z2.im);
        }

        // Operator - (odejmowanie liczb zespolonych)
        public static ComplexNumber operator -(ComplexNumber z1, ComplexNumber z2)
        {
            return new ComplexNumber(z1.re - z2.re, z1.im - z2.im);
        }

        // Operator * (mnożenie liczb zespolonych)
        // (a+bi)(c+di) = (ac − bd) + (ad + bc)i
        public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
        {
            double a = z1.re;
            double b = z1.im;
            double c = z2.re;
            double d = z2.im;

            double real = a * c - b * d;
            double imag = a * d + b * c;

            return new ComplexNumber(real, imag);
        }

        // ICloneable: tworzenie kopii obiektu
        public object Clone()
        { 
            return new ComplexNumber(this.re, this.im);
        }

        // IModular: moduł liczby zespolonej
        // |Z| = sqrt(re^2 + im^2)
        public double Module()
        {
            return Math.Sqrt(re * re + im * im);
        }

        // IEquatable<ComplexNumber>
        public bool Equals(ComplexNumber? other)
        {
            if (ReferenceEquals(other, null))
                return false;

            
            return this.re == other.re && this.im == other.im;
        }

        // override Equals(object)
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is ComplexNumber other)
                return Equals(other);

            return false;
        }

        // GetHashCode
       
        public override int GetHashCode()
        {
            
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + re.GetHashCode();
                hash = hash * 23 + im.GetHashCode();
                return hash;
            }
        }

        // Operatory == i !=
        public static bool operator ==(ComplexNumber? z1, ComplexNumber? z2)
        {
            if (ReferenceEquals(z1, z2))
                return true;

            if (ReferenceEquals(z1, null) || ReferenceEquals(z2, null))
                return false;

            return z1.Equals(z2);
        }

        public static bool operator !=(ComplexNumber? z1, ComplexNumber? z2)
        {
            return !(z1 == z2);
        }

        // Unarny operator 
        public static ComplexNumber operator -(ComplexNumber z)
        {
            return new ComplexNumber(z.re, -z.im);
        }
    }

    // 4. Klasa Program z Main()
    class Program
    {
        public static void Main(string[] args)
        {
            ComplexNumber z1 = new ComplexNumber(8, 9);  
            ComplexNumber z2 = new ComplexNumber(-5, 2);  

            Console.WriteLine("Liczby:");
            Console.WriteLine($"z1 = {z1}");
            Console.WriteLine($"z2 = {z2}");
            Console.WriteLine();

            // Dodawanie
            ComplexNumber sum = z1 + z2;
            Console.WriteLine($"z1 + z2 = {sum}");

            // Odejmowanie
            ComplexNumber diff = z1 - z2;
            Console.WriteLine($"z1 - z2 = {diff}");

            // Mnożenie
            ComplexNumber product = z1 * z2;
            Console.WriteLine($"z1 * z2 = {product}");

            // Sprzężenie (unarne -)
            ComplexNumber conjugate = -z1;
            Console.WriteLine($"Sprzężenie z1: -z1 = {conjugate}");

            // Moduł
            Console.WriteLine();
            Console.WriteLine($"|z1| = {z1.Module()}");
            Console.WriteLine($"|z2| = {z2.Module()}");

            // Clone
            Console.WriteLine();
            ComplexNumber clone = (ComplexNumber)z1.Clone();
            Console.WriteLine($"Kopia z1: {clone}");

            // Porównanie
            Console.WriteLine();
            ComplexNumber z3 = new ComplexNumber(3, 4);
            Console.WriteLine($"z1 == z3 ? {z1 == z3}");
            Console.WriteLine($"z1.Equals(z3) ? {z1.Equals(z3)}");
            Console.WriteLine($"z1 != z2 ? {z1 != z2}");

            Console.WriteLine();
            Console.WriteLine("Naciśnij Enter, aby zakończyć...");
            Console.ReadLine();
        }
    }
}


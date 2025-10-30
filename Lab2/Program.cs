using System;

// 1. Klasa Zwierze
public class Zwierze
{
    protected string nazwa;

    public Zwierze(string nazwa)
    {
        this.nazwa = nazwa;
    }

    public virtual void DajGlos()
    {
        Console.WriteLine("...");
    }
}

// 2. Klasa Pies
public class Pies : Zwierze
{
    public Pies(string nazwa) : base(nazwa) { }

    public override void DajGlos()
    {
        Console.WriteLine($"{nazwa} robi woof woof!");
    }
}

// 3. Klasa Kot
public class Kot : Zwierze
{
    public Kot(string nazwa) : base(nazwa) { }

    public override void DajGlos()
    {
        Console.WriteLine($"{nazwa} robi miau miau!");
    }
}

// 4. Klasa Waz
public class Waz : Zwierze
{
    public Waz(string nazwa) : base(nazwa) { }

    public override void DajGlos()
    {
        Console.WriteLine($"{nazwa} robi ssssssss!");
    }
}

// 6. Globalna metoda powiedz_cos()
public static class Narzedzia
{
    public static void PowiedzCos(Zwierze z)
    {
        z.DajGlos();
        Console.WriteLine($"Typ obiektu: {z.GetType().Name}");
        Console.WriteLine();
    }
}

// 8. Klasa abstrakcyjna Pracownik
public abstract class Pracownik
{
    public abstract void Pracuj();
}

// 9. Klasa Piekarz
public class Piekarz : Pracownik
{
    public override void Pracuj()
    {
        Console.WriteLine("Trwa pieczenie...");
    }
}

// 12. Klasa A
public class A
{
    public A()
    {
        Console.WriteLine("To jest konstruktor A");
    }
}

// 13. Klasa B
public class B : A
{
    public B() : base()
    {
        Console.WriteLine("To jest konstruktor B");
    }
}

// 14. Klasa C
public class C : B
{
    public C() : base()
    {
        Console.WriteLine("To jest konstruktor C");
    }
}

// 7, 10, 11, 15. Funkcja Main()
public class Program
{
    public static void Main()
    {
        //  Zadania 1–7 
        Zwierze z = new Zwierze("Zwierzątko");
        Pies p = new Pies("Reksio");
        Kot k = new Kot("Mruczek");
        Waz w = new Waz("Python");

        Narzedzia.PowiedzCos(z);
        Narzedzia.PowiedzCos(p);
        Narzedzia.PowiedzCos(k);
        Narzedzia.PowiedzCos(w);

        //  Zadania 8–10 
        Piekarz piekarz = new Piekarz();
        piekarz.Pracuj();

        //  Zadanie 11 
        // Pracownik p1 = new Pracownik();  // Błąd kompilacji: nie można utworzyć instancji klasy abstrakcyjnej

        //  Zadania 12–15 
        Console.WriteLine();
        A a = new A();
        Console.WriteLine();
        B b = new B();
        Console.WriteLine();
        C c = new C();
    }
}

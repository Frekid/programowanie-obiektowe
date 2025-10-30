using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("To jest cwiczenie 1");
    }
}

public class Zwierze
{
    // Pola prywatne
    private string nazwa;
    private string gatunek;
    private int liczbaNog;

    // Pole statyczne
    private static int liczbaZwierzat = 0;

    // Gettery
    public string GetNazwa()
    {
        return nazwa;
    }

    public string GetGatunek()
    {
        return gatunek;
    }

    public int GetLiczbaNog()
    {
        return liczbaNog;
    }

    // Setter
    public void SetNazwa(string nowaNazwa)
    {
        nazwa = nowaNazwa;
    }

    // Konstruktor bezparametrowy
    public Zwierze()
    {
        nazwa = "Rex";
        gatunek = "Pies";
        liczbaNog = 4;
        liczbaZwierzat++;
    }

    // Konstruktor z parametrami
    public Zwierze(string nazwa, string gatunek, int liczbaNog)
    {
        this.nazwa = nazwa;
        this.gatunek = gatunek;
        this.liczbaNog = liczbaNog;
        liczbaZwierzat++;
    }

    // Konstruktor kopiujący
    public Zwierze(Zwierze inne)
    {
        this.nazwa = inne.nazwa;
        this.gatunek = inne.gatunek;
        this.liczbaNog = inne.liczbaNog;
        liczbaZwierzat++;
    }

    // Metoda daj_glos()
    public void DajGlos()
    {
        if (gatunek.ToLower() == "kot")
            Console.WriteLine("Miau!");
        else if (gatunek.ToLower() == "krowa")
            Console.WriteLine("Muuu!");
        else if (gatunek.ToLower() == "pies")
            Console.WriteLine("Hau!");
        else
            Console.WriteLine("Nieznany odgłos.");
    }

    // Metoda statyczna zwracająca liczbę zwierząt
    public static int GetLiczbaZwierzat()
    {
        return liczbaZwierzat;
    }
}

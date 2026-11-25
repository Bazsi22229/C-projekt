
using System;
using System.IO;
using System.Collections.Generic;

class Konyv
{
    public string Cim;
    public string Szerzo;
    public int Ev;

    public Konyv(string cim, string szerzo, int ev)
    {
        Cim = cim;
        Szerzo = szerzo;
        Ev = ev;
    }

    public void Kiir()
    {
        Console.WriteLine(Cim + " - " + Szerzo + " (" + Ev + ")");
    }
}

class Program
{
    static List<Konyv> konyvek = new List<Konyv>();
    static string fajl = "konyvek.txt";

    static void Beolvas()
    {
        if (!File.Exists(fajl))
            File.Create(fajl).Close();

        foreach (string sor in File.ReadAllLines(fajl))
        {
            string[] adat = sor.Split(';');

            if (adat.Length == 3)
            {
                int ev;
                if (int.TryParse(adat[2], out ev))
                    konyvek.Add(new Konyv(adat[0], adat[1], ev));
            }
        }
    }

    static void Mentes()
    {
        List<string> sorok = new List<string>();

        foreach (Konyv k in konyvek)
            sorok.Add(k.Cim + ";" + k.Szerzo + ";" + k.Ev);

        File.WriteAllLines(fajl, sorok);
        Console.WriteLine("sikeres mentes!");
    }

    static void UjKonyv()
    {
        Console.Write("Konyv cime: ");
        string cim = Console.ReadLine();

        Console.Write("Szerzo: ");
        string szerzo = Console.ReadLine();

        Console.Write("Megjelenesi ev: ");
        int ev;

        if (!int.TryParse(Console.ReadLine(), out ev))
        {
            Console.WriteLine("hibas evszam!");
            return;
        }

        konyvek.Add(new Konyv(cim, szerzo, ev));
        Mentes();

        Console.WriteLine("konyv hozzaadva!");
    }

    static void KonyvekKiirasa()
    {
        if (konyvek.Count == 0)
        {
            Console.WriteLine("Nincsenek konyvek!");
            return;
        }

        foreach (Konyv k in konyvek)
            k.Kiir();
    }

    static void Kereses()
    {
        Console.Write("melyik konyvet keresed? ");
        string cim = Console.ReadLine();

        bool van = false;

        foreach (Konyv k in konyvek)
        {
            if (k.Cim.ToLower() == cim.ToLower())
            {
                k.Kiir();
                van = true;
            }
        }

        if (!van)
            Console.WriteLine("Nincs ilyen konyv!");
    }

    static void KonyvekSzama()
    {
        Console.WriteLine("konyvek szama: " + konyvek.Count);
    }

    static void LeghosszabbCim()
    {
        if (konyvek.Count == 0)
        {
            Console.WriteLine("Nincsenek konyvek!");
            return;
        }

        Konyv leghosszabb = konyvek[0];

        foreach (Konyv k in konyvek)
        {
            if (k.Cim.Length > leghosszabb.Cim.Length)
                leghosszabb = k;
        }

        Console.WriteLine("A leghosszabb cimu konyv:");
        leghosszabb.Kiir();
    }

    static void Main()
    {
        Beolvas();

        string valasztas = "";

        while (valasztas != "6")
        {
            Console.WriteLine("1. uj konyv");
            Console.WriteLine("2. konyvek listazasa");
            Console.WriteLine("3. konyv keresese");
            Console.WriteLine("4. konyvek szama");
            Console.WriteLine("5. leghosszabb konyvcim");
            Console.WriteLine("6. kilepes");

            Console.Write("valasztas: ");
            valasztas = Console.ReadLine();

            switch (valasztas)
            {
                case "1": UjKonyv(); break;
                case "2": KonyvekKiirasa(); break;
                case "3": Kereses(); break;
                case "4": KonyvekSzama(); break;
                case "5": LeghosszabbCim(); break;
                case "6": Console.WriteLine(""); break;
                default: Console.WriteLine("hibas valasztas!"); break;
            }
        }
    }
}

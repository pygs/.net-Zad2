using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace zad2
{
    class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        double
            liczbaA,
            liczbaB
            ;
        string buforDziałania = null;
        bool
            flagaUłamka = false,
            flagaDziałania = false
            ;
        string buforIO = "0";
        public string IO
        {
            get { return buforIO; }
            set
            {
                buforIO = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IO"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bufory"));
            }
        }

        public double LiczbaA
        {
            get => liczbaA;
            set
            {
                liczbaA = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bufory"));
            }
        }
        public double LiczbaB
        {
            get => liczbaB;
            set
            {
                liczbaB = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bufory"));
            }
        }
        public string BuforDziałania
        {
            get => buforDziałania;
            set
            {
                buforDziałania = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bufory"));
            }
        }
        public string Bufory
        {
            get
            {
                if (buforDziałania == null)
                    return "";
                if (flagaDziałania == false)
                    return $"{liczbaA} {buforDziałania}";
                return $"{liczbaA} {buforDziałania} {liczbaB}";
            }
        }

        internal void DziałanieZwykłe(string znak)
        {
            if (flagaDziałania == true)
                ;
            else if (BuforDziałania == null)
            {
                BuforDziałania = znak;
                LiczbaA = double.Parse(buforIO);
                flagaDziałania = true;
            }
            else
            {
                BuforDziałania = znak;
                LiczbaB = double.Parse(buforIO);
                flagaDziałania = true;
                LiczbaA = WykonajDziałanie();
                IO = LiczbaA.ToString();
            }
        }

        internal void Procent()
        {
            flagaDziałania = true;
            LiczbaB = double.Parse(buforIO) / 100 * liczbaA;
            PodajWynik();
        }

        internal void DziałanieJednoargumentowe(string działanie)
        {
            BuforDziałania = działanie;
            flagaDziałania = true;
            LiczbaA = double.Parse(buforIO);
            IO = WykonajDziałanie().ToString();
        }

        internal void PodajWynik()
        {
            if (flagaDziałania == false)
            {
                LiczbaB = double.Parse(buforIO);
                flagaDziałania = true;
            }
            IO = WykonajDziałanie().ToString();
            liczbaA = double.Parse(IO);
        }

        private double WykonajDziałanie()
        {
            if (BuforDziałania == "+")
                return LiczbaA + LiczbaB;
            else if (BuforDziałania == "-")
                return LiczbaA - LiczbaB;
            else if (BuforDziałania == "*")
                return LiczbaA * LiczbaB;
            else if (BuforDziałania == "/")
                return LiczbaA / LiczbaB;
            else if (BuforDziałania == "x²")
                return LiczbaA * LiczbaA;
            else if (BuforDziałania == "√")
                return Math.Sqrt(LiczbaA);
            else if (BuforDziałania == "1/x")
                return 1 / LiczbaA;
            else if (BuforDziałania == "mod")
                return LiczbaA % LiczbaB;
            else if (BuforDziałania == "log10")
                return Math.Log10(LiczbaA);
            else if (BuforDziałania == "!")
                if (LiczbaA % 1 == 0)
                {
                    double n = LiczbaA;
                    for (double i = n - 1; i > 0; i--)
                    {
                        n *= i;
                    }
                    return n;
                }
                else
                    return 0;
            else if (BuforDziałania == "^")
                return Math.Ceiling(LiczbaA);
            else if (BuforDziałania == "v")
                return Math.Floor(LiczbaA);
            else
                return 0;
        }

        internal void Resetuj()
        {
            Zeruj();
            BuforDziałania = default;
            LiczbaA = default;
            LiczbaB = default;
        }
        internal void Zeruj()
        {
            flagaUłamka = false;
            flagaDziałania = false;
            IO = "0";
        }
        internal void Cofnij()
        {
            if (buforIO == "0")
                return;
            else if (
                buforIO == "0,"
                ||
                buforIO == "-0,"
                ||
                (buforIO[0] == '-' && buforIO.Length == 2)
                )
                Zeruj();
            else
            {
                char usuwanyZnak = buforIO[buforIO.Length - 1];
                IO = buforIO.Substring(0, buforIO.Length - 1);
                if (usuwanyZnak == ',')
                    flagaUłamka = false;
            }
        }
        internal void DopiszCyfrę(string cyfra)
        {
            if (flagaDziałania)
                Zeruj();
            if (buforIO == "0")
                buforIO = "";
            IO += cyfra;
        }
        internal void ZmieńZnak()
        {
            flagaDziałania = false;
            if (buforIO == "0")
                return;
            else if (buforIO[0] == '-')
                IO = buforIO.Substring(1);
            else
                IO = '-' + IO;
        }
        internal void PostawPrzecinek()
        {
            if (flagaDziałania)
                Zeruj();
            if (flagaUłamka || buforIO[buforIO.Length - 1] == ',')
                return;
            else
            {
                IO += ',';
                flagaUłamka = true;
            }
        }
    }
}

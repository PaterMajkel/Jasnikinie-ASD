using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace jaskinie
{
    class Program
    {

        public static class dane
        {
            public static int x, y, z, maks;
            public static List<int> objetosci = new List<int>();
        }

        
        static int checkaround(ref int[,,] tab, int y, int x, int z, int znak, bool licz)
        {
            if(licz)
            { if (z+1 > dane.maks)
                    dane.maks = z+1;
            }
            tab[y, x, z] = znak;

            int licznik = 1;
            if(y-1>=0)
            {
                if (tab[y - 1, x, z] == 0)
                licznik +=checkaround(ref tab, y - 1, x, z, znak, licz);
            }
            if (y + 1 < dane.y)
            {
                if (tab[y + 1, x, z] == 0)
                    licznik += checkaround(ref tab, y + 1, x, z, znak, licz);
            }
            if (x + 1 < dane.x)
            {
                if (tab[y, x+1, z] == 0)
                    licznik += checkaround(ref tab, y, x + 1, z, znak, licz);
            }
            if (x - 1 >= 0)
            {
                if (tab[y, x-1, z] == 0)
                    licznik += checkaround(ref tab, y, x - 1, z, znak, licz);
            }
            if (z + 1 < dane.z)
            {
                if (tab[y, x, z+1] == 0)
                    licznik += checkaround(ref tab, y, x, z + 1, znak, licz);
            }
            if (z - 1 >= 0)
            {
   
                if (tab[y, x, z-1] == 0)
                    licznik += checkaround(ref tab, y, x, z - 1, znak, licz);
            }

            return licznik;
        }
        static void Main(string[] args)
        {

            StreamReader wej = new StreamReader("duzy2.txt");
            int[] tab = Array.ConvertAll(wej.ReadLine().Split(), int.Parse);
            int[,,] jaskinie = new int[tab[0], tab[1], tab[2]];
            int licznik = 0;
            dane.y = tab[0];
            dane.x = tab[1];
            dane.z = tab[2];

            for (int i = 0; i < tab[2]; i++)
            {
                for (int j = 0; j < tab[1]; j++)
                {
                    char[] tab2 = wej.ReadLine().ToCharArray();
                    if (tab2.Length != tab[1])
                    {
                        j--;
                        //Console.WriteLine(tab2);
                        continue;
                    }
                    for (int k = 0; k < tab[0]; k++)
                    {
                        int p;
                        if (tab2[k] == 'x')
                            p = -1;
                        else p = 0;
                        jaskinie[j, k, i] = p;
                    }

                }
            }
            int znak = 1;
            for (int i = 0; i < tab[0]; i++)
            {
                for (int j = 0; j < tab[1]; j++)
                {
                    if (jaskinie[i, j, 0] == 0)
                    {

                        licznik = checkaround(ref jaskinie, i, j, 0, znak, true);
                        dane.objetosci.Add(licznik);

                        znak++;

                    }
                }
            }
            int izolowane = 0;
            for (int k = 1; k < tab[2]; k++)
            {
                for (int i = 0; i < tab[0]; i++)
                {
                    for (int j = 0; j < tab[1]; j++)
                    {
                        if (jaskinie[i, j, k] == 0)
                        {
                            izolowane++;

                            licznik = checkaround(ref jaskinie, i, j, k, znak, false);
                            znak++;
                           
                            dane.objetosci.Add(licznik);
                        }
                        

                    }

                }
            }
            

            StreamWriter plik = new StreamWriter(@"wyj.txt");
                /*for (int i = 0; i < tab[2]; i++)
                {
                    for (int j = 0; j < tab[1]; j++)
                    {
                        for (int k = 0; k < tab[0]; k++)
                        {
                            if (jaskinie[j, k, i].znak == -1)
                                plik.Write("x ");
                            else
                                plik.Write(jaskinie[j, k, i].znak+" ");

                        }
                        plik.WriteLine();
                    }
                    plik.WriteLine();

                }*/

            //int bruh = 0;
            //foreach (var iks in dane.objetosci)
            //Console.Write(bruh+1 + " " + dane.objetosci[bruh++]+"; ");
            //Console.WriteLine();
            
            plik.WriteLine(dane.maks + " " + dane.objetosci.Max() + " " + izolowane);
            plik.Close();//Console.Read();
        }
    }
}

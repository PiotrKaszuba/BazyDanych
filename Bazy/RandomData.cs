using BazyDanych2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazy
{
    class RandomData
    {
        static Random los = new Random();
        public static void test()
        {
            List<gracz> gracze = new List<gracz>();
            List<zapis_do_druzyny> zapisy = new List<zapis_do_druzyny>();
            List<druzyna> druzyny = new List<druzyna>();
           
            for (int i = 0; i < 4; i++)
            {
                gracze.Add(new gracz { IDGracza = i });
                zapisy.Add(new zapis_do_druzyny { idZapis_do_druzyny = i, idGracza = i, idDruzyny=i/2 });
                if (i % 2 == 0) druzyny.Add(new druzyna { idDruzyny = i / 2, idMeczu = 1 });
            }

            druzyny[0].Kolor_druzyny = "red";
            druzyny[1].Kolor_druzyny = "blue";
            List<zdarzenie> zdarzenia = setListZdarzenia(1, 1);

            List<udzial> udzialy = StworzUdzialy(1, zdarzenia, gracze, zapisy);
            statystyki_meczu stat = setStatyMeczu(zdarzenia, udzialy, druzyny, zapisy, 1, 1);

            int aa = 0;


        }
        public static List<druzyna> setListDruzyna(int druzyna, int idMeczu)
        {
            druzyna team1 = new druzyna(druzyna, idMeczu, "red");
            druzyna team2 = new druzyna((druzyna + 1), idMeczu, "blue");

            List<druzyna> list = new List<druzyna>() { team1, team2 };
            return list;
        }

        public static List<zdarzenie> setListZdarzenia(int id_zdarzenia, int idMeczu)
        {
            zdarzenie ev;
            List<zdarzenie> list = new List<zdarzenie>();
            Random r = new Random();
            int towers = 0;
            int i = 0;
            while (towers < 5)
            {
                int e = r.Next(100);
                if (e >= 80)
                {
                    towers++;
                    ev = new zdarzenie((id_zdarzenia + i), idMeczu, "tower");
                    list.Add(ev);
                }
                if (e < 80)
                {
                    ev = new zdarzenie((id_zdarzenia + i), idMeczu, "kill");
                    list.Add(ev);
                }
                i++;
            }

            return list;
        }

        public static statystyki_meczu setStatyMeczu(List<zdarzenie> zdarzenie, List<udzial> udzial, List<druzyna> druzyna, List<zapis_do_druzyny> zapis, int idStatystykiMeczu, int idMeczu)
        {
            statystyki_meczu staty;
            Random r = new Random();
            int czas = r.Next(900, 2400);
            int killsred = 0;
            int killsblue = 0;

            List<int> killerTeam = new List<int>();
            List<string> kolor = new List<string>();
            List<int> killers = (from udzialy in udzial where udzialy.Rola.Equals("killer") select udzialy.idGracza).ToList();
            foreach (int i in killers)
            {
                killerTeam.Add((from z in zapis where z.idGracza == i select z.idDruzyny).Single());

            }
            foreach (int j in killerTeam)
            {
                kolor.Add((from d in druzyna where d.idDruzyny == j select d.Kolor_druzyny).Single());
            }
            foreach (string c in kolor)
            {
                if (c.Equals("red")) killsred++;
                if (c.Equals("blue")) killsblue++;
            }

            staty = new statystyki_meczu(idStatystykiMeczu, czas, idMeczu, killsred, killsblue);

            return staty;
        }

        static bool wybranyGracz(List<int> list, int i)
        {
            bool pick = false;
            foreach (int a in list)
            {
                if (a == i) pick = true;
            }
            return pick;
        }
        public static List<gracz> losowanieGraczy(List<gracz> list)
        {
            List<gracz> wylosowaniGracze = new List<gracz>();
            Random r = new Random();
            List<int> indeksy = new List<int>();
            int iloscGraczy = 0;
            while (iloscGraczy < 4)
            {
                int ind = r.Next(list.Count);
                Console.WriteLine("index " + ind);
                if (!wybranyGracz(indeksy, ind))
                {
                    indeksy.Add(ind);
                    wylosowaniGracze.Add(list[ind]);
                    iloscGraczy++;
                }
            }

            return wylosowaniGracze;
        }
        public static List<zapis_do_druzyny> StworzZapisy(List<gracz> gracze, List<druzyna> druzyn, int ID)
        {
            int a = 0;
            int b = 0;
            List<zapis_do_druzyny> ListaZapisow = new List<zapis_do_druzyny>();
            Random losuj = new Random();
            a = losuj.Next(4);
            do
            {
                b = losuj.Next(4);
            } while (b == a);
            for (int i = 0; i < 4; i++)
            {
                zapis_do_druzyny zapis = new zapis_do_druzyny();
                if (i == a || i == b)
                {
                    zapis.idDruzyny = druzyn.ElementAt(0).idDruzyny;
                    zapis.idGracza = gracze.ElementAt(i).IDGracza;
                    zapis.idZapis_do_druzyny = ID++;
                }
                else
                {
                    zapis.idDruzyny = druzyn.ElementAt(1).idDruzyny;
                    zapis.idGracza = gracze.ElementAt(i).IDGracza;
                    zapis.idZapis_do_druzyny = ID++;
                }
                ListaZapisow.Add(zapis);
            }
            return ListaZapisow;
        }
        public static mecz StworzMecz(List<druzyna> druzyn)
        {
            Random los = new Random();
            mecz mecze = new mecz();
            mecze.idMeczu = druzyn.ElementAt(0).idMeczu;
            if (druzyn.ElementAt(0).Kolor_druzyny == "red")
            {
                mecze.idDruzynaRed = druzyn.ElementAt(0).idDruzyny;
                mecze.idDruzynaBlue = druzyn.ElementAt(1).idDruzyny;
            }
            else
            {
                mecze.idDruzynaRed = druzyn.ElementAt(1).idDruzyny;
                mecze.idDruzynaBlue = druzyn.ElementAt(0).idDruzyny;
            }
            mecze.datameczu = DateTime.Now;
            mecze.tryb = "2vs2";
            int w = los.Next(2);
            if (w == 0) mecze.Wynik = "blue";
            else mecze.Wynik = "red";
            return mecze;
        }
        public static List<udzial> StworzUdzialy(int idUdzial, List<zdarzenie> zdarzenia, List<gracz> gracze, List<zapis_do_druzyny> zapis)
        {
            List<udzial> udzialy = new List<udzial>();
            foreach (zdarzenie i in zdarzenia)
            {
                if (i.rodzaj_zdarzenia == "tower")
                {
                    int w = gracze.Count;
                    
                    int d = los.Next(w);
                    udzial NowyUdzial = new udzial();
                    NowyUdzial.idGracza = gracze.ElementAt(d).IDGracza;
                    NowyUdzial.idUdzial = idUdzial++;
                    NowyUdzial.idZdarzenie = i.idZdarzenie;
                    NowyUdzial.Rola = "destroyer";
                    udzialy.Add(NowyUdzial);
                }
                else
                {
                    int w = gracze.Count;
                  
                    int d = los.Next(w);
                    udzial NowyUdzial = new udzial();
                    NowyUdzial.idGracza = gracze.ElementAt(d).IDGracza;
                    NowyUdzial.idUdzial = idUdzial++;
                    NowyUdzial.idZdarzenie = i.idZdarzenie;
                    NowyUdzial.Rola = "killer";
                    udzialy.Add(NowyUdzial);
                    int kill = (from killer in zapis where gracze.ElementAt(d).IDGracza == killer.idGracza select killer.idDruzyny).Single();
                    List<int> victims = (from victim in zapis where victim.idDruzyny != kill select victim.idGracza).ToList();
                    
                    int g = los.Next(2);
                    NowyUdzial = new udzial();
                    if (g == 0)
                    {
                        NowyUdzial.idGracza = victims.ElementAt(0);
                    }
                    else
                    {
                        NowyUdzial.idGracza = victims.ElementAt(1);
                    }

                    NowyUdzial.idUdzial = idUdzial++;
                    NowyUdzial.idZdarzenie = i.idZdarzenie;
                    NowyUdzial.Rola = "victim";
                    udzialy.Add(NowyUdzial);
                }
            }
            return udzialy;
        }
        public static void play_match()
        {
            //test();
            var mDB = new BazyDB();

            int id_mecz_start;
            int druzyna_start;
            int udzial_start;
            int zapis_start;
            int zdarzenie_start;
            int statystyki_start;

            if (mDB.mecz.Any())
                id_mecz_start = mDB.mecz.OrderByDescending(u => u.idMeczu).FirstOrDefault().idMeczu + 1;
            else
                id_mecz_start = 1;

            if (mDB.druzyna.Any())
                druzyna_start = mDB.druzyna.OrderByDescending(u => u.idDruzyny).FirstOrDefault().idDruzyny + 1;
            else
                druzyna_start = 1;
            if (mDB.udzial.Any())
                udzial_start = mDB.udzial.OrderByDescending(u => u.idUdzial).FirstOrDefault().idUdzial + 1;
            else
                udzial_start = 1;
            if (mDB.zapis_do_druzyny.Any())
                zapis_start = mDB.zapis_do_druzyny.OrderByDescending(u => u.idZapis_do_druzyny).FirstOrDefault().idZapis_do_druzyny + 1;
            else
                zapis_start = 1;
            if (mDB.zdarzenie.Any())
                zdarzenie_start = mDB.zdarzenie.OrderByDescending(u => u.idZdarzenie).FirstOrDefault().idZdarzenie + 1;
            else
                zdarzenie_start = 1;
            if (mDB.statystyki_meczu.Any())
                statystyki_start = mDB.statystyki_meczu.OrderByDescending(u => u.idStatystyki_Meczu).FirstOrDefault().idStatystyki_Meczu + 1;
            else
                statystyki_start = 1;

            List<gracz> players4 = losowanieGraczy(mDB.gracz.ToList());

           
            List<druzyna> druzyny = setListDruzyna(druzyna_start,id_mecz_start);

            mecz match = StworzMecz(druzyny);

            List<zapis_do_druzyny> zapisy = StworzZapisy(players4, druzyny, zapis_start);

            List<zdarzenie> zdarzenia=setListZdarzenia(zdarzenie_start,id_mecz_start);

            List<udzial> udzialy = StworzUdzialy(udzial_start,zdarzenia,players4,zapisy);

            statystyki_meczu statystyki=setStatyMeczu(zdarzenia, udzialy, druzyny, zapisy, statystyki_start,id_mecz_start);

            


            DbContextTransaction trans = mDB.Database.BeginTransaction();
            try {
                mDB.mecz.Add(match);
                foreach (var item in druzyny)
                {
                    mDB.druzyna.Add(item);
                }

               

                foreach (var item in zapisy)
                {
                    mDB.zapis_do_druzyny.Add(item);
                }

                foreach(var item in zdarzenia)
                {
                    mDB.zdarzenie.Add(item);
                }

                foreach (var item in udzialy)
                {
                    mDB.udzial.Add(item);
                }

                mDB.statystyki_meczu.Add(statystyki);
                mDB.SaveChanges();
                trans.Commit();

            }
            catch(Exception e)
            {
                trans.Rollback();
                
               
            }
            

           
  
        }

    }
}

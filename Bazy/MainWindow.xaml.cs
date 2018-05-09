using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Windows.Controls.Primitives;
using System.Data;
using System.Data.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Data.Entity.Validation;
using LinqToDB.SqlQuery;
using System.Net.Mail;
using System.Net;
using MongoDB.Driver;

//1406 data too long
//1048 cannot be null
//1062 duplicate
namespace BazyDanych2
{

    class gracz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDGracza { get; set; }
        public String Nick { get; set; }
        public String SteamID { get; set; }
        public String NazwaKontaWGrze { get; set; }

        public DateTime? DataDolaczenia { get; set; }

          
        }
    
        class druzyna
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idDruzyny { get; set; }

            //[ForeignKey("mecz")]
            public int idMeczu { get; set; }
            public string Kolor_druzyny { get; set; }

        //public virtual ICollection<mecz> RedTeams { get; set; }
        // public virtual mecz mecz { get; set; }
        //public virtual ICollection<mecz> BlueTeams { get; set; }
        public druzyna(int idTean, int id, string kolor)
        {
            idDruzyny = idTean;
            idMeczu = id;
            Kolor_druzyny = kolor;
        }

        public druzyna()
        {

        }
        public override string ToString()
            {
            return "Drużyna: " + idDruzyny + " z meczu: " + idMeczu + " o kolorze: " + Kolor_druzyny;
            }
        }
        class kolejka_statystyki_gracza
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idKolejka_Statystyki_Gracza { get; set; }
            public string TypKolejki { get; set; }
            //[ForeignKey("gracz"), DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idGracza { get; set; }
           // public virtual gracz gracz { get; set; }
            
            public int ELO { get; set; }
            public float Winrate { get; set; }
            public float KDA { get; set; }
            public int Kills { get; set; }
            public int Deaths { get; set; }
            public int Assists { get; set; }
        }
        class mecz
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idMeczu { get; set; }
            public DateTime datameczu { get; set; }
            public string tryb { get; set; }






            //[ForeignKey("Red")] 
            public int idDruzynaRed { get; set; }

            //[ForeignKey("Blue")]
            public int idDruzynaBlue { get; set; }

        // [ForeignKey("idDruzynaRed")]
        //public virtual druzyna Red { get; set; }
        // [ForeignKey("idDruzynaBlue")]
        // public virtual druzyna Blue { get; set; }


        public override string ToString()
        {
            return "Mecz: " + idMeczu + " rozegrany: " + datameczu.ToString() + " w trybie: " + tryb + " z druzynami o numerach: "+idDruzynaBlue+", "+idDruzynaRed;
        }

        public string Wynik { get; set; }
           // [ForeignKey("statystyki_meczu")]
           // public int idStatystyki_Meczu { get; set; }

            //public virtual statystyki_meczu statystyki_meczu { get; set; }

    }
        class osiagniecia
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idOsiagniecia { get; set; }
            [ForeignKey("gracz"), DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idGracza { get; set; }
            public virtual gracz gracz { get; set; }
            public string rodzaj_osiagniecia { get; set; }
        }
        class statystyki_meczu
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idStatystyki_Meczu { get; set; }
            public int czas { get; set; }
            //[ForeignKey("mecz"), DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idMeczu { get; set; }

        //public virtual mecz mecz { get; set; }

        public statystyki_meczu(int s, int c, int m, int r, int b)
        {
            idStatystyki_Meczu = s;
            czas = c;
            idMeczu = m;
            ZabojstwaRed = r;
            ZabojstwaBlue = b;
            AsystyRed = 0;
            AsystyBlue = 0;
        }
        public statystyki_meczu()
        {

        }

        public int ZabojstwaRed { get; set; }
            public int ZabojstwaBlue { get; set; }
            public int AsystyRed { get; set; }
            public int AsystyBlue { get; set; }
        public override string ToString()
        {
            return "Statystyki meczu : " + idStatystyki_Meczu + " o ID: " + idMeczu + ", trwał " + czas + ", zabójstwa niebieskiej drużyny: " + ZabojstwaBlue + ", zabójstwa czerwonej drużyny: "+ZabojstwaRed;
        }

    }
        class udzial
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idUdzial { get; set; }
            //[ForeignKey("zdarzenie"), DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idZdarzenie { get; set; }
            //public virtual zdarzenie zdarzenie { get; set; }
            //[ForeignKey("gracz"), DatabaseGenerated(DatabaseGeneratedOption.None)]
            public  int idGracza { get; set; }
        //public virtual gracz gracz { get; set; }
        public string Rola { get; set; }
        }
        class zapis_do_druzyny
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idZapis_do_druzyny { get; set; }
            //[ForeignKey("gracz"), DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idGracza { get; set; }
        //public virtual gracz gracz { get; set; }
        //[ForeignKey("druzyna"), DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idDruzyny { get; set; }
       // public virtual druzyna druzyna { get; set; }
    }
        class zdarzenie
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idZdarzenie { get; set; }
            //[ForeignKey("mecz"), DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int idMeczu { get; set; }
        //public virtual mecz mecz { get; set; }
            public string rodzaj_zdarzenia { get; set; }
        public zdarzenie(int z, int m, string r)
        {
            idZdarzenie = z;
            idMeczu = m;
            rodzaj_zdarzenia = r;
        }

        public zdarzenie()
        {

        }
    }

        
        [Table("widok")]
        public class widok
        {
            [Key]
            public int GraczeCS { get; set; }
            public int GraczeLOL { get; set; }
        }
    [Table("raport_nowych_uzytkownikow")]
    public class raport_nowych_uzytkownikow
    {
        [Key]
        public int NowiUzytkownicy { get; set; }
    }

    [Table("raport_dzienny")]
    public class raport_dzienny
    {
        [Key]
        [Column("Raport dzienny")]
        public int Raport_Dzienny { get; set; }
        public int Raport_Tygodniowy { get; set; }
        public int Raport_Miesieczny { get; set; }
    }


    class BazyDB : DbContext
        {
            public DbSet<widok> widok { get; set; }
        //public DbSet<raport_dzienny> raport_dzienny { get; set; }
        public DbSet<gracz> gracz { get; set; }
            public DbSet<zdarzenie> zdarzenie { get; set; }
            public DbSet<mecz> mecz { get; set; }
            public DbSet<druzyna> druzyna { get; set; }
            public DbSet<zapis_do_druzyny> zapis_do_druzyny { get; set; }
            public DbSet<kolejka_statystyki_gracza> kolejka_statystyki_gracza  { get; set; }
            public DbSet<statystyki_meczu> statystyki_meczu { get; set; }
            public DbSet<osiagniecia> osiagniecia { get; set; }
            public DbSet<udzial> udzial { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<gracz>().ToTable("gracz");
            modelBuilder.Entity<mecz>().ToTable("mecz");
            modelBuilder.Entity<druzyna>().ToTable("druzyna");
            modelBuilder.Entity<osiagniecia>().ToTable("osiagniecia");
            modelBuilder.Entity<udzial>().ToTable("udzial");
            modelBuilder.Entity<zdarzenie>().ToTable("zdarzenie");
        }
    }


        public partial class MainWindow : Window
        {
            private gracz get_gracz()
            {
                int ids = -1;
                string nicks;
                string steams;
                string nazwas;

                try
                {
                    if (id.Text.Equals(""))
                        ids = -1;
                    else ids = Convert.ToInt32(id.Text);
                }
                catch (System.FormatException e)
                {
                    MessageBox.Show("Zły format ID");
                    return null;
                }
                catch (System.OverflowException e)
                {
                    MessageBox.Show("Przekroczenie wartości zmiennej ID (Int32)");
                    return null;
                }

                if (nick.Text.Equals(""))
                    nicks = null;
                else nicks = nick.Text;
                if (steam.Text.Equals(""))
                    steams = null;
                else
                    steams = steam.Text;
                if (nazwa.Text.Equals(""))
                    nazwas = null;
                else
                    nazwas = nazwa.Text;



                return new gracz { IDGracza = ids, Nick = nicks, SteamID = steams, NazwaKontaWGrze = nazwas };
            }

            public MainWindow()
            {
                InitializeComponent();

            send_mail(new gracz { IDGracza = 1, Nick = "jakisnick" });
            Up();
            //demoUp();


            }

        public void demoUp()
        {
            List<gracz> gracze = new List<gracz>();

            for (int i = 0; i < 3; i++)
            {
                gracze.Add(new gracz { IDGracza = i, Nick = "Nick" + i });

            }

            grid.ItemsSource = gracze;
        }
            public void Up()
            {
                var mDB = new BazyDB();


            //var bazy = new DataModels.BazyDB();
            //dataGrid.ItemsSource = (from x in mDB.mecz select x).ToList();
            

                if (!(bool)checkBox.IsChecked)
                    grid.ItemsSource = (from x in mDB.gracz select x).ToList();
                else
                    grid.ItemsSource = mDB.Database.SqlQuery<gracz>("print").ToList();
            }
            private void send_mail(gracz g)
            {


                var fromAddress = new MailAddress("piotrkaszuba1996@gmail.com", "CustomQueue");
                var toAddress = new MailAddress("piotrkaszuba1996@gmail.com", "root");




                const string fromPassword = "";
                const string subject = "Nowy Gracz!!!";
                string body = "Zarejestrował się nowy gracz: " + g.IDGracza + ", " + g.Nick;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body

            })

            {
                message.Attachments.Add(new Attachment("file.txt"));
                smtp.Send(message);
            }
            }
            private void Dodaj(object sender, RoutedEventArgs e)
            {

                gracz g = get_gracz();
                if (g == null) return;

                var mDB = new BazyDB();

                if (!(bool)checkBox.IsChecked)
                {
                    mDB.gracz.Add(g);
                    try
                    {
                        if (mDB.SaveChanges() == 1) send_mail(g);


                    }
                    catch (DbUpdateException ex)
                    {
                        String trace = null;
                        switch (((MySqlException)(ex.InnerException.InnerException)).Number)
                        {
                            case 1062:
                                trace = "Pole ID lub Nick nie jest unikalne";
                                break;
                            case 1048:
                                trace = "Pola ID i Nick nie mogą być puste";
                                break;
                            case 1406:
                                trace = "Pole Nick, SteamID lub NazwaKontaWGrze przekracza maksymalna długość";
                                break;
                            default:
                                trace = "Niezidentyfikowany błąd";
                                break;
                        }


                        MessageBox.Show(trace);

                    }


                }

                else

                {

                    try
                    {
                        MySqlParameter id = new MySqlParameter("@id", g.IDGracza);
                        MySqlParameter nick = new MySqlParameter("@nick", g.Nick);
                        MySqlParameter steam = new MySqlParameter("@steamid", g.SteamID);
                        MySqlParameter nazwa = new MySqlParameter("@nazwakontawgrze", g.NazwaKontaWGrze);

                        if (mDB.Database.ExecuteSqlCommand("dodaj(@id,@nick,@steamid,@nazwakontawgrze)", id, nick, steam, nazwa) == 1) send_mail(g);
                    }
                    catch (MySqlException ex)
                    {
                        String trace = null;
                        int a = 0;
                        switch (ex.Number)
                        {
                            case 1062:
                                trace = "Pole ID lub Nick nie jest unikalne";
                                break;
                            case 1048:
                                trace = "Pola ID i Nick nie mogą być puste";
                                break;
                            case 1406:
                                trace = "Pole Nick, SteamID lub NazwaKontaWGrze przekracza maksymalna długość";
                                break;
                            case 20000:
                                trace = "Nie można dodać gracza o nicku admin lub root";
                                break;
                            case 20001:
                                trace = "Nie można zmienić nicku gracza na admin lub root";
                                break;
                            case 20002:
                                trace = "Nie można usunąć administratora";
                                break;
                            case 1644:
                                trace = ex.Message;
                                break;
                            default:
                                trace = "Niezidentyfikowany błąd";
                                break;
                        }


                        MessageBox.Show(trace);

                    }

                }




                Up();


            }

            private void Zmien(object sender, RoutedEventArgs e)
            {
                gracz g = get_gracz();
                if (g == null) return;


                var mDB = new BazyDB();
                if (!(bool)checkBox.IsChecked)
                {
                    (from p in mDB.gracz where p.IDGracza == g.IDGracza select p).ToList().ForEach(x => { x.Nick = g.Nick; x.SteamID = g.SteamID; x.NazwaKontaWGrze = g.NazwaKontaWGrze; });

                    try
                    {
                        mDB.SaveChanges();

                    }
                    catch (DbUpdateException ex)
                    {
                        String trace = null;
                        switch (((MySqlException)(ex.InnerException.InnerException)).Number)
                        {
                            case 1062:
                                trace = "Pole ID lub Nick nie jest unikalne";
                                break;
                            case 1048:
                                trace = "Pola ID i Nick nie mogą być puste";
                                break;
                            case 1406:
                                trace = "Pole Nick, SteamID lub NazwaKontaWGrze przekracza maksymalna długość";
                                break;
                            default:
                                trace = "Niezidentyfikowany błąd";
                                break;
                        }


                        MessageBox.Show(trace);


                    }
                }
                else
                {
                    try
                    {
                        MySqlParameter id = new MySqlParameter("@id", g.IDGracza);
                        MySqlParameter nick = new MySqlParameter("@nick", g.Nick);
                        MySqlParameter steam = new MySqlParameter("@steamid", g.SteamID);
                        MySqlParameter nazwa = new MySqlParameter("@nazwakontawgrze", g.NazwaKontaWGrze);
                        mDB.Database.ExecuteSqlCommand("zmien(@id,@nick,@steamid,@nazwakontawgrze)", id, nick, steam, nazwa);
                    }
                    catch (MySqlException ex)
                    {
                        String trace = null;
                        switch (ex.Number)
                        {
                            case 1062:
                                trace = "Pole ID lub Nick nie jest unikalne";
                                break;
                            case 1048:
                                trace = "Pola ID i Nick nie mogą być puste";
                                break;
                            case 1406:
                                trace = "Pole Nick, SteamID lub NazwaKontaWGrze przekracza maksymalna długość";
                                break;
                            case 20001:
                                trace = "Nie można zmienić nicku gracza na admin lub root";
                                break;
                            default:
                                trace = "Niezidentyfikowany błąd";
                                break;
                        }


                        MessageBox.Show(trace);

                    }
                }
                Up();
            }

            private void Usun(object sender, RoutedEventArgs e)
            {

                gracz g = get_gracz();
                if (g == null) return;

                var mDB = new BazyDB();
                if (!(bool)checkBox.IsChecked)
                {
                    try
                    {
                        gracz to_delete = (from x in mDB.gracz where x.IDGracza == g.IDGracza select x).Single();
                        mDB.gracz.Remove(to_delete);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show("Nie ma obiektu o takim ID");
                        return;
                    }



                    try
                    {
                        mDB.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        String trace = null;

                        MySqlException exc = (MySqlException)ex.InnerException.InnerException;
                        switch (exc.Number)
                        {
                            case 20002:
                                trace = "Nie można usunąć administratora";
                                break;
                            case 1644:
                                trace = exc.Message;
                                break;
                            default:
                                trace = "Niezidentyfikowany błąd";
                                break;
                        }


                        MessageBox.Show(trace);

                    }
                }
                else
                {
                    //procedura
                    MySqlParameter param = new MySqlParameter("@date", (object)g.IDGracza);
                    try
                    {
                        if (mDB.Database.ExecuteSqlCommand("usun(@date)", param) == 0)
                        {
                            MessageBox.Show("Nie ma obiektu o takim ID");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        String trace = null;
                        switch (ex.Number)
                        {
                            case 20002:
                                trace = "Nie można usunąć administratora";
                                break;
                            case 1644:
                                trace = ex.Message;
                                break;
                            default:
                                trace = "Niezidentyfikowany błąd";
                                break;
                        }


                        MessageBox.Show(trace);

                    }


                }

                Up();
            }



            private void Id(object sender, RoutedEventArgs e)
            {
                if (id.Text.Equals("Wpisz id"))
                    id.Text = String.Empty;

            }

            private void Nick(object sender, RoutedEventArgs e)
            {
                if (nick.Text.Equals("Wpisz nick"))
                    nick.Text = String.Empty;
            }

            private void IdSteam(object sender, RoutedEventArgs e)
            {
                if (steam.Text.Equals("Wpisz steam id"))
                    steam.Text = String.Empty;
            }

            private void NickWGrze(object sender, RoutedEventArgs e)
            {
                if (nazwa.Text.Equals("Wpisz nazwe konta w grze"))
                    nazwa.Text = String.Empty;
            }

            private void RowDoubleClick(object sender, MouseButtonEventArgs e)
            {
                DataGridRow rowContainer = (DataGridRow)sender;
                gracz gra = (gracz)rowContainer.Item;


            dataGrid.ItemsSource = Bazy.Widoki.ReadActivity(gra.IDGracza, Bazy.Widoki.conn);

            object b = gra.SteamID;
                object c = gra.NazwaKontaWGrze;
                object d = gra.DataDolaczenia;

                if (b != System.DBNull.Value)
                    steam.Text = (String)b;
                else steam.Text = "";
                if (c != System.DBNull.Value)
                    nazwa.Text = (String)c;
                else nazwa.Text = "";

                if (d != System.DBNull.Value && d != null)
                    label.Content = ((DateTime)d).ToLongTimeString();
                else label.Content = "";

                id.Text = Convert.ToString(gra.IDGracza);
                nick.Text = gra.Nick;



            }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Width = 1000;
            this.Height = 500;
            this.Content = new Bazy.Widoki();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var mc = new MongoClient(Bazy.Widoki.conn);
            var database = mc.GetDatabase("bazy");

            IMongoCollection<Bazy.Widoki.Activity> col = database.GetCollection<Bazy.Widoki.Activity>("Activity");

            int id = col.Find<Bazy.Widoki.Activity>((FilterDefinition<Bazy.Widoki.Activity>.Empty)).ToList().OrderByDescending(g => g.ID).FirstOrDefault().ID+1;

            Bazy.Widoki.AddActivity(id, Int32.Parse(this.id.Text), "'logowanie'", Bazy.Widoki.conn);

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            BazyDB mDB = new BazyDB();
            dataGrid.ItemsSource = mDB.Database.SqlQuery<raport_dzienny>("raport_dzienny").ToList();
        }
    }
    
}
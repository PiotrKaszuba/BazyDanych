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
using System.Data.Entity.Core.Metadata.Edm;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bazy
{
    /// <summary>
    /// Interaction logic for Widoki.xaml
    /// </summary>
    public partial class Widoki : Page
    {
       
        public class Developer 
        {
            [BsonId]
            public int ID { get; set; }
            [BsonElement("name")]
            public string Name { get; set; }
            [BsonElement("company_name")]
            public string CompanyName { get; set; }
            [BsonElement("knowledge_base")]
            public List<Knowledge> KnowledgeBase { get; set; }
        }

        public class Knowledge
        {
            [BsonElement("langu_name")]
            public string Language { get; set; }
            [BsonElement("technology")]
            public string Technology { get; set; }
            [BsonElement("rating")]
            public ushort Rating { get; set; }
        }


        public class Activity
        {
            [BsonId]
            public int ID { get; set; }
            [BsonElement("IDGracza")]
            public int IDGracza { get; set; }
            [BsonElement("Aktywnosc")]
            public string Aktywnosc { get; set; }
            [BsonElement("Czas")]
            public string Czas { get; set; }
        }


        public void transaction()
        {

            BazyDanych2.BazyDB context = new BazyDanych2.BazyDB();

            DbContextTransaction trans = context.Database.BeginTransaction();

            context.gracz.Add(new BazyDanych2.gracz{ IDGracza = 100, Nick = "cavd" });

            context.SaveChanges();

            trans.Commit();
        }
        public static string  conn = "mongodb://localhost:27017";

        public static List<Activity> ReadActivity(int IDGracza, string conn)
        {
            var mc = new MongoClient(conn);
            var database = mc.GetDatabase("bazy");

            IMongoCollection<Activity> col = database.GetCollection<Activity>("Activity");

            return col.Find<Activity>(x => x.IDGracza==IDGracza).ToList();


        }


        public static void AddActivity(int ID, int IDGracza, string aktywnosc, string conn)
        {

            //BsonDateTime data = new BsonDateTime(DateTime.Now);
           
            string json = "{_id:" + ID + ", IDGracza:" + IDGracza + ", Aktywnosc:" + aktywnosc + ", Czas:'"+DateTime.Now+"'}";
             MongoDB.Bson.BsonDocument document
            = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

            var mc = new MongoClient(conn);
            var database = mc.GetDatabase("bazy");
            IMongoCollection<BsonDocument> col = database.GetCollection<BsonDocument>("Activity");

            col.InsertOne(document);
        }


        public void mong()
        {
            string conn = "mongodb://localhost:27017";

            var  mc = new MongoClient(conn);
           // var server = mc.Cluster.Description.Servers.FirstOrDefault();
            var database = mc.GetDatabase("test");
            //bool x = database.RunCommandAsyn
            //IMongoCollection<Developer> v = database.GetCollection<Developer>("kolekcja");

            //string json = "{_id:2,name:'John smith'company_name: 'Bloggy-dev',knowledge_base:[{langu_name: 'C#',technology: 'WPF',rating:'9'},{langu_name: 'C#',technology: 'TPL',rating:'7'},{langu_name: 'C++',technology: 'QT',rating:'8'}]}";

            // MongoDB.Bson.BsonDocument document
            //  = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

            //v.InsertOne(document);


            //Developer dev = v.Find<Developer>((FilterDefinition<Developer>.Empty)).Single();
            int a = 1;
           // database.CreateCollection("test");
            
            
        }








        public Widoki()
        {
            InitializeComponent();
            //RandomData.test();
            mong();
           






            List<String> list = new List<String>();

            BazyDanych2.BazyDB context = new BazyDanych2.BazyDB();
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            var tables = metadata.GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>()
                .Single()
                .BaseEntitySets
                .OfType<EntitySet>()
                .Where(s => !s.MetadataProperties.Contains("Type")
                || s.MetadataProperties["Type"].ToString() == "Tables");

            foreach (var table in tables)
            {
                var tableName = table.MetadataProperties.Contains("Table")
                    && table.MetadataProperties["Table"].Value != null
                    ? table.MetadataProperties["Table"].Value.ToString()
                    : table.Name;

                list.Add(tableName);
            }


            listView.ItemsSource = list;
            
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BazyDanych2.BazyDB mdb = new BazyDanych2.BazyDB();
            
            if (    (    ((ListView)(sender)).SelectedItem).Equals("gracz")            )
         
            dataGrid.ItemsSource = mdb.gracz.ToList();


            if ((((ListView)(sender)).SelectedItem).Equals("mecz"))

                dataGrid.ItemsSource = mdb.mecz.ToList();

           
            if ((((ListView)(sender)).SelectedItem).Equals("kolejka_statystyki_gracza"))

                dataGrid.ItemsSource = mdb.kolejka_statystyki_gracza.ToList();


            if ((((ListView)(sender)).SelectedItem).Equals("druzyna"))

                dataGrid.ItemsSource = mdb.druzyna.ToList();

            if ((((ListView)(sender)).SelectedItem).Equals("zapis_do_druzyny"))

                dataGrid.ItemsSource = mdb.zapis_do_druzyny.ToList();

            if ((((ListView)(sender)).SelectedItem).Equals("udzial"))

                dataGrid.ItemsSource = mdb.udzial.ToList();

            if ((((ListView)(sender)).SelectedItem).Equals("osiagniecia"))

                dataGrid.ItemsSource = mdb.osiagniecia.ToList();

            if ((((ListView)(sender)).SelectedItem).Equals("statystyki_meczu"))

                dataGrid.ItemsSource = mdb.statystyki_meczu.ToList();

            if ((((ListView)(sender)).SelectedItem).Equals("zdarzenie"))

                dataGrid.ItemsSource = mdb.zdarzenie.ToList();


            if ((((ListView)(sender)).SelectedItem).Equals("widok"))

                dataGrid.ItemsSource = mdb.widok.ToList();
            // mdb.Database.ExecuteSqlCommand("dodaj(@id,@nick,@steamid,@nazwakontawgrze)", id, nick, steam, nazwa);

        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int ind = dataGrid.SelectedIndex;

            DataTable x = new DataTable(listView.SelectedItem.ToString());
            BazyDanych2.BazyDB mdb = new BazyDanych2.BazyDB();
            
           
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            RandomData.play_match();
        }

   
    }
}

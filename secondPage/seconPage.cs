using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace urojaiWk.secondPage
{
    public partial class seconPage : Form
    {
        public seconPage()
        {
            InitializeComponent();
        }
        /*
* Microsoft Windows [Version 10.0.22631.4602]
* (c) TO "Олегохолдинг" (TO "Olegoholding"). All rights secure.
* License to use GNU-4.0
* Use, and have fun
*/

        private void seconPage_Load(object sender, EventArgs e)
        {

        }
        public static string connStr = "Server = 95.183.12.18; Port = 3306; Database=airlines; user=airUser; password=123";

        Dictionary<string, string> engNames = new Dictionary<string, string>
        {
                {"Билеты",$@"bileti"},
                {"Журнал",$@"journal"},
                {"Компании",$@"kompanii"},
                {"Рейсы",$@"reisi"}
        };
        Dictionary<string, string> inf = new Dictionary<string, string>
        {
                {"Билеты",$@"Совершить поиск стоимости"},
                {"Журнал",$@"Совершить поиск дате"},
                {"Компании",$@"Совершить поиск названию"},
                {"Рейсы",$@"Совершить поиск номеру имени рейса"}
        };
        Dictionary<string, string> tables = new Dictionary<string, string>
        {
                {"bileti",$@"{sqlTables.bileti}"},
                {"journal",$@"{sqlTables.journal}"},
                {"kompanii",$@"{sqlTables.kompanii}"},
                {"reisi",$@"{sqlTables.reisi}"}
        };
        private class sqlTables
        {
            public static string bileti = "SELECT bileti.id AS Номер, id_kompanii AS НомерКомпании, id_reisa AS НомерРейса, cost AS Цена FROM bileti";
            public static string journal = "SELECT journal.id AS Номер, id_kompanii AS НомерКомпании, id_reisa AS НомерРейса, id_bileta AS НомерБилета, data AS Дата FROM journal";
            public static string kompanii = "SELECT kompanii.id AS Номер, imyaKompanii AS ИмяКомпании FROM kompanii";
            public static string reisi = "SELECT reisi.id AS Номер, imyaReisa AS ИмяРейса, otkuda AS Откуда, kuda AS Куда, id_kompanii AS НомерКомпании FROM reisi";
            //public static string bileti = @"SELECT bileti.id AS Номер, kompanii.imyaKompanii AS НазваниеКомпании, reisi.imyaReisa AS НазваниеРейса, cost AS Цена FROM bileti";
            //LEFT JOIN kompanii ON id_kompanii = kompanii.id LEFT JOIN reisi ON id_reisa = reisi.id
            //public static string journal = @"SELECT journal.id AS Номер, kompanii.imyaKompanii AS НазваниеКомпании, reisi.imyaReisa AS НазваниеРейса, id_bileta AS НомерБилета, data AS Дата FROM journal";
            //LEFT JOIN kompanii ON id_kompanii = kompanii.id LEFT JOIN reisi ON id_reisa = reisi.id
            //public static string kompanii = "SELECT kompanii.id AS Номер, imyaKompanii AS НазваниеКомпании FROM kompanii";
            //public static string reisi = @"SELECT reisi.id AS Номер, imyaReisa AS ИмяРейса, otkuda AS Откуда, kuda AS Куда, kompanii.imyaKompanii AS ИмяКомпании FROM reisi";
            //LEFT JOIN kompanii ON id_kompanii = kompanii.id;
            public static string engNames;
            public static string temp;
        }
        private void tablesCm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rusNames = tablesCm.Text;

            engNames.TryGetValue(rusNames, out sqlTables.engNames);
            tables.TryGetValue(sqlTables.engNames, out string query);
            inf.TryGetValue(rusNames, out string inLbl);

            infLbl.Text = inLbl;
            sqlTables.temp = query;

            loadData(query);
        }
        private void loadData(string query)
        {
            using (MySqlConnection conn = new MySqlConnection($"{connStr}"))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGrid.DataSource = dataTable;
                        dataGrid.AutoGenerateColumns = true;
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.HResult.ToString());
                }
            }
        }

        private void dltBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["Номер"].Value.ToString());
                using (MySqlConnection connection = new MySqlConnection(connStr))
                {
                    string query = $"DELETE FROM {sqlTables.engNames} WHERE id = {ID}";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        loadData(sqlTables.temp);
                    }
                    catch (Exception ex)
                    {
                        loadData(sqlTables.temp);
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            tables.TryGetValue(sqlTables.engNames, out string query);

            var dataSet = new DataSet();
            var table = (DataTable)dataGrid.DataSource;

            table.TableName = sqlTables.engNames;
            dataSet.Tables.Add(table);

            //foreach (DataColumn column in table.Columns) { MessageBox.Show(column.ColumnName); }
#warning TO "Olehgoholding"
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    var adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(query, conn);
                    var builder = new MySqlCommandBuilder(adapter);

                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.Update(dataSet, sqlTables.engNames);
                }
                dataSet.Reset();
                loadData(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void srcBtn_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> tablesSearch = new Dictionary<string, string>
        {
                {"bileti",$@"{sqlTables.bileti} WHERE cost LIKE '{srcEdit.Text}%'"},
                {"journal",$@"{sqlTables.journal} WHERE data LIKE '{srcEdit.Text}%'"},
                {"kompanii",$@"{sqlTables.kompanii} WHERE imyaKompanii LIKE '{srcEdit.Text}%'"},
                {"reisi",$@"{sqlTables.reisi} WHERE imyaReisa LIKE '{srcEdit.Text}%'"}
        };
            tablesSearch.TryGetValue(sqlTables.engNames, out string query);
            loadData(query);
        }
    }
}


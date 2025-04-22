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
        public static string connStr = "Server = 95.183.12.18; Port = 3306; Database=sborWk; user=sborUser; password=123";

        Dictionary<string, string> engNames = new Dictionary<string, string>
        {
                {"Бригады",$@"brigadi"},
                {"Журнал",$@"journal"},
                {"Продукция",$@"produkciya"},
                {"Сборщики",$@"sborshiki"}
        };
        Dictionary<string, string> inf = new Dictionary<string, string>
        {
                {"Бригады",$@"Совершить поиск номеру бригадира"},
                {"Журнал",$@"Совершить поиск дате"},
                {"Продукция",$@"Совершить поиск названию"},
                {"Сборщики",$@"Совершить поиск номеру бригадира"}
        };
        Dictionary<string, string> tables = new Dictionary<string, string>
        {
                {"brigadi",$@"{sqlTables.brigadi}"},
                {"produkciya",$@"{sqlTables.produkciya}"},
                {"journal",$@"{sqlTables.journal}"},
                {"sborshiki",$@"{sqlTables.sborshiki}"}
        };
        private class sqlTables
        {
            public static string brigadi = "SELECT id AS Номер, nazvaniye AS Название, id_brigadira AS НомерБригадира FROM brigadi";
            public static string journal = "SELECT id AS Номер, id_produkciya AS НомерПродукции, id_brigadi AS НомерБригады, data AS Дата FROM journal";
            public static string produkciya = "SELECT id AS Номер, name AS НомерПродукции, type AS ТипПродукции FROM produkciya";
            public static string sborshiki = "SELECT id AS Номер, familiya AS Фамилия, imya AS Имя, id_brigada AS НомерБригады FROM sborshiki";
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
                {"brigadi",$@"{sqlTables.brigadi} WHERE id_brigadira LIKE '{srcEdit.Text}%'"},
                {"produkciya",$@"{sqlTables.produkciya} WHERE name LIKE '{srcEdit.Text}%'"},
                {"journal",$@"{sqlTables.journal} WHERE data LIKE '{srcEdit.Text}%'"},
                {"sborshiki",$@"{sqlTables.sborshiki} WHERE id_brigada LIKE '{srcEdit.Text}%'"}
        };
            tablesSearch.TryGetValue(sqlTables.engNames, out string query);
            loadData(query);
        }
    }
}


using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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
        public static string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\radie\\OneDrive\\Рабочий стол\\vovaBd.accdb";

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
            public static string produkciya = "SELECT id AS Номер, imya AS ИмяПродукции, type AS ТипПродукции FROM produkciya";
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

            LoadData(query);
        }
        private void LoadData(string query)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (OleDbCommand command = new OleDbCommand(query, conn))
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGrid.DataSource = dataTable;
                        dataGrid.AutoGenerateColumns = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.HResult.ToString());
                }
            }
        }

        private void DltBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["Номер"].Value.ToString());
                using (OleDbConnection connection = new OleDbConnection(connStr))
                {
                    string query = $"DELETE FROM {sqlTables.engNames} WHERE id = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", ID);
                        try
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            LoadData(sqlTables.temp);
                        }
                        catch (Exception ex)
                        {
                            LoadData(sqlTables.temp);
                            MessageBox.Show($"Ошибка: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            tables.TryGetValue(sqlTables.engNames, out string query);

            var dataSet = new DataSet();
            var table = (DataTable)dataGrid.DataSource;

            table.TableName = sqlTables.engNames;
            dataSet.Tables.Add(table);

            try
            {
                using (var conn = new OleDbConnection(connStr))
                {
                    var adapter = new OleDbDataAdapter();
                    adapter.SelectCommand = new OleDbCommand(query, conn);
                    var builder = new OleDbCommandBuilder(adapter);

                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.Update(dataSet, sqlTables.engNames);
                }
                dataSet.Reset();
                LoadData(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SrcBtn_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> tablesSearch = new Dictionary<string, string>
        {
            {"brigadi", $"{sqlTables.brigadi} WHERE id_brigadira LIKE ?"},
            {"produkciya", $"{sqlTables.produkciya} WHERE imya LIKE ?"},
            {"journal", $"{sqlTables.journal} WHERE data LIKE ?"},
            {"sborshiki", $"{sqlTables.sborshiki} WHERE id_brigada LIKE ?"}
        };

            tablesSearch.TryGetValue(sqlTables.engNames, out string query);
            query = query.Replace("?", $"'{srcEdit.Text}%'");

            LoadData(query);
        }
    }
}


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
        public static string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\radie\\OneDrive\\Рабочий стол\\bddb.accdb";

        Dictionary<string, string> engNames = new Dictionary<string, string>
        {
                {"Спортсмены",$@"sportsmen"},
                {"Стадионы",$@"stadion"},
                {"Виды спорта",$@"vidiSporta"},
                {"Журнал выступлений",$@"journal"}
        };
        Dictionary<string, string> inf = new Dictionary<string, string>
        {
                {"Спортсмены",$@"Совершить поиск фамилия"},
                {"Виды спорта",$@"Совершить поиск названию"},
                {"Стадионы",$@"Совершить поиск названию"},
                {"Журнал выступлений",$@"Совершить поиск по дате"}
        };
        Dictionary<string, string> tables = new Dictionary<string, string>
        {
                {"sportsmen",$@"{sqlTables.sportsmen}"},
                {"stadion",$@"{sqlTables.stadion}"},
                {"journal",$@"{sqlTables.journal}"},
                {"vidiSporta",$@"{sqlTables.vidiSporta}"}
        };

        private class sqlTables
        {
            public static string stadion = "SELECT id AS Номер, nazvaniye AS Название, adress AS Адресс FROM stadion";
            public static string journal = "SELECT journal.id AS Номер, id_sportsmena AS НомерСпортсмена, id_stadiona AS НомерСтадиона, mesto AS МестоУчастия ,data AS Дата FROM journal";
            public static string vidiSporta = "SELECT id AS Номер, nazvaniye AS Название FROM vidiSporta";
            public static string sportsmen = "SELECT sportsmen.id AS Номер, familiya AS Фамилия, imya AS Имя, id_vidSporta AS НомерВидаСпорта FROM sportsmen";
            public static string sportsmenJoin = "SELECT sportsmen.id AS Номер, familiya AS Фамилия, imya AS Имя, vidiSporta.nazvaniye AS ВидСпорта FROM (sportsmen LEFT JOIN vidiSporta ON sportsmen.id_vidSporta = vidiSporta.id)";
            public static string stadionJoin = "SELECT journal.id AS Номер, stadion.nazvaniye AS НазваниеСтадиона, sportsmen.familiya AS Фамилия, mesto AS МестоУчастия, data AS ДатаУчастия FROM ((journal LEFT JOIN stadion ON journal.id_stadiona = stadion.id) LEFT JOIN sportsmen ON journal.id_sportsmena = sportsmen.id)";
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

            checkBox1.Checked = false;

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
            {"sportsmen", $"{sqlTables.sportsmen} WHERE imya LIKE ?"},
            {"stadion", $"{sqlTables.stadion} WHERE nazvaniye LIKE ?"},
            {"journal", $"{sqlTables.journal} WHERE data LIKE ?"},
            {"vidiSporta", $"{sqlTables.vidiSporta} WHERE nazvaniye LIKE ?"}
        };

            tablesSearch.TryGetValue(sqlTables.engNames, out string query);
            query = query.Replace("?", $"'{srcEdit.Text}%'");

            LoadData(query);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string rusNames = tablesCm.Text;
            engNames.TryGetValue(rusNames, out sqlTables.engNames);
            

            if (checkBox1.Checked)
            {
                var tables = new Dictionary<string, string>
                {
                    {"sportsmen",$@"{sqlTables.sportsmenJoin}"},
                    {"journal",$@"{sqlTables.stadionJoin}"}
                };
                tables.TryGetValue(sqlTables.engNames, out string query);
                MessageBox.Show(query);
                LoadData(query);
            }
        }
    }
}


using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Data.SQLite;

namespace Lab9
{
    public partial class MainWindow : Window
    {
        private string connectionString = @"Data Source=WnioskiDB.db; Version=3;";
        public MainWindow()
        {
            InitializeComponent();
            new DatabaseManager();

        }
        public class WniosekItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; } 
        }

        public void RefreshList()
        {
            try
            {
                var items = new System.Collections.Generic.List<WniosekItem>();
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, ImieNazwisko, NumerAlbumu FROM Wnioski ORDER BY Id DESC";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new WniosekItem
                            {
                                Id = reader.GetInt32(0),
                                DisplayText = $"{reader.GetString(1)} (Album: {reader.GetString(2)})"
                            });
                        }
                    }
                }
                lstWnioski.ItemsSource = items;
            }
            catch (Exception ex) { txtStatus.Text = "Błąd listy: " + ex.Message; }
        }

        public void BtnRefresh_Click(object sender, RoutedEventArgs e) => RefreshList();

        private void LstWnioski_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstWnioski.SelectedItem is WniosekItem selected)
            {
                LoadWniosekById(selected.Id);
            }
        }

        private void LoadWniosekById(int id)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Wnioski WHERE Id = @id";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtDataWniosku.Text = reader.GetString(1);
                                txtNumerAlbumu.Text = reader.GetString(2);
                                txtImieNazwisko.Text = reader.GetString(3);
                                txtSemestr.Text = reader.GetString(4);
                                txtRok.Text = reader.GetString(5);
                                txtKierunek.Text = reader.GetString(6);
                                txtStopien.Text = reader.GetString(7);
                                txtPrzedmiot.Text = reader.GetString(8);
                                txtPunkty.Text = reader.GetString(9);
                                txtProwadzacy.Text = reader.GetString(10);
                                txtUzasadnienie.Text = reader.GetString(11);
                                txtPodpisStudenta.Text = reader.GetString(12);
                                txtDecyzja.Text = reader.GetString(13);
                                txtSkladKomisji.Text = reader.GetString(14);
                                txtDataDecyzji.Text = reader.GetString(15);
                                txtStatus.Text = "Status: Wczytano wybrany wniosek!";
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { txtStatus.Text = "Błąd: " + ex.Message; }
        }
        public void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO Wnioski 
                        (DataWniosku, NumerAlbumu, ImieNazwisko, Semestr, Rok, Kierunek, Stopien, Przedmiot, Punkty, Prowadzacy, Uzasadnienie, PodpisStudenta, Decyzja, SkladKomisji, DataDecyzji) 
                        VALUES 
                        (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@p1", txtDataWniosku.Text ?? "");
                        command.Parameters.AddWithValue("@p2", txtNumerAlbumu.Text ?? "");
                        command.Parameters.AddWithValue("@p3", txtImieNazwisko.Text ?? "");
                        command.Parameters.AddWithValue("@p4", txtSemestr.Text ?? "");
                        command.Parameters.AddWithValue("@p5", txtRok.Text ?? "");
                        command.Parameters.AddWithValue("@p6", txtKierunek.Text ?? "");
                        command.Parameters.AddWithValue("@p7", txtStopien.Text ?? "");
                        command.Parameters.AddWithValue("@p8", txtPrzedmiot.Text ?? "");
                        command.Parameters.AddWithValue("@p9", txtPunkty.Text ?? "");
                        command.Parameters.AddWithValue("@p10", txtProwadzacy.Text ?? "");
                        command.Parameters.AddWithValue("@p11", txtUzasadnienie.Text ?? "");
                        command.Parameters.AddWithValue("@p12", txtPodpisStudenta.Text ?? "");
                        command.Parameters.AddWithValue("@p13", txtDecyzja.Text ?? "");
                        command.Parameters.AddWithValue("@p14", txtSkladKomisji.Text ?? "");
                        command.Parameters.AddWithValue("@p15", txtDataDecyzji.Text ?? "");

                        command.ExecuteNonQuery(); 
                    }
                }
                txtStatus.Text = "Status: Pomyślnie zapisano do bazy!";
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Błąd zapisu: " + ex.Message;
            }
        }

        public void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Wnioski ORDER BY Id DESC LIMIT 1";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtDataWniosku.Text = reader.GetString(1);
                            txtNumerAlbumu.Text = reader.GetString(2);
                            txtImieNazwisko.Text = reader.GetString(3);
                            txtSemestr.Text = reader.GetString(4);
                            txtRok.Text = reader.GetString(5);
                            txtKierunek.Text = reader.GetString(6);
                            txtStopien.Text = reader.GetString(7);
                            txtPrzedmiot.Text = reader.GetString(8);
                            txtPunkty.Text = reader.GetString(9);
                            txtProwadzacy.Text = reader.GetString(10);
                            txtUzasadnienie.Text = reader.GetString(11);
                            txtPodpisStudenta.Text = reader.GetString(12);
                            txtDecyzja.Text = reader.GetString(13);
                            txtSkladKomisji.Text = reader.GetString(14);
                            txtDataDecyzji.Text = reader.GetString(15);

                            txtStatus.Text = "Status: Wczytano ostatni wniosek!";
                        }
                        else
                        {
                            txtStatus.Text = "Status: Baza jest pusta.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Błąd odczytu: " + ex.Message;
            }
        }
    }
}
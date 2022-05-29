﻿using System;
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
using System.Data.SqlClient;
using System.Data;

namespace datagridbindingtextbox
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

       
        public void LoadGrid()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-LA36CLI\SQLEXPRESS;Initial Catalog=asd;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("select * from final", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            datagridenes.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void ekle_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-LA36CLI\SQLEXPRESS;Initial Catalog=asd;Integrated Security=True");
            SqlCommand cmd;

            if (name.Text != "" && surname.Text != "")
            {
                cmd = new SqlCommand("insert into final(name,surname) values(@name,@surname)", con);
                con.Open();

                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@surname", surname.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarıyla Eklendi");
                LoadGrid();
                ClearData();
            }
            else
            {
                MessageBox.Show("Lütfen Detayları Sağlayın!");
            }
        }

        private void ClearData()
        {
            name.Text = "";
            surname.Text = "";
        }

        private void datagridenes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void datagridenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  DataItem dataitem = datagridenes.SelectedItem as DataItem;
            var enes = this.datagridenes.CurrentItem as DataRowView;
            if(enes != null)
            {
                var ded = (enes as DataRowView).Row;
                name.Text = ded["name"].ToString();
                surname.Text = ded["surname"].ToString();
                idtext.Text = ded["id"].ToString();
            }
            

            
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void gridName_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void idtext_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-LA36CLI\SQLEXPRESS;Initial Catalog=asd;Integrated Security=True");
            SqlCommand cmd;
            if (name.Text != "" && surname.Text != "")
            {
                cmd = new SqlCommand("update final set name=@name,surname=@surname where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", idtext.Text);
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@surname", surname.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarıyla Güncellendi");
                con.Close();
                LoadGrid();
                ClearData();
            }
            else
            {
                MessageBox.Show("Lütfen Güncellenecek Kaydı Seçin");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-LA36CLI\SQLEXPRESS;Initial Catalog=asd;Integrated Security=True");
            SqlCommand cmd;
            if (idtext.Text != null)
            {
                cmd = new SqlCommand("delete final where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", idtext.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarıyla Silindi!");
                LoadGrid();
                ClearData();
            }
            else
            {
                MessageBox.Show("Lütfen Silinecek Kaydı Seçiniz");
            }
        }
    }

    internal class DataItem
    {

        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

    }
}
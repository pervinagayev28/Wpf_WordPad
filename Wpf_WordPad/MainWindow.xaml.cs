using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Wpf_WordPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string? copy = default;
        public MainWindow()
        {
            InitializeComponent();
        }




        private void txtName_click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "button2" && butto1.Content.ToString() != "Show fiels")
            {
                partOfText.Text = File.ReadAllText(butto1.Content.ToString());
                return;
            }
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            string filename = default;
            if (openFileDialog.ShowDialog() == true)
            {
                butto1.Content = openFileDialog.FileName;

            }

        }

        private void button_save_click(object sender, RoutedEventArgs e)
        {
            if (partOfText.Text != "")
            {
                SaveFileDialog saveFileDialog = new();
                saveFileDialog.FilterIndex = 1;
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;
                    File.WriteAllText(filePath, partOfText.Text);
                }
            }
        }
        private void WhenPartOfTextChanged(object s, EventArgs e) => File.WriteAllText(butto1.Content.ToString(), partOfText.Text);
        private void button_auto_save_click(object sender, RoutedEventArgs e)
        {
            if (butto1.Content != "Show fiels" && button_auto_save.Background != Brushes.Green)
            {
                button_auto_save.Background = Brushes.Green;
                border_auto_save.Background = Brushes.Green;
                partOfText.TextChanged += WhenPartOfTextChanged;
            }
            else
            {
                border_auto_save.Background = Brushes.White;
                button_auto_save.Background = Brushes.White;
                partOfText.TextChanged -= WhenPartOfTextChanged;

            }
        }

        private void button_cut_click(object sender, RoutedEventArgs e)
        {
            var data = partOfText.Text.ToString();
            partOfText.Text = data.Remove(data.IndexOf(partOfText.SelectedText), partOfText.SelectedText.ToString().Length);
        }

        private void button_copy_click(object sender, RoutedEventArgs e)
        {
            if (partOfText.SelectedText.ToString() != "")
                copy = partOfText.SelectedText.ToString();
        }

        private void button_paste_click(object sender, RoutedEventArgs e)
        {
            partOfText.Text = partOfText.Text.Insert(partOfText.SelectionStart, copy != null ? copy : "");
        }

        private void button_selectAll_click(object sender, RoutedEventArgs e)
        {
            if (partOfText.SelectedText.Length < partOfText.Text.Length)
            {
                partOfText.Focus();
                partOfText.Select(0, partOfText.Text.Length); ;
            }
            else
                partOfText.Select(0, 0);


        }
    }
}

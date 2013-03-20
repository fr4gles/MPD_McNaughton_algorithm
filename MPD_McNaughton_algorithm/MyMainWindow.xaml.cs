using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms.Integration;

namespace MPD_McNaughton_algorithm
{
    /// <summary>
    /// Interaction logic for MyMainWindow.xaml
    /// </summary>
    public partial class MyMainWindow : Window
    {
        public MyMainWindow()
        {
            InitializeComponent();

            wfHost = new WindowsFormsHost();

            var tmp = new ObservableCollection<ZadaniaItems>();
            tmp.Add(new ZadaniaItems() { Zadanie = "Z1", CzasWykonania = 2 });
            tmp.Add(new ZadaniaItems() { Zadanie = "Z2", CzasWykonania = 5 });
            tmp.Add(new ZadaniaItems() { Zadanie = "Z3", CzasWykonania = 3 });
            tmp.Add(new ZadaniaItems() { Zadanie = "Z4", CzasWykonania = 7 });


//            dataGrid.Columns.Add(new DataGridTextColumn()
//            {
//                Header = "Zadanie:",
//                Binding = new Binding("Path=Zadanie.Nr")
//            });
//
//            dataGrid.Columns.Add(new DataGridTextColumn()
//            {
//                Header = "Czas wykonania:",
//                Binding = new Binding("Path=Zadanie.CzasWykonania")
//            });

            dataGrid.ItemsSource = tmp;
            //dataGrid.ItemStringFormat = "{}{0:N0}";

//            dataGrid.Columns[0].IsReadOnly = true;
        }

//        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
//        {
//            //dataGrid.Items.Add(new ZadaniaItems() { Zadanie = "Z4", CzasWykonania = 7 });
//        }


    }

    class ZadaniaItems
    {
        public String Zadanie { get; set; }
        public int CzasWykonania { get; set; }
    }
}

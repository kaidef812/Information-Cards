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
using System.Windows.Shapes;
using InformationCards_Client.ViewModel;

namespace InformationCards_Client.View
{
    /// <summary>
    /// Логика взаимодействия для EditCardWindow.xaml
    /// </summary>
    public partial class EditCardWindow : Window
    {
        public EditCardWindow(int id)
        {
            InitializeComponent();
            DataContext = new EditCardViewModel(id, this);
        }
    }
}

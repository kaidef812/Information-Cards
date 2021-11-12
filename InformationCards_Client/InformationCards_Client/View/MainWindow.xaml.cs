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
using InformationCards_Client.ViewModel;
using InformationCards_Client.Model;
using System.IO;

namespace InformationCards_Client.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(this, CardsPanel);
        }

        public void SetSelectedColor(Grid card)
        {
            card.Background = new SolidColorBrush(Colors.LightGray);
        }

        public void SetUnselectedColor(Grid card)
        {
            card.Background = new SolidColorBrush(Colors.Transparent);
        }

        public void AddCards(IEnumerable<object> cards)
        {
            CardsPanel.Children.Clear();
            foreach (var card in cards)
            {
                var cardGrid = CreateCard(card as BookCard);
                CardsPanel.Children.Add(cardGrid);
            }
        }

        private Grid CreateCard(BookCard bookCard)
        {
            Grid cardGrid = new Grid();

            cardGrid.Style = (Style)Resources["CardGrid"];

            ColumnDefinition columnImage = new ColumnDefinition();
            ColumnDefinition columnName = new ColumnDefinition();
            columnImage.Width = new GridLength(250, GridUnitType.Pixel);

            cardGrid.ColumnDefinitions.Add(columnImage);
            cardGrid.ColumnDefinitions.Add(columnName);

            Image cardImage = new Image();
            cardImage.Style = (Style)Resources["CardImage"];
            using (MemoryStream stream = new MemoryStream(bookCard.Image))
            {
                cardImage.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
            cardGrid.Children.Add(cardImage);

            TextBlock cardName = new TextBlock();
            cardName.Style = (Style)Resources["CardText"];
            cardName.Text = bookCard.Name;
            cardGrid.Children.Add(cardName);

            Grid.SetColumn(cardImage, 0);
            Grid.SetColumn(cardName, 1);

            MouseBinding mouseBinding = new MouseBinding();
            MouseGesture leftClickGesture = new MouseGesture();
            leftClickGesture.MouseAction = MouseAction.LeftClick;
            mouseBinding.Gesture = leftClickGesture;
            mouseBinding.Command = ((dynamic)this.DataContext).SelectCard;
            mouseBinding.CommandParameter = cardGrid;
            cardGrid.InputBindings.Add(mouseBinding);

            return cardGrid;
        }
    }
}

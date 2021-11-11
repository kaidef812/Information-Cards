using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using InformationCards_Client.Model;
using InformationCards_Client.View;

namespace InformationCards_Client.ViewModel
{
    class MainViewModel
    {
        public MainViewModel(MainWindow window, StackPanel stackPanel)
        {
            Cards = new List<BookCard>();
            _mainWindow = window;
            _cardsPanel = stackPanel;
        }

        private MainWindow _mainWindow;

        private StackPanel _cardsPanel;

        private BookCard _selectedCard;

        private RelayCommand _selectCard;
        public RelayCommand SelectCard => Select();
        private RelayCommand Select()
        {
            return _selectCard ?? new RelayCommand(obj =>
            {
                SetSelectedCard(obj);
            });
        }

        private void SetSelectedCard(object obj)
        {
            var selectedCard = obj as Grid;
            int selectedNumber = _cardsPanel.Children.IndexOf(selectedCard);
            _selectedCard = Cards[selectedNumber];
            _mainWindow.ChangeBackground(selectedCard);
        }

        #region GET
        public List<BookCard> Cards { get; set; }

        private RelayCommand _getCards;
        public RelayCommand GetCards => TryGetCards();
        private RelayCommand TryGetCards()
        {
            return _getCards ?? new RelayCommand(obj =>
            {
                var list = GetCardsFromServer();
                UpdateCardsList(list);
                FillViewCards();
            });
        }

        private List<BookCard> GetCardsFromServer()
        {
            HttpRequests httpRequests = new HttpRequests();
            Task<List<BookCard>> cardsFromRequest = Task.Run(() => httpRequests.GetCards());
            cardsFromRequest.Wait();

            return cardsFromRequest.Result;
        }

        private void UpdateCardsList(IEnumerable<BookCard> cardsCollection)
        {
            Cards.Clear();
            Cards.AddRange(cardsCollection);
        }

        private void FillViewCards()
        {
            _mainWindow.AddCards(Cards);
        }
        #endregion

        #region POST

        private RelayCommand _postCard;
        public RelayCommand PostCard => TryPostCard();
        private RelayCommand TryPostCard()
        {
            return _postCard ?? new RelayCommand(obj =>
            {
                PostNewCard();
            });
        }

        private void PostNewCard()
        {
            AddCardWindow addCardWindow = new AddCardWindow();
            addCardWindow.ShowDialog();
        }
        #endregion

        #region PUT
        private RelayCommand _putCard;
        public RelayCommand PutCard => TryPutCard();
        private RelayCommand TryPutCard()
        {
            return _putCard ?? new RelayCommand(obj =>
            {
                EditCardWindow editCardWindow = new EditCardWindow(_selectedCard.Id);
                editCardWindow.ShowDialog();
                var newList = GetCardsFromServer();
                UpdateCardsList(newList);
                FillViewCards();
            });
        }
        #endregion

        #region DELETE
        private RelayCommand _deleteCard;
        public RelayCommand DeleteCard => TryDeleteCard();
        private RelayCommand TryDeleteCard()
        {
            return _deleteCard ?? new RelayCommand(obj =>
            {
                DeleteCardFromServer(_selectedCard);
                var newCardList = GetCardsFromServer();
                UpdateCardsList(newCardList);
                FillViewCards();
            });
        }

        private void DeleteCardFromServer(BookCard bookCard)
        {
            HttpRequests httpRequests = new HttpRequests();
            Task postCard = Task.Run(() => httpRequests.DeleteCard(bookCard.Id));
            postCard.Wait();
        }
        #endregion
    }
}

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
            _selectedCards = new List<BookCard>();
            _mainWindow = window;
            _cardsPanel = stackPanel;
        }

        private MainWindow _mainWindow;

        #region SELECT CARD
        private StackPanel _cardsPanel;

        private List<BookCard> _selectedCards;

        private RelayCommand _selectCard;
        public RelayCommand SelectCard => Select();
        private RelayCommand Select()
        {
            return _selectCard ?? new RelayCommand(obj =>
            {
                UpdateSelectedCard(obj);
            });
        }

        private void UpdateSelectedCard(object obj)
        {
            var selectedCard = obj as Grid;
            int selectedNumber = _cardsPanel.Children.IndexOf(selectedCard);
            var buffer = Cards[selectedNumber];
            if (_selectedCards.Contains(buffer))
            {
                _selectedCards.Remove(buffer);
                _mainWindow.SetUnselectedColor(selectedCard);
            }
            else
            {
                _selectedCards.Add(buffer);
                _mainWindow.SetSelectedColor(selectedCard);
            }
        }
        #endregion

        #region GET
        public List<BookCard> Cards { get; set; }

        private RelayCommand _getCards;
        public RelayCommand GetCards => TryGetCards();
        private RelayCommand TryGetCards()
        {
            return _getCards ?? new RelayCommand(obj =>
            {
                var list = GetCardsFromServer();
                if (list != null)
                {
                    UpdateCardsList(list);
                    FillViewCards();
                    _selectedCards.Clear();
                }
                else
                {
                    MessageBox.Show("There is nothing to receive from the server.");
                }
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
            Cards = Cards.OrderBy(c => c.Name).ToList();
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
                var newList = GetCardsFromServer();
                UpdateCardsList(newList);
                FillViewCards();
                _selectedCards.Clear();
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
                if (_selectedCards.Count == 1)
                {
                    EditCardWindow editCardWindow = new EditCardWindow(_selectedCards.First().Id);
                    editCardWindow.ShowDialog();
                    var newList = GetCardsFromServer();
                    UpdateCardsList(newList);
                    FillViewCards();
                    _selectedCards.Clear();
                }
                else
                {
                    MessageBox.Show($"Selected cards: {_selectedCards.Count}; Select 1 card!");
                }
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
                foreach (var card in _selectedCards)
                {
                    DeleteCardFromServer(card);
                }
                var newCardList = GetCardsFromServer();
                UpdateCardsList(newCardList);
                FillViewCards();
                _selectedCards.Clear();
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

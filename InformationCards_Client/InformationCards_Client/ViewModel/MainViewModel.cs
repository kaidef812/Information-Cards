using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformationCards_Client.Model;

namespace InformationCards_Client.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Cards = new List<BookCard>();
        }

        #region GET
        public List<BookCard> Cards { get; set; }

        private RelayCommand _getCards;
        public RelayCommand GetCards => TryGetCards();
        private RelayCommand TryGetCards()
        {
            return _getCards ?? new RelayCommand(obj =>
            {
                GetCardsFromServer();
            });
        }

        private void GetCardsFromServer()
        {
            HttpRequests httpRequests = new HttpRequests();
            Task<List<BookCard>> cardsFromRequest = Task.Run(() => httpRequests.GetCards());
            cardsFromRequest.Wait();

            FillViewCards(cardsFromRequest.Result);
        }

        private void FillViewCards(IEnumerable<BookCard> cardsCollection)
        {
            NotifyPropertyChanged(nameof(Cards));
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
            BookCard bookCard = new BookCard();
            bookCard.Name = "Test";

            HttpRequests httpRequests = new HttpRequests();
            Task postCard = Task.Run(() => httpRequests.PostCard(bookCard));
            postCard.Wait();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

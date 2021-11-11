using InformationCards_Client.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using InformationCards_Client.View;

namespace InformationCards_Client.ViewModel
{
    class EditCardViewModel: INotifyPropertyChanged
    {
        public EditCardViewModel(int id, EditCardWindow editCardWindow)
        {
            _changingCard = GetSelectedCard(id);
            NameCard = _changingCard.Name;
            NotifyPropertyChanged(nameof(NameCard));
            ImageCard = GetImage();
            NotifyPropertyChanged(nameof(ImageCard));
            _editCardWindow = editCardWindow;
        }

        private EditCardWindow _editCardWindow;

        private BookCard _changingCard { get; set; }
        public Image ImageCard { get; set; }
        public string NameCard { get; set; }

        private BookCard GetSelectedCard(int id)
        {
            HttpRequests httpRequests = new HttpRequests();
            Task<List<BookCard>> cardsFromRequest = Task.Run(() => httpRequests.GetCards());
            cardsFromRequest.Wait();
            return cardsFromRequest.Result.Find(c => c.Id == id);
        }

        private Image GetImage()
        {
            Image cardImage = new Image();
            using (MemoryStream stream = new MemoryStream(_changingCard.Image))
            {
                cardImage.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
            return cardImage;
        }

        private RelayCommand _saveEdit;
        public RelayCommand SaveEdit => TrySave();
        private RelayCommand TrySave()
        {
            return _saveEdit ?? new RelayCommand(obj =>
            {
                SaveChanges();
                _editCardWindow.DialogResult = true;
            });
        }

        private void SaveChanges()
        {
            _changingCard.Name = NameCard;
            HttpRequests httpRequests = new HttpRequests();
            Task postCard = Task.Run(() => httpRequests.PutCard(_changingCard));
            postCard.Wait();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

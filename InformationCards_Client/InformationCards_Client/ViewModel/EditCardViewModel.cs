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
using Microsoft.Win32;

namespace InformationCards_Client.ViewModel
{
    class EditCardViewModel: INotifyPropertyChanged
    {
        public EditCardViewModel(int id, EditCardWindow editCardWindow)
        {
            _changingCard = GetSelectedCard(id);
            NameCard = _changingCard.Name;
            NotifyPropertyChanged(nameof(NameCard));
            ImageCardSource = CreateImage();
            NotifyPropertyChanged(nameof(ImageCardSource));
            _editCardWindow = editCardWindow;
        }

        private EditCardWindow _editCardWindow;

        private BookCard _changingCard { get; set; }
        public BitmapFrame ImageCardSource { get; set; }
        public string NameCard { get; set; }

        private BookCard GetSelectedCard(int id)
        {
            HttpRequests httpRequests = new HttpRequests();
            Task<List<BookCard>> cardsFromRequest = Task.Run(() => httpRequests.GetCards());
            cardsFromRequest.Wait();
            return cardsFromRequest.Result.Find(c => c.Id == id);
        }

        private BitmapFrame CreateImage()
        {
            using (MemoryStream stream = new MemoryStream(_changingCard.Image))
            {
                var frame = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                return frame;
            }
        }

        private byte[] _image;

        private RelayCommand _uploadImage;
        public RelayCommand UploadImage => TryUploadImage();
        private RelayCommand TryUploadImage()
        {
            return _uploadImage ?? new RelayCommand(obj =>
            {
                _changingCard.Image = ReadImage();
                ImageCardSource = CreateImage();
                NotifyPropertyChanged(nameof(ImageCardSource));
            });
        }

        private byte[] ReadImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*png";
            dlg.ShowDialog();
            dlg.Multiselect = false;
            using (Stream stream = dlg.OpenFile())
            {
                BinaryReader binary = new BinaryReader(stream);
                byte[] file = binary.ReadBytes((int)stream.Length);
                return file;
            }
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

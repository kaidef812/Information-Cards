using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationCards_Client.Model;
using InformationCards_Client.View;
using Microsoft.Win32;

namespace InformationCards_Client.ViewModel
{
    class AddCardViewModel
    {
        public AddCardViewModel(AddCardWindow window)
        {
            _currentWindow = window;
        }

        private AddCardWindow _currentWindow;

        public string InputName { get; set; }
        private byte[] _image;

        private RelayCommand _uploadImage;
        public RelayCommand UploadImage => TryUploadImage();
        private RelayCommand TryUploadImage()
        {
            return _uploadImage ?? new RelayCommand(obj =>
            {
                _image = ReadImage();
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

        private RelayCommand _addCard;
        public RelayCommand AddCard => TryAddCard();
        private RelayCommand TryAddCard()
        {
            return _addCard ?? new RelayCommand(obj =>
            {
                BookCard newCard = CreateNewCard();

                HttpRequests httpRequests = new HttpRequests();
                Task postCard = Task.Run(() => httpRequests.PostCard(newCard));
                postCard.Wait();
                _currentWindow.DialogResult = true;
            });
        }

        private BookCard CreateNewCard()
        {
            return new BookCard() { Id = GetNewId(), Name = InputName, Image = _image };
        }

        private int GetNewId()
        {
            HttpRequests httpRequests = new HttpRequests();
            Task<List<BookCard>> cardsFromRequest = Task.Run(() => httpRequests.GetCards());
            cardsFromRequest.Wait();
            int newId = 0;
            var list = cardsFromRequest.Result;
            while(list.Find(c => c.Id == newId) != null)
            {
                newId++;
            }
            return newId;
        }
    }
}

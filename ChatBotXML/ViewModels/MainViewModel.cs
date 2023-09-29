using ChatBotXML.Models;
using ChatBotXML.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ChatBotXML.Utils;

namespace ChatBotXML.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Sentiment SelectedSentiment { get; set; }
        public ObservableCollection<Sentiment> Sentiments { get; set; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            // Load the messages from the XML file
            MessageService messageService = new MessageService();
            Sentiments = new ObservableCollection<Sentiment>(messageService.LoadMessagesFromXml("messages.xml"));

            DeleteCommand = new RelayCommand<Message>(DeleteMessage, CanDeleteMessage);
        }

        private bool CanDeleteMessage(Message message)
        {
            return message != null;
        }

        private void DeleteMessage(Message message)
        {
            Sentiment sentimentContainingMessage = Sentiments.First(s => s.Messages.Contains(message));

            if (sentimentContainingMessage != null)
            {
                sentimentContainingMessage.Messages.Remove(message);

                // Update the XML file
                MessageService messageService = new MessageService();
                messageService.RemoveMessageFromXml("messages.xml", message);

                OnPropertyChanged(nameof(Sentiments));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
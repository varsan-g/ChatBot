using ChatBotXML.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ChatBotXML.Services
{
    public class MessageService
    {
        public List<Sentiment> LoadMessagesFromXml(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            List<Sentiment> sentiments = doc.Element("messages")
                .Elements()
                .Select(sentimentElement => new Sentiment
                {
                    Title = sentimentElement.Name.LocalName,
                    Messages = LoadMessagesFromElement(sentimentElement)
                })
                .ToList();

            return sentiments;
        }

        private List<Message> LoadMessagesFromElement(XElement sentimentElement)
        {
            List<Message> messages = sentimentElement.Elements("message")
                .Select(messageElement => new Message
                {
                    Content = messageElement.Value
                })
                .ToList();

            return messages;
        }

        public void RemoveMessageFromXml(string filePath, Message message)
        {
            XDocument doc = XDocument.Load(filePath);
            XElement messageElement = doc.Descendants("message")
                .FirstOrDefault(m => m.Value == message.Content);

            if (messageElement != null)
            {
                messageElement.Remove();
                doc.Save(filePath);
            }
        }
    }
}
using System.Collections.Generic;
using Xunit;

namespace design_patterns
{
    public class Observer
    {
        [Theory]
        [InlineData("logData", "log", "logData", "")]
        [InlineData("emailData", "email", "", "emailData")]
        public void EditorTest(string data, string eventType, string logData, string emailData)
        {
            var logEventListener = new LogEventListener();
            var emailEventListener = new EmailEventListener();

            var editor = new Editor();
            editor.Events.Subscribe("log", logEventListener);
            editor.Events.Subscribe("email", emailEventListener);

            if (eventType == "log")
                editor.Log(data);
            else if (eventType == "email")
                editor.Email(data);

            Assert.Equal(logData, logEventListener.Date);
            Assert.Equal(emailData, emailEventListener.Date);
        }
    }

    public class Editor
    {
        public readonly EventManager Events = new();

        public void Log(string data)
        {
            Events.Notify("log", data);
        }

        public void Email(string data)
        {
            Events.Notify("email", data);
        }
    }

    public class EventManager
    {
        private readonly Dictionary<string, List<IEventListener>> _events = new();

        public void Subscribe(string type, IEventListener listener)
        {
            if (!_events.ContainsKey(type))
                _events[type] = new List<IEventListener>();

            _events[type].Add(listener);
        }

        public void UnSubscribe(string type, IEventListener listener)
        {
            if (!_events.ContainsKey(type))
                return;

            _events[type].Remove(listener);
        }

        public void Notify(string type, string data)
        {
            if (!_events.ContainsKey(type))
                return;

            foreach (var eventListener in _events[type])
                eventListener.Update(data);
        }
    }

    public interface IEventListener
    {
        void Update(string data);
    }

    public class LogEventListener : IEventListener
    {
        public string Date = "";

        public void Update(string data)
        {
            Date = data;
        }
    }

    public class EmailEventListener : IEventListener
    {
        public string Date = "";

        public void Update(string data)
        {
            Date = data;
        }
    }
}
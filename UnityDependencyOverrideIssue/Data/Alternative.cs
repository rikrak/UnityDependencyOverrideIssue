using System;
using System.Collections.Generic;
using System.Text;

namespace UnityDependencyOverrideIssue.Data
{
    public interface IController
    {
        string GetMessage();
    }

    public class TheController : IController
    {
        private readonly IMessageProvider _messageProvider;

        public TheController(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        public string GetMessage()
        {
            return _messageProvider.CalculateMessage();
        }
    }

    public interface IMessageProvider
    {
        string CalculateMessage();
    }

    class DefaultMessageProvider : IMessageProvider
    {
        public string CalculateMessage()
        {
            return "Hello World";
        }
    }

    class AlternativeMessageProvider : IMessageProvider
    {
        public string CalculateMessage()
        {
            return "Goodbye cruel world!";
        }
    }
}

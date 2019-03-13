using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Service
{
    public class NullMailService : IMailService
    {
        public readonly ILogger<NullMailService> _logger;
        public NullMailService(ILogger<NullMailService> logger)
        {
            this._logger = logger;
        }
        public void SendMessage(string to , string subject, string body)
        {
            _logger.LogInformation($"To {to}, Subject: {subject} Body : {body} ");
        }
    }
}

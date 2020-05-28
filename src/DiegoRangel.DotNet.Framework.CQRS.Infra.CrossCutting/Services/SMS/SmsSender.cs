using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.SMS
{
    public class SmsSender : ISmsSender
    {
        private readonly ILogger _logger;
        private readonly TwilioSettings _twilioSettings;

        public SmsSender(TwilioSettings twilioSettings, ILogger<SmsSender> logger)
        {
            _twilioSettings = twilioSettings;
            _logger = logger;
        }

        public async Task SendAsync(string to, string body)
        {
            try
            {
                TwilioClient.Init(_twilioSettings.Sid, _twilioSettings.Token);

                var message = await MessageResource.CreateAsync(
                    body: body,
                    from: new PhoneNumber(_twilioSettings.FromNumber),
                    to: new PhoneNumber(to)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"SMS sending error: {ex.Message}.");
                throw;
            }
        }
    }
}
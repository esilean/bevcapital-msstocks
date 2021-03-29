using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Background.Aws.Helpers
{
    public class AwsSQS : IAwsSQS
    {
        private readonly ILogger<AwsSQS> _logger;
        private readonly IAmazonSQS _amazonSQS;

        public AwsSQS(IAmazonSQS amazonSQS,
                      ILogger<AwsSQS> logger)
        {
            _amazonSQS = amazonSQS;
            _logger = logger;
        }

        public async Task<List<Message>> ReceiveMessageAsync(string queueUrl)
        {
            try
            {
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = queueUrl,
                    MaxNumberOfMessages = 5,
                    WaitTimeSeconds = 10,
                    VisibilityTimeout = 20
                };

                var result = await _amazonSQS.ReceiveMessageAsync(request);

                return result.Messages.Any() ? result.Messages : new List<Message>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                //DeadLetter?
                //Alarm?
                return new List<Message>();
            }
        }

        public async Task<bool> DeleteMessageAsync(string queueUrl, string messageReceiptHandle)
        {
            try
            {
                var deleteResult = await _amazonSQS.DeleteMessageAsync(queueUrl, messageReceiptHandle);
                return deleteResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                //DeadLetter?
                //Alarm?
                return false;
            }
        }
    }
}

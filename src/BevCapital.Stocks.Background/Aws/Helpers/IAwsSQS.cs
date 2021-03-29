using Amazon.SQS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Background.Aws.Helpers
{
    public interface IAwsSQS
    {
        Task<List<Message>> ReceiveMessageAsync(string queueUrl);
        Task<bool> DeleteMessageAsync(string queueUrl, string messageReceiptHandle);
    }
}

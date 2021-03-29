using Newtonsoft.Json;

namespace BevCapital.Stocks.Background.Aws.Messages
{
    public class SQSMessageEvent
    {
        public string MessageId { get; set; }
        public string ReceiptHandle { get; set; }
        public SQSMessage Message { get; set; }
    }

    public class SQSMessage
    {
        public string Message { get; set; }
        public SQSMessageAttributes MessageAttributes { get; set; }
    }

    public class SQSMessageAttributes
    {
        [JsonProperty("message-type")]
        public SQSMessageAttributeValue MessageType { get; set; }
    }

    public class SQSMessageAttributeValue
    {
        public string Value { get; set; }
    }
}

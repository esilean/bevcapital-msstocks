namespace BevCapital.Stocks.Background.Aws
{
    public class SQSSettings
    {
        public string QueueURL { get; set; }
        public int TimerInternalInSeconds { get; set; }
    }
}

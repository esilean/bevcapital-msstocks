using Amazon.XRay.Recorder.Core;
using BevCapital.Stocks.Background.Aws.Helpers;
using BevCapital.Stocks.Background.Aws.Messages;
using BevCapital.Stocks.Domain.Core.Events;
using BevCapital.Stocks.Domain.Events.AppUserEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Background.Aws
{
    public sealed class SQSBackgroundService : BackgroundService
    {
        private readonly ILogger<SQSBackgroundService> _logger;
        private readonly SQSSettings _sqsSettings;
        private readonly IAwsSQS _awsSQS;
        private readonly IEventBus _eventBus;
        private Timer _timer;

        public SQSBackgroundService(IServiceScopeFactory serviceScopeFactory,
                                    IAwsSQS awsSQS,
                                    IOptions<SQSSettings> options,
                                    ILogger<SQSBackgroundService> logger)
        {
            _awsSQS = awsSQS;
            _sqsSettings = options.Value;
            _logger = logger;
            _eventBus = serviceScopeFactory.CreateScope()
                                           .ServiceProvider.GetService<IEventBus>();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stopping OutboxProcessorBackgroundService...");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Starting OutboxProcessorBackgroundService...");
            _timer = new Timer(ReceiveMessages, null, TimeSpan.FromSeconds(40), TimeSpan.FromSeconds(_sqsSettings.TimerInternalInSeconds));
            return Task.CompletedTask;
        }

        private void ReceiveMessages(object state)
        {
            _ = Process();
        }

        public async Task Process()
        {
            var queueUrl = _sqsSettings.QueueURL;
            try
            {
                AWSXRayRecorder.Instance.BeginSegment(nameof(SQSBackgroundService));

                var receivedMessages = await _awsSQS.ReceiveMessageAsync(queueUrl);

                var messages = receivedMessages.Select(c => new SQSMessageEvent
                {
                    MessageId = c.MessageId,
                    ReceiptHandle = c.ReceiptHandle,
                    Message = JsonConvert.DeserializeObject<SQSMessage>(c.Body)
                });

                foreach (var message in messages)
                {
                    var type = GetEventType(message.Message.MessageAttributes.MessageType.Value);
                    var @event = JsonConvert.DeserializeObject(message.Message.Message, type) as IEvent;

                    await _eventBus.PublishLocal(@event);

                    await _awsSQS.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
                }

            }
            catch (OperationCanceledException ce)
            {
                _logger.LogError(ce, ce.Message);
                AWSXRayRecorder.Instance.AddException(ce);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                AWSXRayRecorder.Instance.AddException(e);
            }
            finally
            {
                AWSXRayRecorder.Instance.EndSegment();
            }

        }

        private Type GetEventType(string messageType)
        {
            var types = typeof(AppUserCreatedEvent).GetTypeInfo().Assembly.GetTypes()
                .Where(mytype => mytype.GetInterfaces().Contains(typeof(IEvent)));

            return types.Where(x => x.FullName.ToLower().Contains(messageType)).FirstOrDefault();
        }
    }
}

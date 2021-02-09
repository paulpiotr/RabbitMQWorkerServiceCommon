using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;

namespace RabbitMQWorkerServiceCommon
{
    public class Worker : BackgroundService
    {

        #region private readonly log4net.ILog log4net
#if !NET48
        /// <summary>
        /// Log4 Net Logger
        /// </summary>
        private readonly log4net.ILog log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
#endif
        #endregion

        internal readonly ILogger<Worker> _logger;

        internal static object _lockRabbitMQPublisherCommonPublish = new object();

        internal static Mutex rabbitMQPublisherCommonPublishMutex = null;

        internal static int delay = 1;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (null == rabbitMQPublisherCommonPublishMutex)
                {
                    //rabbitMQPublisherCommonPublishMutex  = RabbitMQReceiverCommon.RabbitMQReceiverCommon.CreateMutexAndReceiveMessages("RabbitMQPublisherCommonPublish");
                }
                //log4net.Debug($"Sleep { delay } s");
                await Task.Delay(1000 * delay, stoppingToken);
            }
        }
    }
}

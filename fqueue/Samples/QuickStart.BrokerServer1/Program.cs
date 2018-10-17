using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using ECommon.Configurations;
using ECommon.Extensions;
using ECommon.Socketing;
using EQueue.Broker;
using EQueue.Configurations;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace QuickStart.BrokerServer1
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeEQueue();

            var bindingAddress = ConfigurationManager.AppSettings["bindingAddress"];
            var address = ConfigurationManager.AppSettings["nameServerAddress"];
            var brokerAddress = string.IsNullOrEmpty(bindingAddress) ? SocketUtils.GetLocalIPV4() : IPAddress.Parse(bindingAddress);
            var nameServerAddress = string.IsNullOrEmpty(address) ? SocketUtils.GetLocalIPV4() : IPAddress.Parse(address);

            var setting = new BrokerSetting(
                isMessageStoreMemoryMode: bool.Parse(ConfigurationManager.AppSettings["isMemoryMode"]),
                chunkFileStoreRootPath: ConfigurationManager.AppSettings["fileStoreRootPath"],
                chunkFlushInterval: int.Parse(ConfigurationManager.AppSettings["flushInterval"]),
                chunkCacheMaxCount: int.Parse(ConfigurationManager.AppSettings["chunkCacheMaxCount"]),
                chunkCacheMinCount: int.Parse(ConfigurationManager.AppSettings["chunkCacheMinCount"]),
                messageChunkDataSize: int.Parse(ConfigurationManager.AppSettings["chunkSize"]) * 1024 * 1024,
                chunkWriteBuffer: int.Parse(ConfigurationManager.AppSettings["chunkWriteBuffer"]) * 1024,
                enableCache: bool.Parse(ConfigurationManager.AppSettings["enableCache"]),
                syncFlush: bool.Parse(ConfigurationManager.AppSettings["syncFlush"]),
                messageChunkLocalCacheSize: 30 * 10000,
                queueChunkLocalCacheSize: 10000)
            {
                NotifyWhenMessageArrived = bool.Parse(ConfigurationManager.AppSettings["notifyWhenMessageArrived"]),
                MessageWriteQueueThreshold = int.Parse(ConfigurationManager.AppSettings["messageWriteQueueThreshold"]),
                DeleteMessageIgnoreUnConsumed = bool.Parse(ConfigurationManager.AppSettings["deleteMessageIgnoreUnConsumed"])
            };

            //设置Name Server
            setting.NameServerList = new List<IPEndPoint> { new IPEndPoint(nameServerAddress, 9493) };

            setting.BrokerInfo.BrokerName = ConfigurationManager.AppSettings["brokerName"];
            setting.BrokerInfo.GroupName = ConfigurationManager.AppSettings["groupName"];
            setting.BrokerInfo.ProducerAddress = new IPEndPoint(brokerAddress, int.Parse(ConfigurationManager.AppSettings["producerPort"])).ToAddress();
            setting.BrokerInfo.ConsumerAddress = new IPEndPoint(brokerAddress, int.Parse(ConfigurationManager.AppSettings["consumerPort"])).ToAddress();
            setting.BrokerInfo.AdminAddress = new IPEndPoint(brokerAddress, int.Parse(ConfigurationManager.AppSettings["adminPort"])).ToAddress();

            /*
                从部署逻辑上看，Broker Master, Broker Slave是属于一个逻辑上的单元，
                一个Broker Master可以配置多个Broker Slave；所以，我设计了一个Broker Group的概念。
                同一个Broker Group中可以有一个Broker Master和多个Broker Slave；


             */
            BrokerController.Create(setting).Start();

            Console.ReadLine();
        }

        static void InitializeEQueue()
        {
            var configuration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .RegisterEQueueComponents()
                .UseDeleteMessageByCountStrategy(5)
                .BuildContainer();
        }
    }
}

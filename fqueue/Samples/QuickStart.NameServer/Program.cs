﻿using System;
using System.Configuration;
using System.Net;
using ECommon.Configurations;
using ECommon.Socketing;
using EQueue.Configurations;
using EQueue.NameServer;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace QuickStart.NameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeEQueue();
            var bindingAddress = ConfigurationManager.AppSettings["bindingAddress"];
            var bindingIpAddress = string.IsNullOrEmpty(bindingAddress) ? SocketUtils.GetLocalIPV4() : IPAddress.Parse(bindingAddress);

            /*
             Name Server的职责
             是负责管理所有的Broker，并为Producer，Consumer提供Broker信息以及所有Topic的路由信息；

             */
            new NameServerController(new NameServerSetting
            {
                BindingAddress = new IPEndPoint(bindingIpAddress, 9493)
            }).Start();

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
                .BuildContainer();
        }
    }
}

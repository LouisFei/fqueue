﻿namespace EQueue.Protocols.Brokers
{
    public enum BrokerRequestCode
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        SendMessage = 10,
        /// <summary>
        /// 拉消息
        /// </summary>
        PullMessage = 11,
        /// <summary>
        /// 批量发送消息
        /// </summary>
        BatchSendMessage = 12,
        /// <summary>
        /// 生产者心跳
        /// </summary>
        ProducerHeartbeat = 100,
        /// <summary>
        /// 消费者心跳
        /// </summary>
        ConsumerHeartbeat = 101,
        GetConsumerIdsForTopic = 102,
        UpdateQueueConsumeOffsetRequest = 103,
        GetBrokerStatisticInfo = 1000,
        GetTopicQueueInfo = 1001,
        GetTopicConsumeInfo = 1002,
        GetProducerList = 1003,
        GetConsumerList = 1004,
        CreateTopic = 1005,
        DeleteTopic = 1006,
        AddQueue = 1007,
        DeleteQueue = 1008,
        SetQueueProducerVisible = 1009,
        SetQueueConsumerVisible = 1010,
        SetQueueNextConsumeOffset = 1011,
        DeleteConsumerGroup = 1012,
        GetMessageDetail = 1013,
        GetLastestMessages = 1014,
        GetMessageDetailByQueueOffset = 1015
    }
}

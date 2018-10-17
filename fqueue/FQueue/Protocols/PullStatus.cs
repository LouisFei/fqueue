namespace EQueue.Protocols
{
    /// <summary>
    /// 拉动作的状态
    /// </summary>
    public enum PullStatus : short
    {
        Found = 1,
        NoNewMessage = 2,
        NextOffsetReset = 3,
        Ignored = 4,
        QueueNotExist = 5,
        BrokerIsCleaning = 6
    }
}

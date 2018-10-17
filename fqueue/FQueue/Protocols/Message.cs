using System;
using ECommon.Utilities;

namespace EQueue.Protocols
{
    /// <summary>
    /// 消息
    /// </summary>
    [Serializable]
    public class Message
    {
        /// <summary>
        /// 消息所属Topic
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 可根据code来区分消息内容
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息内容，一个二进制数组
        /// </summary>
        public byte[] Body { get; set; }
        public DateTime CreatedTime { get; set; }

        public Message() { }
        public Message(string topic, int code, byte[] body, string tag = null) : this(topic, code, body, DateTime.Now, tag) { }
        public Message(string topic, int code, byte[] body, DateTime createdTime, string tag = null)
        {
            Ensure.NotNull(topic, "topic");
            Ensure.Positive(code, "code");
            Ensure.NotNull(body, "body");
            Topic = topic;
            Tag = tag;
            Code = code;
            Body = body;
            CreatedTime = createdTime;
        }

        public override string ToString()
        {
            return string.Format("[Topic={0},Code={1},Tag={2},CreatedTime={3},BodyLength={4}]", Topic, Code, Tag, CreatedTime, Body.Length);
        }
    }
}

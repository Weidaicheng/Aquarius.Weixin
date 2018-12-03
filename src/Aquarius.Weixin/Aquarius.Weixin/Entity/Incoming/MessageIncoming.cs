namespace Aquarius.Weixin.Entity.Incoming
{
    /// <summary>
    /// 服务器消息
    /// </summary>
    public class MessageIncoming
    {
        public string signature { get; set; }

        public string timestamp { get; set; }

        public string nonce { get; set; }

        public string echostr { get; set; }

        public string encrypt_type { get; set; }

        public string msg_signature { get; set; }
    }
}

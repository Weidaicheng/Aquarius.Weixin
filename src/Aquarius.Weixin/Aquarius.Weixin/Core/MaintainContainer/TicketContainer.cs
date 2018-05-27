using System;
using Aquarius.Weixin.Cache;
using Aquarius.Weixin.Core.InterfaceCaller;

namespace Aquarius.Weixin.Core.MaintainContainer
{
    /// <summary>
    /// Ticket容器
    /// </summary>
    public class TicketContainer
    {
        #region .ctor
        private readonly ICache _cache;
        private readonly TicketInterfaceCaller _ticketInterfaceCaller;

        private static readonly object locker = new object();

        public TicketContainer(ICache cache, TicketInterfaceCaller ticketInterfaceCaller)
        {
            _cache = cache;
            _ticketInterfaceCaller = ticketInterfaceCaller;
        }
        #endregion

        /// <summary>
        /// 获取JS-SDK票据
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string GetJsApiTicket(string accessToken)
        {
            string token = _cache.Get("JSSDKTicket");
            lock(locker)
            {
                if (string.IsNullOrEmpty(token))
                {
                    lock(locker)
                    {
                        var ticket = _ticketInterfaceCaller.GetJsApiTicket(accessToken);
                        _cache.Set("JSSDKTicket", ticket.ticket, TimeSpan.FromSeconds(ticket.expires_in));
                        token = ticket.ticket;
                    }
                }
            }

            return token;
        }
    }
}

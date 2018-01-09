using System;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.InterfaceCaller;

namespace Weixin.Netcore.Core.MaintainContainer
{
    /// <summary>
    /// Ticket容器
    /// </summary>
    public class TicketContainer
    {
        #region .ctor
        private readonly ICache _cache;
        private readonly TicketInterfaceCaller _ticketInterfaceCaller;

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
            if (string.IsNullOrEmpty(token))
            {
                var ticket = _ticketInterfaceCaller.GetJsApiTicket(accessToken);
                _cache.Set("JSSDKTicket", ticket.ticket, TimeSpan.FromSeconds(ticket.expires_in));
                token = ticket.ticket;
            }

            return token;
        }
    }
}

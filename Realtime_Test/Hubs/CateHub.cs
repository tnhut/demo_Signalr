using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Realtime_Test.Hubs
{
    public class CateHub:Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CateHub>();
            context.Clients.All.displayCateGory();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetLogDLL;
using System.ServiceModel;
using GetLogDLL.GetLog;
using System.Net;
using System.IO;

namespace GetLogDLL
{
    public class GetServiceLog
    {
        public RecordLog[] GetLog()
        {
            Service1Client log = new Service1Client();
            return log.GetLog();
        }
        public RecordLog[] Myfunc()
        {
            WSHttpBinding wsHttpBinding = new WSHttpBinding();
            EndpointAddress myEndpoint = new EndpointAddress(new Uri("http://desktop-et5hjr6:90/XLogWCF/GetLogWCF.Service1.svc"));

            //new Channel
            ChannelFactory<IService1> myChannelFactory = new ChannelFactory<IService1>(wsHttpBinding, myEndpoint);
            
            IService1 wcfClient1 = myChannelFactory.CreateChannel();
            try
            {
                ((IClientChannel)wcfClient1).Open();
            }
            catch(ProtocolException ex)
            {
                throw;
            }
            RecordLog[] s = wcfClient1.GetLog();
            ((IClientChannel)wcfClient1).Close();
            return s;

        }
    }
}

using GetLogWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleB
{
    class Program
    {
        private static void Evaluate(string strServer, string strBinding,
            int nPort, string strOper, double dblVal1, double dblVal2)
        {
            ChannelFactory<GetLogWCF.IService1> channelFactory = null;
            EndpointAddress ep = null;

            string strEPAdr = "http://" + strServer +
                   ":" + nPort.ToString() + "/MathService";
            try
            {
                switch (strBinding)
                {
                    case "TCP":
                        NetTcpBinding tcpb = new NetTcpBinding();
                        channelFactory = new ChannelFactory<GetLogWCF.IService1>(tcpb);

                        // End Point Address
                        strEPAdr = "net.tcp://" + strServer + ":" +
                                     nPort.ToString() + "/MathService";
                        break;

                    case "HTTP":
                        BasicHttpBinding httpb = new BasicHttpBinding();
                        channelFactory = new ChannelFactory<GetLogWCF.IService1>(httpb);

                        // End Point Address
                        strEPAdr = "http://" + strServer + ":" +
                                  nPort.ToString() + "/MathService";
                        break;
                }

                // Create End Point
                ep = new EndpointAddress(strEPAdr);

                // Create Channel
                GetLogWCF.IService1 mathSvcObj = channelFactory.CreateChannel(ep);
                IQueryable<GetLogDLL.GetLog.RecordLog> dblResult = null;

                // Call Methods
                switch (strOper)
                {
                    case "ADD": dblResult = mathSvcObj.GetLog(); break;
                }

                //  Display Results.
                Console.WriteLine("Result {0} ", dblResult);
                channelFactory.Close();
            }
            catch (Exception eX)
            {
                // Something unexpected happended .. 
                Console.WriteLine("Error while performing operation [" +
                  eX.Message + "] \n\n Inner Exception [" + eX.InnerException + "]");
            }
        }
    }
}
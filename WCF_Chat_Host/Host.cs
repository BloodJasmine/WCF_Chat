using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF_Chat_Host
{
    class Host
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(WCF_Chat_BL.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Хост запущен.");
                Console.ReadLine();
            }
        }
    }
}

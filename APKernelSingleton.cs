using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APInject
{
    public class APKernelSingleton
    {
        public static IAPInjectKernel Kernel { get; private set; }

        private static APKernelSingleton _instance;

        private static readonly object _lock = new object();

        public static void RebuildKernel(APInjectLoader loader) 
        {
            Kernel = new APInjectKernel(loader);
        }
        private APKernelSingleton(APInjectLoader loader) 
        {
            Kernel = new APInjectKernel(loader);
        }
        public static APKernelSingleton GetInstance(APInjectLoader loader)
        {
            if (_instance == null)
            {
                lock (_lock)
                {

                    if (_instance == null)
                    {
                        _instance = new APKernelSingleton(loader);
                    }
                }
            }
            return _instance;
        }
    }
}

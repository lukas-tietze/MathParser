using System;
using Api;
using Matheparser.Functions;

namespace Plugin
{
    public class Plugin : IPlugin
    {
        public void Dispose()
        {
        }

        public void Init(CalculationContext activeContext)
        {
            activeContext.Out.WriteLine("Hello World!");
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Quartz;

namespace Testing_Quartz
{
    public class JobService : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Debug.WriteLine("Disponible");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Task.FromResult(0);
        }
    }
}

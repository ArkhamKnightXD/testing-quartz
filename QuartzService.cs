using System;
using Quartz;
using Quartz.Impl;

namespace Testing_Quartz
{
    public interface IQuartzService
    {
        void ExecuteJobAtIndicateTime();
    }

    public class QuartzService : IQuartzService
    {
        public async void ExecuteJobAtIndicateTime()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();


            // define the job and tie it to our HelloJob class
            var job = JobBuilder.Create<JobService>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(DateTimeOffset.Now)
                //.WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(5).WithRepeatCount(5))
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}

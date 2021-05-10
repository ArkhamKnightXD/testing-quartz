using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;

namespace Testing_Quartz
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    //  await context.Response.WriteAsync("Hello World!");

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

                });
            });
        }
    }
}

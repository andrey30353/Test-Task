using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using CacheService.Services;

namespace CacheService
{
    class Program
    {
        #region Nested classes to support running as service

        public const string ServiceName = "MyCacheService";

        public class Service : ServiceBase
        {
            public Service()
            {
                ServiceName = Program.ServiceName;
            }

            protected override void OnStart(string[] args)
            {
                Program.Start(args);
            }

            protected override void OnStop()
            {
                Program.Stop();
            }
        }

        #endregion

        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
                // running as service
                using (var service = new Service())
                    ServiceBase.Run(service);
            else
            {
                // running as console app
                Start(args);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                Stop();
            }
        }

        private static void Start(string[] args)
        {
            var tableVersionDict = new Dictionary<string, int>();
            while (true)
            {
                var needUpdateCache = false;
                using (var context = new MsSqlContext())
                {
                    foreach (var tableVersion in context.TableVersion)
                    {
                        var key = tableVersion.TableName;
                        var value = tableVersion.ChangeId;

                        if (tableVersionDict.ContainsKey(key) && tableVersionDict[tableVersion.TableName] == value)
                            continue;

                        needUpdateCache = true;
                        tableVersionDict[tableVersion.TableName] = tableVersion.ChangeId;
                    }
                }

                if (needUpdateCache)
                {
                    UpdateCache();
                }

                Thread.Sleep(30 * 1000);
            }
        }

        private static void Stop()
        {
            // onstop code here
        }

        private static void UpdateCache()
        {
           // Console.WriteLine("Update cache");
            
            using (var context = new MsSqlContext())
            {
                var dms = new DataMapService(context);

                var sportMenuList = dms.CreateSportMenu();
                var eventDetails = dms.CreateEventDetails();
                var upcomingEvents = dms.CreateUpcomingEvents();

                using (var cacheDb = new MongoContext())
                {
                    cacheDb.ReplaceAll(sportMenuList);
                    cacheDb.ReplaceAll(eventDetails);
                    cacheDb.ReplaceAll(upcomingEvents);
                }
            }
        }
    }
}
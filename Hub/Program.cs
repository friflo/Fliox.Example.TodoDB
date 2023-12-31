﻿using System.Threading.Tasks;
using Friflo.Json.Fliox;
using Friflo.Json.Fliox.Hub.Explorer;
using Friflo.Json.Fliox.Hub.Host;
using Friflo.Json.Fliox.Hub.Remote;

namespace TodoHub;

internal static class  Program
{
    public static void Main()
    {
        var schema      = DatabaseSchema.Create<TodoClient>();
        var database    = Provider.CreateDatabase("main_db", "sqlite", schema);
        database.AddCommands(new TodoCommands());
        var hub         = new FlioxHub(database);
        hub.Info.Set ("TodoDB", "dev", "https://github.com/friflo/Fliox.Example.TodoDB", "rgb(0 171 145)"); // optional
        hub.UseClusterDB(); // required by HubExplorer
        hub.UsePubSub();    // optional - enables Pub-Sub
        // --- create HttpHost
        var httpHost    = new HttpHost(hub, "/fliox/");
        httpHost.UseStaticFiles(HubExplorer.Path); // nuget: https://www.nuget.org/packages/Friflo.Json.Fliox.Hub.Explorer
        HttpServer.RunHost("http://localhost:5000/", httpHost); // http://localhost:5000/fliox/
    }
}

public class TodoCommands : IServiceCommands
{
    [CommandHandler]
    private static async Task<Result<int>> ClearCompletedJobs(Param<bool> param, MessageContext context)
    {
        if (!param.Validate(out string error)) {
            return Result.Error(error);
        }
        var client  = new TodoClient(context.Hub); 
        var jobs    = client.jobs.Query(job => job.completed == param.Value);
        await client.SyncTasks();

        client.jobs.DeleteRange(jobs.Result);
        await client.SyncTasks();
        
        return jobs.Result.Count;
    }
}

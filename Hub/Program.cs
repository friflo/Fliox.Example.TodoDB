using Friflo.Json.Fliox.Hub.Explorer;
using Friflo.Json.Fliox.Hub.Host;
using Friflo.Json.Fliox.Hub.Remote;
using Todo;

namespace TodoHub;

internal static class  Program
{
    public static void Main(string[] args)
    {
        var schema      = DatabaseSchema.Create<TodoClient>();
        var database    = Provider.CreateDatabase("main_db", "file", schema);
        database.AddCommands(new TodoCommands());
        var hub         = new FlioxHub(database);
        hub.Info.Set ("TodoDB", "dev", "https://github.com/friflo/Fliox.Example.TodoDB", "rgb(0 171 145)"); // optional
        hub.UseClusterDB(); // required by HubExplorer
        hub.UsePubSub();    // optional - enables Pub-Sub
        // --- create HttpHost
        var httpHost    = new HttpHost(hub, "/fliox/");
        httpHost.UseStaticFiles(HubExplorer.Path); // nuget: https://www.nuget.org/packages/Friflo.Json.Fliox.Hub.Explorer
        
        Startup.Run(args, httpHost); // ASP.NET Core 6
        // HttpServer.RunHost("http://+:8010/", httpHost);
    }
}
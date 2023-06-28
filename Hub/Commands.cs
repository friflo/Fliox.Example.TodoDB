using System.Threading.Tasks;
using Friflo.Json.Fliox;
using Friflo.Json.Fliox.Hub.Host;
using Todo;

namespace TodoHub;

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
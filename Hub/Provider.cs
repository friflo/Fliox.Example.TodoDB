using System;
using System.Collections.Generic;
using Friflo.Json.Fliox.Hub.Host;
using Friflo.Json.Fliox.Hub.MySQL;
using Friflo.Json.Fliox.Hub.PostgreSQL;
using Friflo.Json.Fliox.Hub.SQLite;
using Friflo.Json.Fliox.Hub.SQLServer;

namespace TodoHub;

public static class Provider
{
    private static readonly Dictionary<string,string> ConnectionStrings = new() {
        { "file",       "../Test/DB/main_db" }, // records stored in folder: main_db/jobs
        { "sqlite",     "Data Source=todo_db.sqlite3" },
        { "mysql",      "Server=localhost;Port=3306;User ID=root;Password=YOURPASSWORD;Database=todo_db;Application Name=Fliox" },
        { "mariadb",    "Server=localhost;Port=3307;User ID=root;Password=YOURPASSWORD;Database=todo_db" },
        { "postgres",   "Host=localhost;Username=postgres;Password=YOURPASSWORD;Database=todo_db;Application Name=Fliox" },
        { "sqlserver",  "Data Source=.;Integrated Security=True;Database=todo_db" },
    };
    
    public static EntityDatabase CreateDatabase(string db, string provider, DatabaseSchema schema) {
        ConnectionStrings.TryGetValue(provider, out var config);
        switch (provider) {
            case null:
            case "memory":    return new MemoryDatabase     (db, schema);
            case "file":      return new FileDatabase       (db, config, schema);
            case "sqlite":    return new SQLiteDatabase     (db, config, schema); // https://www.nuget.org/packages/Friflo.Json.Fliox.Hub.SQLite/
            case "mysql":     return new MySQLDatabase      (db, config, schema); // https://www.nuget.org/packages/Friflo.Json.Fliox.Hub.MySQL/
            case "mariadb":   return new MariaDBDatabase    (db, config, schema); // https://www.nuget.org/packages/Friflo.Json.Fliox.Hub.MySQL/
            case "postgres":  return new PostgreSQLDatabase (db, config, schema); // https://www.nuget.org/packages/Friflo.Json.Fliox.Hub.PostgreSQL/
            case "sqlserver": return new SQLServerDatabase  (db, config, schema); // https://www.nuget.org/packages/Friflo.Json.Fliox.Hub.SQLServer/
            default:          throw  new InvalidOperationException($"unknown provider: {provider}");
        }
    }
}
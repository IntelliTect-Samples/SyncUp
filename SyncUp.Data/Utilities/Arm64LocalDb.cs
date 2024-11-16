using System.Diagnostics;
using System.Text;

namespace IntelliTect.SyncUp.Utilities;

public static class Arm64LocalDb
{
    public static string UpdateArm64LocalDbConnectionString(string connectionString)
    {
        // See if we are even trying to connect to localDb
        if (connectionString.ToLower().Contains("(localdb)"))
        {
            // See if this is ARM64 and then start SQL LocalDb
            // But this only detects what the runtime thinks. 
            // TODO: Need to find another way to do this. For now, named pipes can work for everything.
            //if (System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture == System.Runtime.InteropServices.Architecture.Arm64)
            //{
            //Get the current status of LocalDb because it can stop unexpectedly.
            if (!IsRunning())
            {
                if (!StartLocalDb())
                {
                    throw new Exception("Could not start SQL LocalDB");
                }
            }
            // It is running now get the named pipe
            var namedPipe = GetNamedPipe();
            // Insert this into the connection string
            var parts = connectionString.Split(';');
            StringBuilder newConnectionString = new();
            foreach (var part in parts)
            {
                var partParts = part.Split("=");
                if (partParts.Length > 1 && partParts[0].ToLower().Trim() == "server")
                {
                    newConnectionString.Append(partParts[0] + "=" + namedPipe + ";");
                }
                else
                {
                    newConnectionString.Append(part + ";");
                }
            }
            return newConnectionString.ToString();
            //}
        }
        return connectionString;
    }

    private static bool IsRunning()
    {
        var output = GetInfo();
        return output.Contains("np:\\\\.\\pipe\\LOCALDB") && output.Contains("Running");
    }

    private static string GetNamedPipe()
    {
        var output = GetInfo();
        var parts = output.Split("Instance pipe name: ");
        return parts[1].Trim();
    }

    private static string GetInfo()
    {
        return RunProcess("SqlLocalDB.exe", "info MSSQLLocalDB");
    }

    private static bool StartLocalDb()
    {
        var output = RunProcess("SqlLocalDB.exe", "start");
        return output.Contains("LocalDB") && output.Contains("started.");
    }

    private static string RunProcess(string command, string args)
    {
        Process p = new Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.FileName = command;
        p.StartInfo.Arguments = args;
        p.Start();

        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        return output;
    }
}

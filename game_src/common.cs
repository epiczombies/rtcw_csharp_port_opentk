using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class game
{
    public static qboolean Com_Filter()
    {
        throw new Exception("Fuck i have to do this!");
        return qboolean.qtrue;
    }
    public static void Com_Printf(string fmt, params object[] args)
    {
        string msg = (args.Length > 0 ? String.Format(fmt, args) : fmt);

        // echo to console if we're not a dedicated server
        if (com_dedicated && !com_dedicated.integer)
        {
            CL_ConsolePrint(msg);
        }

        // echo to dedicated console and early console
        Sys_Print(msg);

        throw new Exception("Finish this with log file...");
    }
}
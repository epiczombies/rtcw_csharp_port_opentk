using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class game
{
    static cvar_t com_viewlog;
    static cvar_t com_speeds;
    static cvar_t com_developer;
    static cvar_t com_dedicated;
    static cvar_t com_timescale;
    static cvar_t com_fixedtime;
    static cvar_t com_dropsim;       // 0.0 to 1.0, simulated packet drops
    static cvar_t com_journal;
    static cvar_t com_maxfps;
    static cvar_t com_timedemo;
    static cvar_t com_sv_running;
    static cvar_t com_cl_running;
    static cvar_t com_logfile;       // 1 = buffer log, 2 = flush after each print
    static cvar_t com_showtrace;
    static cvar_t com_version;
    static cvar_t com_blood;
    static cvar_t com_buildScript;   // for automated data building scripts
    static cvar_t com_introPlayed;
    static cvar_t cl_paused;
    static cvar_t sv_paused;
    static cvar_t com_cameraMode;
    static cvar_t com_noErrorInterrupt;
    static cvar_t com_recommendedSet;

    public static qboolean Com_Filter(string filter, string name, int casesensitive)
    {
        throw new Exception("Fuck i have to do this!");
        return qboolean.qfalse;
    }
    public static void Com_Printf(string fmt, params object[] args)
    {
        string msg = (args.Length > 0 ? String.Format(fmt, args) : fmt);

        // echo to console if we're not a dedicated server
        if (com_dedicated != null && com_dedicated.integer != 0)
            CL_ConsolePrint(msg);

        // echo to dedicated console and early console
        Sys_Print(msg);

        throw new Exception("Finish this with log file...");
    }
    public static void Com_Error(errorParm_t code, string fmt, params object[] args)
    {
        throw new Exception(va(fmt, args));
    }
}
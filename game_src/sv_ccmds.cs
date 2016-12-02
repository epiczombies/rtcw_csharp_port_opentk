using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static partial class game
{
    /*
    ==================
    SV_KickNum_f

    Kick a user off of the server  FIXME: move to game
    ==================
    */
    static void SV_KickNum_f()
    {
        client_t cl;

        // make sure server is running
        if (!com_sv_running.integer)
        {
            Com_Printf("Server is not running.\n");
            return;
        }

        if (Cmd_Argc() != 2)
        {
            Com_Printf("Usage: kicknum <client number>\n");
            return;
        }

        cl = SV_GetPlayerByNum();
        if (!cl)
        {
            return;
        }
        if (cl.netchan.remoteAddress.type == netadrtype_t.NA_LOOPBACK)
        {
            SV_SendServerCommand(null, "print \"%s\"", "Cannot kick host player\n");
            return;
        }

        SV_DropClient(cl, "was kicked");
        cl.lastPacketTime = svs.time;  // in case there is a funny zombie
    }
    /*
    ================
    SV_Status_f
    ================
    */
    static void SV_Status_f()
    {
        int i, j, l;
        client_t cl;
        playerState_t ps;
        string s;
        int ping;

        // make sure server is running
        if (!com_sv_running.integer)
        {
            Com_Printf("Server is not running.\n");
            return;
        }

        Com_Printf("map: %s\n", sv_mapname.String);

        Com_Printf("num score ping name            lastmsg address               qport rate\n");
        Com_Printf("--- ----- ---- --------------- ------- --------------------- ----- -----\n");
        for (i = 0, cl = svs.clients; i < sv_maxclients.integer; i++, cl++)
        {
            if (!cl.state)
                continue;

            Com_Printf("%3i ", i);
            ps = SV_GameClientNum(i);
            Com_Printf("%5i ", ps.persistant[PERS_SCORE]);

            if (cl.state == CS_CONNECTED)
            {
                Com_Printf("CNCT ");
            }
            else if (cl.state == CS_ZOMBIE)
            {
                Com_Printf("ZMBI ");
            }
            else
            {
                ping = cl.ping < 9999 ? cl.ping : 9999;
                Com_Printf("%4i ", ping);
            }

            Com_Printf("%s", cl.name);
            l = 16 - strlen(cl.name);
            for (j = 0; j < l; j++)
                Com_Printf(" ");

            Com_Printf("%7i ", svs.time - cl.lastPacketTime);

            s = NET_AdrToString(cl.netchan.remoteAddress);
            Com_Printf("%s", s);
            l = 22 - strlen(s);
            for (j = 0; j < l; j++)
                Com_Printf(" ");

            Com_Printf("%5i", cl.netchan.qport);

            Com_Printf(" %5i", cl.rate);

            Com_Printf("\n");
        }
        Com_Printf("\n");
    }
    /*
    ==================
    SV_ConSay_f
    ==================
    */
    static void SV_ConSay_f()
    {
        throw new Exception("TODO");
    }
    /*
    ==================
    SV_Heartbeat_f

    Also called by SV_DropClient, SV_DirectConnect, and SV_SpawnServer
    ==================
    */
    static void SV_Heartbeat_f()
    {
        svs.nextHeartbeatTime = -9999999;
    }
    /*
    ===========
    SV_Serverinfo_f

    Examine the serverinfo string
    ===========
    */
    static void SV_Serverinfo_f()
    {
        Com_Printf("Server info settings:\n");
        Info_Print(Cvar_InfoString(CVAR_SERVERINFO));
    }
    /*
    ===========
    SV_Systeminfo_f

    Examine or change the serverinfo string
    ===========
    */
    static void SV_Systeminfo_f()
    {
        Com_Printf("System info settings:\n");
        Info_Print(Cvar_InfoString(CVAR_SYSTEMINFO));
    }
    /*
    ===========
    SV_DumpUser_f

    Examine all a users info strings FIXME: move to game
    ===========
    */
    static void SV_DumpUser_f()
    {
        client_t cl;

        // make sure server is running
        if (!com_sv_running.integer)
        {
            Com_Printf("Server is not running.\n");
            return;
        }

        if (Cmd_Argc() != 2)
        {
            Com_Printf("Usage: info <userid>\n");
            return;
        }

        cl = SV_GetPlayerByName();
        if (!cl)
        {
            return;
        }

        Com_Printf("userinfo\n");
        Com_Printf("--------\n");
        Info_Print(cl.userinfo);
    }
    /*
    =================
    SV_KillServer
    =================
    */
    static void SV_KillServer_f()
    {
        SV_Shutdown("killserver");
    }

    //===========================================================

    static qboolean initialized; // SV_AddOperatorCommands
    /*
    ==================
    SV_AddOperatorCommands
    ==================
    */
    static void SV_AddOperatorCommands()
    {
        if (initialized == qboolean.qtrue)
            return;

        initialized = qboolean.qtrue;

        Cmd_AddCommand("heartbeat", SV_Heartbeat_f);
        Cmd_AddCommand("kick", SV_Kick_f);
        Cmd_AddCommand("banUser", SV_Ban_f);
        Cmd_AddCommand("banClient", SV_BanNum_f);
        Cmd_AddCommand("clientkick", SV_KickNum_f);
        Cmd_AddCommand("status", SV_Status_f);
        Cmd_AddCommand("serverinfo", SV_Serverinfo_f);
        Cmd_AddCommand("systeminfo", SV_Systeminfo_f);
        Cmd_AddCommand("dumpuser", SV_DumpUser_f);
        Cmd_AddCommand("map_restart", SV_MapRestart_f);
        Cmd_AddCommand("sectorlist", SV_SectorList_f);
        Cmd_AddCommand("spmap", SV_Map_f);
//#ifndef WOLF_SP_DEMO
        Cmd_AddCommand("map", SV_Map_f);
        Cmd_AddCommand("devmap", SV_Map_f);
        Cmd_AddCommand("spdevmap", SV_Map_f);
//#endif
        Cmd_AddCommand("loadgame", SV_LoadGame_f);
        Cmd_AddCommand("killserver", SV_KillServer_f);
        if (com_dedicated.integer)
            Cmd_AddCommand("say", SV_ConSay_f);
    }
    /*
    ==================
    SV_RemoveOperatorCommands
    ==================
    */
    static void SV_RemoveOperatorCommands()
    {
#if false
	    // removing these won't let the server start again
	    Cmd_RemoveCommand( "heartbeat" );
	    Cmd_RemoveCommand( "kick" );
	    Cmd_RemoveCommand( "banUser" );
	    Cmd_RemoveCommand( "banClient" );
	    Cmd_RemoveCommand( "status" );
	    Cmd_RemoveCommand( "serverinfo" );
	    Cmd_RemoveCommand( "systeminfo" );
	    Cmd_RemoveCommand( "dumpuser" );
	    Cmd_RemoveCommand( "map_restart" );
	    Cmd_RemoveCommand( "sectorlist" );
	    Cmd_RemoveCommand( "say" );
#endif
    }
}
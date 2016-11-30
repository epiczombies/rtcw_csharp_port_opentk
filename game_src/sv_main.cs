using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class game
{
    //serverStatic_t svs;                 // persistant server info
    //server_t sv;                        // local server

    static cvar_t sv_fps;                // time rate for running non-clients
    static cvar_t sv_timeout;            // seconds without any message
    static cvar_t sv_zombietime;         // seconds to sink messages after disconnect
    static cvar_t sv_rconPassword;       // password for remote server commands
    static cvar_t sv_privatePassword;    // password for the privateClient slots
    static cvar_t sv_allowDownload;
    static cvar_t sv_maxclients;
    static cvar_t sv_privateClients;     // number of clients reserved for password
    static cvar_t sv_hostname;
    static cvar_t[] sv_master;//cvar_t  *sv_master[MAX_MASTER_SERVERS];     // master server ip address
    static cvar_t sv_reconnectlimit;     // minimum seconds between connect messages
    static cvar_t sv_showloss;           // report when usercmds are lost
    static cvar_t sv_padPackets;         // add nop bytes to messages
    static cvar_t sv_killserver;         // menu system can set to 1 to shut server down
    static cvar_t sv_mapname;
    static cvar_t sv_mapChecksum;
    static cvar_t sv_serverid;
    static cvar_t sv_maxRate;
    static cvar_t sv_minPing;
    static cvar_t sv_maxPing;
    static cvar_t sv_gametype;
    static cvar_t sv_pure;
    static cvar_t sv_floodProtect;
    static cvar_t sv_allowAnonymous;
}
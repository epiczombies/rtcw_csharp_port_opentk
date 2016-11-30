using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum qboolean { qfalse = 0, qtrue = 1 };
public static partial class game
{
    static int CVAR_ARCHIVE = 1;  // set to cause it to be saved to vars.rc
                           // used for system variables, not for player
                           // specific configurations
    static int CVAR_USERINFO = 2;  // sent to server on connect or change
    static int CVAR_SERVERINFO = 4; // sent in response to front end requests
    static int CVAR_SYSTEMINFO = 8; // these cvars will be duplicated on all clients
    static int CVAR_INIT = 16; // don't allow change from console at all,
                               // but can be set from the command line
    static int CVAR_LATCH = 32; // will only change when C code next does
                         // a Cvar_Get(), so it can't be changed
                         // without proper initialization.  modified
                         // will be set, even though the value hasn't
                         // changed yet
    static int CVAR_ROM = 64;  // display only, cannot be set by user at all
    static int CVAR_USER_CREATED = 128; // created by a set command
    static int CVAR_TEMP = 256;// can be set even when cheats are disabled, but is not archived
    static int CVAR_CHEAT = 512; // can not be changed if cheats are disabled
    static int CVAR_NORESTART = 1024; // do not clear when a cvar_restart is issued

    static int MAX_STRING_TOKENS = 256; // max tokens resulting from Cmd_TokenizeString


    static string S_COLOR_BLACK = "^0";
    static string S_COLOR_RED = "^1";
    static string S_COLOR_GREEN = "^2";
    static string S_COLOR_YELLOW = "^3";
    static string S_COLOR_BLUE = "^4";
    static string S_COLOR_CYAN = "^5";
    static string S_COLOR_MAGENTA = "^6";
    static string S_COLOR_WHITE = "^7";
}
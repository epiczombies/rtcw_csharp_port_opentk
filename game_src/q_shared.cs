using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum errorParm_t
{
    ERR_FATAL,                  // exit the entire game with a popup window
    ERR_DROP,                   // print to console and disconnect from game
    ERR_SERVERDISCONNECT,       // don't kill server
    ERR_DISCONNECT,             // client disconnected from the server
    ERR_NEED_CD,                // pop up the need-cd dialog
    ERR_ENDGAME                 // not an error.  just clean up properly, exit to the menu, and start up the "endgame" menu  //----(SA)	added
};
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






    // angle indexes
    static int PITCH = 0;    // up / down
    static int YAW = 1;    // left / right
    static int ROLL = 2;    // fall over

    // RF, this is just here so different elements of the engine can be aware of this setting as it changes
    static int MAX_SP_CLIENTS = 64;     // increasing this will increase memory usage significantly

    // the game guarantees that no string from the network will ever
    // exceed MAX_STRING_CHARS
    static int MAX_STRING_CHARS = 1024;   // max length of a string passed to Cmd_TokenizeString
    static int MAX_STRING_TOKENS = 256;   // max tokens resulting from Cmd_TokenizeString
    static int MAX_TOKEN_CHARS = 1024;   // max length of an individual token

    static int MAX_INFO_STRING = 1024;
    static int MAX_INFO_KEY = 1024;
    static int MAX_INFO_VALUE = 1024;

    static int BIG_INFO_STRING = 8192;  // used for system info key only
    static int BIG_INFO_KEY = 8192;
    static int BIG_INFO_VALUE = 8192;

    static int MAX_QPATH = 64;   // max length of a quake game pathname
    static int MAX_OSPATH = 256;  // max length of a filesystem pathname

    static int MAX_NAME_LENGTH = 32;  // max length of a client name

    static int MAX_SAY_TEXT = 150;

    









    static string S_COLOR_BLACK = "^0";
    static string S_COLOR_RED = "^1";
    static string S_COLOR_GREEN = "^2";
    static string S_COLOR_YELLOW = "^3";
    static string S_COLOR_BLUE = "^4";
    static string S_COLOR_CYAN = "^5";
    static string S_COLOR_MAGENTA = "^6";
    static string S_COLOR_WHITE = "^7";



    static void Com_sprintf( ref string dest, int size, string fmt, params object[] args)
    {
	    int len;
	    string bigbuffer; // 32000

        bigbuffer = va(fmt, args);
        len = bigbuffer.Length;

        if (len >= 32000)
            Com_Error(errorParm_t.ERR_FATAL, "Com_sprintf: overflowed bigbuffer");
        if (len >= size)
            Com_Printf("Com_sprintf: overflow of %i in %i\n", len, size);
        Q_strncpyz(ref dest, bigbuffer, size );
    }
    static void Q_strncpyz(ref string dest, string src, int destsize )
    {
        if (src == null)
            Com_Error(errorParm_t.ERR_FATAL, "Q_strncpyz: NULL src");
        
        if (destsize < 1)
            Com_Error(errorParm_t.ERR_FATAL, "Q_strncpyz: destsize < 1");

        dest = src;
    }
}
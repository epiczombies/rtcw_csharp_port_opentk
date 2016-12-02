using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

delegate void xcommand_t();
class cmd_function_t
{
    public cmd_function_t next;
    public string name;
    public xcommand_t function;
};
public static partial class game
{
    static int cmd_argc = 0;
    static string[] cmd_argv = new string[MAX_STRING_TOKENS];
    static string[] cmd_tokenized = new string[BIG_INFO_STRING + MAX_STRING_TOKENS];         // will have 0 bytes inserted
    static List<cmd_function_t> cmd_functions = new List<cmd_function_t>();



    /*
    ============
    Cmd_Argc
    ============
    */
    static int Cmd_Argc()
    {
        return cmd_argc;
    }

    /*
    ============
    Cmd_Argv
    ============
    */
    static string Cmd_Argv(int arg)
    {
        if (arg >= cmd_argc)
            return "";
        return cmd_argv[arg];
    }

    /*
    ============
    Cmd_TokenizeString

    Parses the given string into command line tokens.
    The text is copied to a seperate buffer and 0 characters
    are inserted in the apropriate place, The argv array
    will point into this temporary buffer.
    ============
    */
    static void Cmd_TokenizeString(string text_in)
    {
        throw new Exception("check on github: xna-rtcw-sp");
    }

    /*
    ============
    Cmd_AddCommand
    ============
    */
    static void Cmd_AddCommand(string cmd_name, xcommand_t function)
    {
        // fail if the command already exists
        foreach (cmd_function_t cmd in cmd_functions)
        {
            if (cmd.name == cmd_name)
            {
                if (function != null)
                {
                    Com_Printf("Cmd_AddCommand: %s already defined\n", cmd_name);
                }

                return;
            }
        }

        cmd_function_t newcmd = new cmd_function_t();

        newcmd.name = cmd_name;
        newcmd.function = function;

        cmd_functions.Add(newcmd);
    }
    /*
    ============
    Cmd_RemoveCommand
    ============
    */
    static void Cmd_RemoveCommand(string cmd_name)
    {
        for (int i = 0; i < cmd_functions.Count; i++)
        {
            if (cmd_functions[i].name == cmd_name)
            {
                cmd_functions.RemoveAt(i);
                return;
            }
        }
    }


    /*
    ============
    Cmd_CommandCompletion
    ============
    */
    public delegate void callbackDelegate(string s);
    static void Cmd_CommandCompletion(callbackDelegate callback)
    {
        foreach (cmd_function_t cmd in cmd_functions)
            if (cmd != null)
                callback(cmd.name);
    }
    
    /*
    ============
    Cmd_ExecuteString

    A complete command line has been parsed, so try to execute it
    ============
    */
    static void Cmd_ExecuteString(string text)
    {
        // execute the command line
        Cmd_TokenizeString(text);
        if (Cmd_Argc() <= 0)
            return;     // no tokens

        foreach (cmd_function_t func in cmd_functions)
        {
            if (func.name == cmd_argv[0])
            {
                if (func.function != null)
                {
                    func.function();
                    break;
                }
            }
        }

        // check cvars
        if (Cvar_Command() == qboolean.qtrue)
            return;

        // check client game commands
        if (com_cl_running && com_cl_running.integer && CL_GameCommand())
            return;

        // check server game commands
        if (com_sv_running && com_sv_running.integer && SV_GameCommand())
            return;

        // check ui commands
        if (com_cl_running && com_cl_running.integer && UI_GameCommand())
            return;

        // send it as a server command if we are connected
        // this will usually result in a chat message
        CL_ForwardCommandToServer(text);
    }


    /*
    ============
    Cmd_List_f
    ============
    */
    static void Cmd_List_f( )
    {
        int i;
        string match;

        if (Cmd_Argc() > 1)
            match = Cmd_Argv(1);
        else
            match = null;

        i = 0;
        foreach (cmd_function_t cmd in cmd_functions)
        {
            if (match != null && Com_Filter(match, cmd.name, (int)qboolean.qfalse) == qboolean.qfalse)
                continue;

            Com_Printf("%s\n", cmd.name);
            i++;
        }
        Com_Printf("%i commands\n", i);
    }

    /*
    ============
    Cmd_Init
    ============
    */
    static void Cmd_Init()
    {
        Cmd_AddCommand("cmdlist", Cmd_List_f);
        Cmd_AddCommand("exec", Cmd_Exec_f);
        Cmd_AddCommand("vstr", Cmd_Vstr_f);
        Cmd_AddCommand("echo", Cmd_Echo_f);
        Cmd_AddCommand("wait", Cmd_Wait_f);
    }
}
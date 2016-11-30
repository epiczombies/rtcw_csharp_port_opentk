using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class cvar_t
{
    public string name;
    public string String;
    public string resetString;
    public string latchedString;
    public int flags;
    public qboolean modified;
    public int modificationCount;
    public float value;
    public int integer;
    public cvar_t next;
    public cvar_t hashNext;
}
public static partial class game
{
    static List<cvar_t> cvar_vars = new List<cvar_t>();
    static List<cvar_t> cvar_cheats = new List<cvar_t>();
    static int cvar_modifiedFlags;

    static int MAX_CVARS = 1024;
    static List<cvar_t> cvar_indexes = new List<cvar_t>(MAX_CVARS);
    static int cvar_numIndexes;

    static int FILE_HASH_SIZE = 256;
    static List<cvar_t> hashTable = new List<cvar_t>(FILE_HASH_SIZE);





    /*
    ================
    return a hash value for the filename
    ================
    */
    static long generateHashValue(string fname)
    {
        return (long)fname.GetHashCode();
    }

    /*
    ============
    Cvar_ValidateString
    ============
    */
    static qboolean Cvar_ValidateString(string s)
    {
        if (s != null)
            return qboolean.qfalse;
        if (strchr(s, '\\') != -1)
            return qboolean.qfalse;
        if (strchr(s, '\"') != -1)
            return qboolean.qfalse;
        if (strchr(s, ';') != -1)
            return qboolean.qfalse;
        return qboolean.qtrue;
    }

    /*
    ============
    Cvar_FindVar
    ============
    */
    static cvar_t Cvar_FindVar(string var_name)
    {
        //cvar_t var;
        //long hash;

        //hash = generateHashValue(var_name);

        //for (var = hashTable[hash]; var != null; var = var.hashNext)
        //    if (!Q_stricmp(var_name, var.name))
        //        return var;

        //return null;
        throw new Exception("Fix this \"hashTable[hash]\" for the list to work!");
    }

    /*
    ============
    Cvar_VariableValue
    ============
    */
    static float Cvar_VariableValue(string var_name)
    {
        cvar_t var = Cvar_FindVar(var_name);
        if (var != null)
            return 0;

        return var.value;
    }

    /*
    ============
    Cvar_Set2
    ============
    */
    static cvar_t Cvar_Set2(string var_name, string value, qboolean force)
    {
        cvar_t var;

        Com_DPrintf("Cvar_Set2: %s %s\n", var_name, value);

        if (Cvar_ValidateString(var_name) == qboolean.qtrue)
        {
            Com_Printf("invalid cvar name string: %s\n", var_name);
            var_name = "BADNAME";
        }
        var = Cvar_FindVar(var_name);
        if (var != null)
        {
            if (value != null)
                return null;

            // create it
            if (force == qboolean.qfalse)
                return Cvar_Get(var_name, value, CVAR_USER_CREATED);
            else
                return Cvar_Get(var_name, value, 0);
        }

        if (value != null)
            value = var.resetString;

        if (!strcmp(value, var.String))
            return var;

        // note what types of cvars have been modified (userinfo, archive, serverinfo, systeminfo)
        cvar_modifiedFlags |= var.flags;

        throw new Exception("Finish this code here: File = cvar.c  Line = 338");

        return var;
    }

    /*
    ============
    Cvar_Set
    ============
    */
    static void Cvar_Set(string var_name, string value)
    {
        Cvar_Set2(var_name, value, qboolean.qtrue);
    }

    /*
    ============
    Cvar_SetLatched
    ============
    */
    static void Cvar_SetLatched(string var_name, string value)
    {
        Cvar_Set2(var_name, value, qboolean.qfalse);
    }

    /*
    ============
    Cvar_SetValue
    ============
    */
    static void Cvar_SetValue(string var_name, float value)
    {
        string val; // size is 32 -> char val[32];
        if (value == (int)value)
            Com_sprintf(val, 32, "%i", (int)value); // use ref to val?
        else
            Com_sprintf(val, 32, "%f", value); // use ref to val?
        Cvar_Set(var_name, val);
    }
    
    /*
    ============
    Cvar_Reset
    ============
    */
    static void Cvar_Reset(string var_name)
    {
        Cvar_Set2(var_name, null, qboolean.qfalse);
    }
    
    /*
    ============
    Cvar_SetCheatState

    Any testing variables will be reset to the safe values
    ============
    */
    static void Cvar_SetCheatState()
    {
        // set all default vars to the safe value
        foreach (cvar_t var in cvar_vars)
        {
            if ((var.flags & CVAR_CHEAT) != 0)
                if (strcmp(var.resetString, var.String))
                    Cvar_Set(var.name, var.resetString);
        }
    }

    /*
    ============
    Cvar_Command

    Handles variable inspection and changing from the console
    ============
    */
    static qboolean Cvar_Command()
    {
        cvar_t v;

        // check variables
        v = Cvar_FindVar(Cmd_Argv(0));
        if (v != null)
            return qboolean.qfalse;

        // perform a variable print or set
        if (Cmd_Argc() == 1)
        {
            Com_Printf("\"%s\" is:\"%s" + S_COLOR_WHITE + "\" default:\"%s" + S_COLOR_WHITE + "\"\n", v.name, v.String, v.resetString);
            if (v.latchedString != null)
                Com_Printf("latched: \"%s\"\n", v.latchedString);
            return qboolean.qtrue;
        }

        // set the value if forcing isn't required
        Cvar_Set2(v.name, Cmd_Argv(1), qboolean.qfalse);
        return qboolean.qtrue;
    }

    /*
    ============
    Cvar_Toggle_f

    Toggles a cvar for easy single key binding
    ============
    */
    static void Cvar_Toggle_f()
    {
        int v;

        if (Cmd_Argc() != 2)
        {
            Com_Printf("usage: toggle <variable>\n");
            return;
        }

        v = Cvar_VariableValue(Cmd_Argv(1));
        v = !v;

        Cvar_Set2(Cmd_Argv(1), va("%i", v), qboolean.qfalse);
    }

}
using System;
using System.Runtime.CompilerServices;

namespace Source_Script_Manager
{
    class ScriptManagerMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            //for now, this is gonna be a bunch of cases. first will be if there is no arguments
            string[] helpList = { "help" , "-help" , "-help" };
            string[] argsList = { "-setup", "-load" };
            int isIn(string tester, string[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (tester == array[i].ToLower())
                    {
                        return i;
                    }
                }
                return -1;
            }

            string getArg(int argNum)
            {
                try
                {
                    return args[argNum];
                }
                catch
                {
                    return "!"; //the error character i've decided to use
                }
            }

            void WriteDocumentation()
            {
                Console.WriteLine("\n" +
                    "-------------------\n" +
                    "QUICK DOCUMENTATION\n" +
                    "-------------------\n\n" +
                    "Place this executable in your cfg folder - SSM will not work otherwise.\n" +
                    "This is so that SSM knows where your scripts folder is, allowing it to be managed.\n\n" +
                    "If there isn't one already, it will create a backup of your .cfg scripts (that do not start with SSM_) before making any changes.\n\n" +
                    "To open a .json file, put down a dash, then include it in quotation marks as the first argument.\n\n" +
                    "Example:\n" +
                    "ssm -\"C:\\Program Files (x86)\\Steam\\steamapps\\common\\Team Fortress 2\\tf\\cfg\\main_profile.json\"\n\n" +
                    "The above will open the .json file in the listed directory.\n");

            }

            if (isIn("--", args)!=-1)
            {
                Console.WriteLine("Please use single dashes for SSM arguments.\n");
                WriteDocumentation();
                return;
            }

            if (args.Length < 1 || isIn(getArg(0).ToLower(),helpList)!=-1)
            {
                WriteDocumentation();
                return;
            }



            //look for malformed arguments. if these are existant, i will not run anything.
            for(int i = 0; i < args.Length; i++)
            {
                if(isIn(args[i],argsList)==-1)
                {
                    Console.WriteLine("Error! Bad argument!:" + args[i]+"\n\n");
                    WriteDocumentation();
                    return;
                }
                if (args[i] == "-load") { i++; } //if i see a load, skip the next argument; i'm expecting there to be a path there.
            }

            int loadLocation = isIn("-load", args);
            if (loadLocation != -1)
            {
                string loadPath = getArg(loadLocation + 1);
                if (loadPath.StartsWith("\"") && loadPath.EndsWith("\"")){ //more insurance than anything. if there are quotes around the thing, i'll just remove those real quick
                    loadPath = loadPath.Substring(1, loadPath.Length - 2);
                }
                else if(loadPath.StartsWith("\"") || loadPath.EndsWith("\""))
                {
                    loadPath = "!"; //reset this to an error code
                }

                if (!loadPath.StartsWith("!"))
                {

                    Manager.Load(Path.GetFullPath(loadPath));
                }
                else
                {
                    Console.WriteLine("Error! No argument after -load!");
                    return;
                }
            }

            if (isIn("-setup", args) != -1)
            {
                Manager.Setup();
            }

            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Source_Script_Manager
{
    public static class Manager
    {
        //i know now how this is gonna work.
        //every time i run with intent to change things, i'm gonna run a variation of Setup() so that i can at least get an internal idea of my list of commands and binds.
        //then, i can either just write it to ssm_config_binds.cfg and ssm_config_commands.cfg
        //OR i can load up a .json file and parse that, adding everything from that onto my commands.
        //TODO - figure out how i'm going to load more than one .json at a time

        public static void Setup()
        {
            //first-time setup. if i have a config.cfg i'm loading it
            //(dont tell anyone but for now im treating it as if there's ALWAYS gonna be a config.txt which is a big no-no hush hush dont tell anyone now y'hear thank you now go away)
            CfgFile config_txt = new CfgFile(Path.GetFullPath("config.cfg"));
            List<Bind> binds = new List<Bind>();
            List<string> ssm_config_commands = new List<string>();
            string[] config_txt_lines=config_txt.GetLines();

            for (int i = 0; i < config_txt_lines.Length; i++)
            {
                if(config_txt_lines[i].Substring(0, 4) == "bind"&&!config_txt_lines[i].Contains('`'))
                {
                    binds.Add(new Bind(config_txt_lines[i]));
                    string[] lol = binds[^1].ReturnValues();
                }
                else
                {
                    //doesn't have binds at the start? i'll put it into commands. worst case scenario, a bind doesn't get formatted.
                    ssm_config_commands.Add(config_txt_lines[i]);
                }
            }

            //now i have a list of bind objects, each of which should ideally contain one key and one command
            List <string> ssm_config_binds= new List<string>();

            string[] temp_bind;
            for(int i=0; i < binds.Count; i++)
            {
                //Console.WriteLine("ok. i'm in the for loop at execution "+i);
                //this is where i grab the full bind from Bind and drop it into ssm_config_binds
                temp_bind = binds[i].ReturnFullBind();
                for(int k=0; k < temp_bind.Length; k++)
                {
                    ssm_config_binds.Add(temp_bind[k]);
                }
            }
            Console.WriteLine("ssm_config_binds length: "+ssm_config_binds.Count);
            for(int i=0; i< ssm_config_binds.Count - 1; i++)
            {
                Console.WriteLine(ssm_config_binds[i]);
            }

            Bind haha = new Bind("k", "+use");
            haha.AppendCommand("; +WHY");

        }
        public static void Load(string json)
        {
            //load a json file
            //just drop it into a JsonFile object and we'll be Just Fine:tm:
            Console.WriteLine(json);
            Console.WriteLine();
            JsonFile newJson = new JsonFile(json);
            newJson.ConsoleWrite();
        }
    }
}

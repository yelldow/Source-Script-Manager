using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source_Script_Manager
{
    internal class Alias
    {
        //contains alias information. for example, i can do:
        //alias +asdf "+forward"
        //alias -asfd "-forward"
        //asdf would be the alias name; the + and - variants would be what actually gets put into the file, but SSM simply thinks of it as asdf
        List<Command> commands = new List<Command>();
        public string name = "";
        public bool IsDual;
        public Alias(string name, bool IsDual)
        {
            this.name = name;
            this.IsDual = IsDual;
        }
        public void AddCommand(Command command)
        {
            if (!command.GetPositive().Contains("; ")) //the command is fine
            {
                commands.Add(command);
            }
            else if (command.GetPositive().Contains("; ")) //contains a '; '
            {
                string[] split = command.GetPositive().Split("; ");
                for(int i = 0; i < split.Length; i++)
                {
                    commands.Add(new Command(split[i]));
                }
            }
            else if (command.GetPositive().Contains(";"))
            {
                string[] split = command.GetPositive().Split(';');
                for (int i = 0; i < split.Length; i++)
                {
                    commands.Add(new Command(split[i]));
                }
            }
        }

        public string GetPositiveCommands()
        {
            string fullCommands = "";
            for(int i = 0; i < commands.Count; i++)
            {
                if (i == commands.Count - 1)
                {//end
                    fullCommands += commands[i].GetPositive();
                }
                else
                {//normal
                    fullCommands += commands[i].GetPositive() + "; ";
                }
            }
            return fullCommands;
        }
        public string GetPositive() //default
        {
            if (IsDual)
            {
                return "+" + name;
            }
            return name;
        }

        public string GetPositiveDeclaration()
        {
            if (IsDual)
            {
                return "alias \"+"+name+"\" \""+GetPositiveCommands()+"\"";
            }
            return name;
        }
        public string GetNegative()
        {
            Console.WriteLine("GetNegative(); IsDual: " + IsDual + "; name: " + name);
            if (IsDual)
            {
                return "-" + name;
            }
            return "!";
        }
        public string GetNegativeCommands()
        {
            string fullCommands = "";
            Console.WriteLine(commands.Count);
            for (int i = 0; i < commands.Count; i++)
            {
                Console.WriteLine("for loop! i="+i);
                if (i == commands.Count - 1)
                {//end
                    Console.WriteLine("hey, im in the end part of that if");
                    Console.WriteLine("Adding command " + commands[i].GetPositive()+"which is dual: "+commands[i].GetDual());
                    if (commands[i].GetDual())
                    {
                        fullCommands += commands[i].GetNegative();
                    }
                }
                else
                {//normal
                    Console.WriteLine("hey, im in the normal part of that if");
                    Console.WriteLine("Adding command " + commands[i].GetPositive() + "which is dual: " + commands[i].GetDual());
                    if (commands[i].GetDual())
                    {
                        fullCommands += commands[i].GetNegative() + "; ";
                    }
                }
            }
            for (int i = 0; i < commands.Count; i++)
            {
                Console.WriteLine(commands[i].GetPositive());
            }
            Console.WriteLine(fullCommands);
            return fullCommands;
        }
        
        public string GetNegativeDeclaration()
        {
            if (IsDual)
            {
                return "alias \"-" + name+"\" \""+GetNegativeCommands()+"\"";
            }
            return "!";
        }
    }
}

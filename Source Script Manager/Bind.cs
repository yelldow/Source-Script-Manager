using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Source_Script_Manager
{
    internal class Bind
    {
        public Key key; //what's in between those first quotation marks, stored in class form
        public Command command;//whats in between those second quotation marks
        string command_helper="";
        public Alias Alias;
        //TO DO: TURN COMMAND FROM A STRING INTO A Command.cs OBJECT
        //that way i can keep track MUCH better. and make code even more efficient. right now it sucks working on strings sucks fuck everything fuck strings
        public Bind(string key, string command)
        {
            if (command.Contains('\"'))
            {
                //mark this as an extra-steps kinda bind. no idea WHY the FUCK this bind has quotation marks in it but might as well do what i can to fix it.
            }
            this.key = new Key(Keys.GetIndex(key));
            this.command = new Command(command);
            this.Alias = new Alias("ssm_"+Keys.GetPhrase(this.key.GetKeyIndex()),true);
            this.Alias.AddCommand(this.command);
        }

        public Bind(string fullCommand)
        {
            if (fullCommand[5] == '\"')
            {
                //fuck. i hate parsing quotes.
                //my new least favorite thing to do in programming.
                string[] avoid = { "bind ", "bind", " " };
                string[] split = fullCommand.Split('\"');
                List<string> list = new();
                for (int i = 0; i < split.Length; i++)
                {
                    if (!avoid.Contains(split[i]) && split[i] != "")
                    {
                        list.Add(split[i]);
                    }
                }
                if (list.Count > 2)
                {
                    //wtf
                    Console.WriteLine("unhappy 1");
                    this.key = new Key(0);
                    this.command = new Command("");
                    this.Alias = new Alias("!",false);
                    return;
                }
                else
                {
                    this.key = new Key(Keys.GetIndex(list[0]));
                    this.command_helper = list[1];
                    if (command_helper[0] == ' ')//dumb way of fixing this but i dont care. if only the key has quotes, command will have a space in front of it.
                    {
                        this.command_helper = command_helper[1..];
                    }
                    this.command = new Command(command_helper);
                    this.Alias = new Alias("ssm_" + Keys.GetPhrase(this.key.GetKeyIndex()),true);
                    this.Alias.AddCommand(this.command);
                    return;
                }
            }
            else
            {
                if (fullCommand.Contains('\"'))
                {
                    //quotes somewhere, at least. i'll try to find everything between all quotes, then everything between all spaces.
                    string[] avoid = { "bind ", "bind", " " };
                    string[] split = fullCommand.Split(' ');
                    List<string> list = new();
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (!avoid.Contains(split[i]) && split[i] != "")
                        {
                            list.Add(split[i]);
                        }
                    }
                    if (list.Count > 2) //oh crap! there's more than 2 entries here! wtf!
                    {
                        //wtf
                        Console.WriteLine("unhappy 2");
                        this.key = new Key(0);
                        this.command = new Command("");
                        this.Alias = new Alias("!",false);
                        return;
                    }
                    else
                    {
                        this.key = new Key(Keys.GetIndex(list[0]));
                        this.command_helper = list[1][1..^1];

                        //TODO: figure out why the FUCK i put this here
                        //was i tired
                        if (command_helper.Contains('\"')) //oh crap! there's quotes in here! how did those get in
                        {
                            //wtf
                            Console.WriteLine("unhappy 3");
                            Console.WriteLine(command_helper);
                            this.key = new Key(0); //initialize it to an error. worst case scenario, i put this literally into the end config file
                                              //that'll just confuse source engine for a single console line before it just keeps on keepin on
                            this.command = new Command("");
                            this.Alias = new Alias("!",false);
                            return;
                        }
                        this.command = new Command(command_helper);
                        this.Alias = new Alias("ssm_" + Keys.GetPhrase(this.key.GetKeyIndex()), true);
                        this.Alias.AddCommand(this.command);
                        return;
                    }
                }
                else
                {
                    //look for quotes. if there are none, i'll have to look for everything between all spaces.
                    string[] avoid = { "bind ", "bind", " " };
                    string[] split = fullCommand.Split(' ');
                    List<string> list = new();
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (!avoid.Contains(split[i]) && split[i] != "")
                        {
                            list.Add(split[i]);
                        }
                    }
                    if (list.Count > 2)
                    {
                        //wtf
                        Console.WriteLine("unhappy 4");
                        this.key = new Key(0);
                        this.command = new Command("");
                        this.Alias = new Alias("!", false);
                        return;
                    }
                    else
                    {
                        this.key = new Key(Keys.GetIndex(list[0]));
                        this.command = new Command(list[1]);
                        this.Alias = new Alias("ssm_" + Keys.GetPhrase(this.key.GetKeyIndex()), true);
                        this.Alias.AddCommand(this.command);
                        return;
                    }
                }
            }

        }//god. that was complicated but i think it works.
        public string[] ReturnValues() //moreso just for testing
        {
            return new string[] { Keys.GetPhrase(key.GetKeyIndex()), command_helper };
        }

        /*public string[] ReturnCommands() //return an array containing all commands
        {
            string[] split=command_helper.Split(';');
            for(int i=0; i < split.Length; i++)
            {
                if (split[i][0]==' ')
                {
                    split[i] = split[i][1..];
                }
            }
            return split;
        }
        */
        
        /*public string[] ReturnNegativeCommands() //return an array containing the negative counterparts of all commands
        {
            string[] commands = ReturnCommands();
            List < string > negative= new();
            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i][0] == '+')
                {
                    negative.Add("-"+commands[i][1..]);
                }
            }
            return negative.ToArray();
        }
        */
        public void AppendCommand(string append)
        {
            //this is for adding a new command to my command string.
            //i'll be checking for any ";" or " " in append - if there are, i'll remove them.
            if (append[0]==';')
            {
                if(append[1]==' ')
                {
                    append = append[2..];
                }
                else
                {
                    append = append[1..];
                }
            }
            //then, i'll be mushing it onto command like this
            command_helper = command_helper + "; " + append;
        }
        public string[] ReturnFullBind() 
            //this used to be in Setup() of Manager.cs - however, i figured it'd be better to drop it here for the sake of efficiency.
            //if i need to use this elsewhere (and i'll need to use this elsewhere - reading json files, for example) i'll be good!
        {
            //return "bind \"" + key + "\" \"" + command + "\"";
            string temp_key = Keys.GetPhrase(key.GetKeyIndex());
            List<string> fullBind = new()
            {
                "bind \"" + Keys.GetLiteral(key.GetKeyIndex()) + "\" \"+ssm_" + temp_key + "\"",
                Alias.GetPositiveDeclaration(),
                Alias.GetNegativeDeclaration()
            };
            fullBind.Add(""); //insert an empty line for readability. this is gonna be a little big i'll need everything i can get
            return fullBind.ToArray();
        }
    }
}

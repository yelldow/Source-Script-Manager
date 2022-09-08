using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source_Script_Manager
{
    internal class Command
    {
        //TODO - implement + and - types
        //implement functions for going to and from + and -
        //check if there are spaces in the command. if there are FUCK EVERYTHING this isnt gonna work error out or something

        //this is for storing everything about this command. if it is a +- command, specifically
        //public string GetPlus(){
        //public string GetMinus(){


        string command = "";
        bool IsDual = false;
        bool error = false; //if i delete the object, i'll break things.
                            //i'll instead pass on an error code instead of my command and let other functions deal with it accordingly
        public Command(string command)
        {
            this.command = command;
            if (command[1] == ';')
            {
                if (command[1] == ' ')
                {
                    command = command.Substring(2);
                }
                else
                {
                    command = command.Substring(1);
                }
            }
            if (command[0] == '-' || command[0] == '+')
            {
                IsDual = true; //set this as a plus or minus command
            }
        }

        public Command(string command, bool IsDual)
        {
            this.IsDual = IsDual;
            this.command = command;
            if (command[1] == ';')
            {
                if (command[1] == ' ')
                {
                    command = command.Substring(2);
                }
                else
                {
                    command = command.Substring(1);
                }
            }
        }

        public string GetPositive()
        {
            if (!error)
            {
                if (IsDual)
                {
                    return "+" + command;
                }
                return command;
            }
            return "!";
        }
        public string GetNegative()
        {
            Console.WriteLine("GetNegative called! IsDual: " + IsDual + "; Command: " + command);
            if (!error)
            {
                if (IsDual)
                {
                    return "-" + command;
                }
                return ""; //a - command is only for disabling a toggle. i'm not executing anything upon stopping a toggle, am i?
                           //if i return a command, it'll execute on both the pressing AND releasing of a button. no good.
            }
            return "!";
        }
        public void SetDual(bool IsDual)
        {
            this.IsDual = IsDual;
        }
        public bool GetDual()
        {
            return IsDual;
        }
    }
}

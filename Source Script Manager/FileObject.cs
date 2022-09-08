using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source_Script_Manager
{
    internal class FileObject
    {
        //contains a file path and the data for a source engine .cfg file
        string[]? lines;
        public void Write()
        {
            //writes the file it's got stored
        }

        public void Read(string path)
        {
            //reads a file from a path and sets it's data to the data in the file
            lines = File.ReadAllLines(path);
        }

        public string[] GetLines()
        {
            if (lines != null)
            {
                return lines;
            }
            else
            {
                return new string[1] {""}; //return an empty string with a single line - an empty file. if i find a better way later on i'll modify this code
            }
        }
        public void ConsoleWrite() //debug. writes contents to console
        {
            if (lines != null)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine(lines[i]);
                }
            }
            else
            {
                Console.WriteLine("Error! ConsoleWrite called, but lines is null!");
            }
        }
    }
}

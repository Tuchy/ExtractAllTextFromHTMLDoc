using System;
using System.Text;
using System.IO;

namespace ExtractAllTexts
{
    class ExtractText
    {
        static void ReadFile(string fileName)
        {
            // Check if the file name/path passed as arguments exist 
            if (!File.Exists(fileName))
            {
                // Tell the user the file doesn't exist
                Console.WriteLine("The file '{0}' does not exist.", fileName);
                return;
            }

            StreamReader reader = new StreamReader(fileName);
            using (reader)
            {
                // Read one line at a time until the end of the file
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    line = RemoveTagsAndAttributeValues(line);
                    PrintLine(line);
                }
            }
        }

        static string RemoveTagsAndAttributeValues(string line)
        {
            StringBuilder sb = new StringBuilder(); 
            
            bool isTagOpen = false; // Keep track of opening tag and closing tag

            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];

                // Check if the current char is an opening tag
                if (ch == '<')
                {
                    isTagOpen = true;
                }

                // check if the current letter is a closing tag
                if (ch == '>')
                {
                    isTagOpen = false;
                    continue;
                }

                // Check if the tag is open. If it is closed we add the char to our StringBuilder sb
                if (!isTagOpen)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        static void PrintLine(string line)
        {
            // Check the line has something in it
            if (line != "")
            {
                Console.WriteLine(line.Trim());
            }
            else
            {
                return;
            }
        }
        

        static void Main(string[] args)
        {
            //string fileName = @"D:\Comp science\Exercise files\Sample text.html";

            Console.Write("Enter file name/path to be read(0 to quit): ");
            string fileName = Console.ReadLine();

            do
            {
                // Break out of the loop if the user wants to close the program
                if (fileName == "0")
                {
                    Console.WriteLine("\nThanks for using my application. :)");
                    break;
                }

                // Check if the input is empty
                if (fileName == "")
                {
                    // Prompth the user to enter a path/name
                    Console.WriteLine("Error! Enter a path/name!");
                    Console.Write("Enter file name/path to be read(0 to quit): ");
                    fileName = Console.ReadLine();
                    continue;
                }

                // Read the file and print its content without the tags
                ReadFile(fileName);

                // Update the loop variables
                Console.WriteLine();
                Console.Write("Enter file name/path to be read(0 to quit): ");
                fileName = Console.ReadLine();
            } while (true);

            Console.ReadKey();
        }
    }
}

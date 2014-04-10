using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST
{
    public class SeleniumFixer
    {
        public static void FixInput(String input, String fix)
        {
            // Create a new file to output the results 
            System.IO.StreamWriter output = new System.IO.StreamWriter(input);
            String[] lines = System.IO.File.ReadAllLines(fix);

            int pointer = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    try
                    {
                        string w = words[i];
                        if (words[i].StartsWith("@") && !words[i].StartsWith("@#"))
                        {
                            output.Write(Environment.NewLine + words[i] + " ");
                            Console.Write("\n\r" + words[i] + " ");
                            continue;
                        }
                        if (words[i].StartsWith("@") && words[i].StartsWith("@#"))
                        {
                            output.Write(Environment.NewLine + "@### ");
                            Console.Write("\n\r@### ");
                            i++;
                            i++;
                            continue;
                        }
                        if ((words[i].StartsWith("{$") || words[i].StartsWith("{") || words[i].StartsWith("[{$") || words[i].StartsWith("[{")) &&
                            (words[i].EndsWith("a}") || words[i].EndsWith("A}") || words[i].EndsWith("}") || words[i].EndsWith("a}.") || words[i].EndsWith("A}.") ||
                             words[i].EndsWith("}.") || words[i].EndsWith("a}]") || words[i].EndsWith("A}]") || words[i].EndsWith("}]")))
                        {
                            output.Write("{$a} ");
                            Console.Write("{$a} ");
                            i++;
                            continue;
                        }
                        if (i + 1 <= words.Length && (words[i].StartsWith("{$") || words[i].StartsWith("{") || words[i].StartsWith("[{$") || words[i].StartsWith("[{")) &&
                            (words[i + 1].EndsWith("a}") || words[i + 1].EndsWith("A}") || words[i + 1].EndsWith("}") || words[i + 1].EndsWith("a}.") || words[i + 1].EndsWith("A}.") || words[i + 1].EndsWith("}.") || words[i + 1].EndsWith("a}]") || words[i + 1].EndsWith("A}]") || words[i + 1].EndsWith("}]")))
                        {
                            output.Write("{$a} ");
                            Console.Write("{$a} ");
                            i++;
                            continue;
                        }
                        if (words[i].StartsWith("{$") && words[i + 1].StartsWith("a->") || words[i + 1].StartsWith("A->"))
                        {
                            if (pointer == 0)
                            {
                                output.Write("{$a->issue} ");
                                Console.Write("{$a->issue} ");
                                pointer++;
                                // The number of words varies, must find the next word ending with a curly brace
                                while (!words[i].EndsWith("}"))
                                {
                                    i++;
                                }
                                continue;
                            }

                            output.Write("{$a->userid} ");
                            Console.Write("{$a->userid} ");

                            // The number of words varies, must find the next word ending with a curly brace
                            while (!words[i].EndsWith("}"))
                            {
                                i++;
                            }
                            continue;
                        }
                        if (words[i].StartsWith("(") && (words[i].EndsWith(")") || words[i].EndsWith(").")))
                        {
                            continue;
                        }
                        else
                        {
                            output.Write(words[i] + " ");
                            Console.Write(words[i] + " ");
                            continue;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }
                }
            }

            output.Close();
        }
    }
}

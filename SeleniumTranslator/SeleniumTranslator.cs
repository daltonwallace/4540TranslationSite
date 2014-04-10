using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace ST
{
    public class SeleniumTranslator
    {
        /// <summary>
        /// Translates the input filename into the all the languages specified in the
        /// input parameter array of languages. This method assumes that the file name
        /// is legal and represents plain text (.txt). This method also assumes that 
        /// the languages array in not empty and that each element of the array is 
        /// supported by Google's Translate. Results of this method are output to individual
        /// files named after the translated language.
        /// </summary>
        /// <param name="filename">A plain text file to be translated</param>
        /// <param name="languages">A list of languages to translate to</param>
        public static void TranslateInputFile(String filename, String language, String destination, ref int isDone)
        {
            // Open a new Firefox window
            IWebDriver driver = new FirefoxDriver();
            int sleep = 1000;

            // Translate into each language in the paramter array
            // Manipulate variable 'i' to select destination languages
            //for(int i = 60; i < 81; i++)
            //{
            try
            {
                // Navigate the browser window to Google's Translate URL; specify the target language here
                // driver.Navigate().GoToUrl("http://translate.google.com/#auto/" + languages[i]);
                driver.Navigate().GoToUrl("http://translate.google.com/#auto/" + language);
            }
            catch (Exception e)
            {
                // Either the langauge is not supported or Google has changed the how URLs are generated
                // Console.WriteLine("Unable to translate the following language code: " + languages[i] + ". " + e.Message);
                Console.WriteLine("Unable to translate the following language code: " + language + ". " + e.Message);
                // continue;
            }
            // Create a new file to output the results 
            System.IO.StreamWriter output = new System.IO.StreamWriter(destination);

            // Read in all the lines of text of the parameter file
            String curr = null;
            String[] lines = System.IO.File.ReadAllLines(filename);

            // Send each line of text out to Google Translate via Selenium and retrieve result
            // ID for user input on Google Translate = "source"
            // ID for result     on Google Translate = "result_box"
            for (int j = 0; j < lines.Length; j++)
            {
                if (j == 10)
                {
                    break;
                }
                Console.WriteLine("Lines remaining on this language: " + ((lines.Length) - j).ToString());

                // Skip over empty lines
                if (lines[j].Equals(""))
                {
                    output.WriteLine("");
                    Console.WriteLine();
                    continue;
                }

                // Do not translate query statements
                if (lines[j].Equals("@AND") || lines[j].Equals("@IN") || lines[j].Equals("@WHERE") ||
                   lines[j].Equals("@SELECT") || lines[j].Equals("@FROM"))
                {
                    output.WriteLine(lines[j]);
                    Console.WriteLine(lines[j]);
                    continue;
                }

                // There are four cases:
                // 1) the current line is an entry
                // 2) the current line is part of an entry with lines following
                // 3) the current line is part of an entry with lines before and after
                // 4) the current line is part of an entry and is the end of the entry

                int linesToParse = 1;
                if (lines[j].StartsWith("@"))
                {
                    // Identifies CASE 1:
                    if (j < lines.Length - 1 && lines[j + 1].StartsWith("@"))
                    {
                    }
                    else if (j < lines.Length - 1)
                    {
                        // All other CASES :
                        // Count the number of lines that make up this entry
                        int k = j + 1;
                        while (k < lines.Length)
                        {
                            string temp = lines[k];
                            if (!lines[k].StartsWith("@"))
                            {
                                linesToParse++;
                            }
                            else
                            {
                                break;
                            }
                            k++;
                        }
                    }
                }

                // Use the linesToParse variable as a loop index
                for (int k = 0; k < linesToParse; k++)
                {
                    //bool start = false;
                    bool split = false;

                    // Get the first line and strip the @ off
                    // Increment i here as well

                    String input = lines[j + k];
                    if (k == 0)
                    {
                        input = input.Replace("@", "");
                        //sstart = true;
                    }

                    if (input.Equals(""))
                    {
                        output.Write(System.Environment.NewLine);
                        Console.Write(System.Environment.NewLine);
                        continue;
                    }

                    // Determine if the input has a forward slash
                    String input1;
                    String input2;
                    // int y = input.IndexOf('/');
                    if (input.Contains("/") && !input[input.IndexOf('/') - 1].Equals('<') && !input[input.IndexOf('/') + 1].Equals('>'))
                    {
                        String[] arr = input.Split('/');
                        input1 = arr[0];
                        input2 = arr[1];
                        split = true;
                    }
                    else
                    {
                        input1 = input;
                        input2 = "";
                    }

                    curr = GetTranslation(ref input1, ref driver, ref sleep);

                    /*
                    // Reinsert the @ sign
                    if (start)
                    {
                        output.Write("@");
                        Console.Write("@");
                    }
                    */
                    // Output a @ sign for every line per Dalton's request
                    output.Write("@");
                    Console.Write("@");

                    // Store each result in a new file
                    output.Write(curr);
                    Console.Write(curr);

                    if (split)
                    {
                        curr = GetTranslation(ref input2, ref driver, ref sleep);

                        // Store each result in a new file
                        output.Write("/" + curr);
                        Console.Write("/" + curr);
                    }

                    output.Write(System.Environment.NewLine);
                    Console.Write(System.Environment.NewLine);
                }

                j += linesToParse - 1;
            }

            // Close the output file
            output.Close();
            //}
            isDone = 1;
        }

        /// <summary>
        /// Performs the actual translation process of a string. The input driver uses the
        /// input string to request a translation from Google Translate. The retrieved value
        /// is then returned to the caller.
        /// </summary>
        /// <param name="input">English text to translate</param>
        /// <param name="driver">Web Driver to perform the automation</param>
        /// <param name="sleep">Duration to wait for Google to translate the text</param>
        /// <returns></returns>
        public static String GetTranslation(ref String input, ref IWebDriver driver, ref int sleep)
        {
            String curr = "";

            // Clear the Google Translate input fields and wait for Google to clear result
            driver.FindElement(By.Id("source")).Clear();
            Thread.Sleep(sleep);

            // Input a line to be translated
            driver.FindElement(By.Id("source")).Click();
            driver.FindElement(By.Id("source")).SendKeys(input.ToLower());

            // Wait for Google's response before retrieving result
            Thread.Sleep(sleep);
            curr = driver.FindElement(By.Id("result_box")).Text;

            return curr;
        }
    }
}


using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Desc: This program was created to allow the user to add and view their minutes spent gaming per day in a month.
/// Name: Ganymede
/// Date: 11-11-2025
/// </summary>
namespace Assignment3
{
    internal class Program
    {
        /// <summary>
        /// Main method for Assignment 3.
        /// Program allows the user to enter/save/load/edit/view daily time tracking values from a file.
        /// Allows simple data analysis for a given month.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool continueProgram = true;

            // TODO: 
            // declare a constant to represent the maximum size of the arrays
            // arrays must be large enough to store data for an entire month 
            const int MAX_MONTH_SIZE = 31;
            // TODO:
            // create a string array named dates, using the max size constant you created above to specify the physical size of the array
            string[] dates = new string[MAX_MONTH_SIZE];
            // TODO:
            // create a double array named values, using the max size constant you created above to specify the physical size of the array
            double[] values = new double[MAX_MONTH_SIZE];
            // TODO:
            // create a variable to represent the logical size of the array
            int logicalArraySize = 0;
            int count = 0;

            DisplayProgramIntro();

            // TODO: call DisplayMainMenu()

            DisplayMainMenu();

            while (continueProgram)
            {
                string mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
                Console.WriteLine();

                //MAIN MENU Switch statement
                switch (mainMenuChoice)
                {
                    case "N": //[N]ew Daily Entries

                        if (AcceptNewEntryDisclaimer())
                        {
                            count = EnterDailyValues(dates, values);
                            Console.WriteLine($"\nEntries completed. {count} records in temporary memory.\n");
                            logicalArraySize = count;
                        }
                        else
                        {
                            Console.WriteLine("Cancelling new data entry. Returning to MAIN MENU.");
                        }
                        break;
                    case "S": //[S]ave Entries to File
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before SAVING.");
                        }
                        else if (AcceptSaveEntryDisclaimer())
                        {
                            string filename = PromptForFilename();
                            SaveToFile(filename, dates, values, count);
                        }
                        else
                        {
                            Console.WriteLine("Cancelling save operation. Returning to MAIN MENU.");
                        }

                        break;
                    case "E": //[E]dit Entries
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before EDITING.");
                        }
                        else if (AcceptEditEntryDisclaimer())
                        {
                            EditEntries(dates, values, count);
                        }
                        else
                        {
                            Console.WriteLine("Cancelling EDIT operation. Returning to MAIN MENU.");
                        }
                        break;
                    case "L": //[L]oad  File
                        if (AcceptLoadEntryDisclaimer())
                        {
                            string filename = Prompt("Enter name of file to load: ");
                            count = LoadFromFile(filename, dates, values);
                            Console.WriteLine($"{count} records were loaded.\n");
                        }
                        else
                        {
                            Console.WriteLine("Cancelling LOAD operation. Returning to MAIN MENU.");
                        }
                        break;
                    case "V":
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before VIEWING.");
                        }
                        else
                        {
                            DisplayEntries(dates, values, count);
                        }
                        break;
                    case "M": //[M]onthly Statistics
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before ANALYSIS.");
                        }
                        else
                        {
                            RunAnalysisMenu(dates, values, count);
                        }
                        break;
                    case "D": //[D]isplay Main Menu
                        DisplayMainMenu();
                        break;
                    case "Q": //[Q]uit Program
                        bool quit = Prompt("Are you sure you want to quit (y/n)? ").ToLower().Equals("y");
                        Console.WriteLine();
                        if (quit)
                        {
                            continueProgram = false;
                        }
                        break;
                    default: //invalid entry. Reprompt.
                        Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
                        break;
                }
            }

            DisplayProgramOutro();
        }

        /// <summary>
        /// Runs the analysis sub-menu to display summary metrics.
        /// </summary>
        /// <param name="dates">an array containing dates in YYYY-MM-DD format</param>
        /// <param name="numbers">an array containing numeric values</param>
        /// <param name="count">logical count of elements</param>
        static void RunAnalysisMenu(string[] dates, double[] numbers, int count)
        {
            bool runAnalysis = true;
            string year = dates[0].Substring(4, 4),
                month = dates[0].Substring(1, 3);

            while (runAnalysis)
            {
                string analysisMenuChoice;

                DisplayAnalysisMenu();

                analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
                Console.WriteLine();

                switch (analysisMenuChoice)
                {
                    case "A": //[A]verage 
                        double mean = CalculateMean(numbers, count);
                        Console.WriteLine($"The mean value for {month} {year} is: {mean:N2}.\n");
                        break;
                    case "H": //[H]ighest 
                        double largest = CalculateLargest(numbers, count);
                        Console.WriteLine($"The largest value for {month} {year} is: {largest:N2}.\n");
                        break;
                    case "L": //[L]owest
                        double smallest = CalculateSmallest(numbers, count);
                        Console.WriteLine($"The smallest value for {month} {year} is: {smallest:N2}.\n");
                        break;
                    case "G": //[G]raph 
                        //TODO: call DisplayChart()
                        Prompt("Press <enter> to continue...");
                        break;
                    case "R": //[R]eturn to MAIN MENU
                        runAnalysis = false;
                        break;
                    default: //invalid entry. Reprompt.
                        Console.WriteLine("Invalid reponse. Enter one of the letters to choose a submenu option.");
                        break;
                }
            }
        }

        // ================================================================================================ //
        //                                                                                                  //
        //                                              METHODS                                             //
        //                                                                                                  //
        // ================================================================================================ //

        // ++++++++++++++++++++++++++++++++++++ Difficulty 1 ++++++++++++++++++++++++++++++++++++

        static void DisplayMainMenu()
        {
            Console.WriteLine("Gaming Minutes Charter Program by Ganymede Mayrose\n" +
                "[N]ew Daily Entries\n" +
                "[S]ave Entries to File\n" +
                "[E]dit Entries\n" +
                "[L]oad File\n" +
                "[V]iew Entered / Loaded Data\n" +
                "[M]onthly Statistics\n" +
                "[D]isplay Main Menu\n" +
                "[Q]uit Program");
            Console.Write("Enter main menu option ('D' to display menu: ");
        }


        static void DisplayAnalysisMenu()
        {
            Console.WriteLine("[A]verage \n" +
                "[H]ighest \n" +
                "[L]owest \n" +
                "[G]raph \n" +
                "[R]eturn to Main Menu");
            Console.Write("Enter Analysis Sub-Menu Option: ");
        }

        /// <summary>
        /// Asks user for input
        /// </summary>
        /// <param name="msgLabel"></param>
        /// <returns>User input</returns>
        static string Prompt(string msgLabel)
        {
            bool validInput = false;
            string bring = "bebbebe";
            while (validInput == false)
            {
                Console.Write(msgLabel);
                bring = Console.ReadLine();
                if (bring == null) validInput = false; // check if anything was entered
                else
                {
                    validInput = true;
                }
            }
            return bring;
        }

        /// <summary>
        /// Reads a double and checks if it is a valid number
        /// </summary>
        /// <returns>What was input or error message</returns>
        static double PromptDouble(string msgLabel)
        {
            double bubble = 0.00;
            bool validInput = false;
            Console.Write(msgLabel);
            while (validInput == false)
            {
                if (double.TryParse(Console.ReadLine(), out bubble))
                {
                    validInput = true;
                }
                else
                {
                    Console.Write("Enter a NUMBER: ");
                }
            }
            return bubble;
        }

        /// <summary>
        /// Reads an int and checks if it is a valid number
        /// </summary>
        /// <returns>What was input or an error message</returns>
        static int PromptInt(string msgLabel)
        {
            int glint = 0;
            bool validInput = false;
            Console.Write(msgLabel);
            while (validInput == false)
            {
                if (int.TryParse(Console.ReadLine(), out glint))
                {
                    validInput = true;
                }
                else
                {
                    Console.Write("Enter a whole number (no decimals): ");
                }
            }
            return glint;
        }

        // TODO: create the CalculateLargest() method
        /// <summary>
        /// Flips through a double array to find the largest number in said array
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>Largest number in array</returns>
        static double CalculateLargest(double[] arr, int count)
        {
            double theLargestNumber = 0;
            for (int i = 0; i < count; i++)
            {
                if (arr[i] > theLargestNumber) theLargestNumber = arr[i];
            }
            return theLargestNumber;
        }

        /// <summary>
        /// Flips through an array to find the smallest number in said array
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>Smallest number in array</returns>
        static double CalculateSmallest(double[] arr, int count)
        {
            double theSmallestNumber = 0;
            for (int i = 0; i < count; i++)
            {
                if (arr[i] < theSmallestNumber) theSmallestNumber = arr[i];
            }
            return theSmallestNumber;
        }

        /// <summary>
        /// Calculate mean average of an array
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>The mean</returns>
        static double CalculateMean(double[] arr, int count)
        {
            double mean = 0;
            for (int i = 0;i < count; i++)
            {
                mean += arr[i];
            }
            return mean / arr.Length;
        }

        // ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Allows the user to inform the computer how many minutes of gaming they've accomplished on each day of a month.
        /// </summary>
        /// <param name="dates"></param>
        /// <param name="minutes"></param>
        /// <returns>The count, how many days there are essentially</returns>
        static int EnterDailyValues(string[] dates, double[] minutes)
        {
            int count = 0;
            string[] validMonths = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            string month;
            int monthThatItIs; // numerical value for the month
            bool validInput;
            int year;
            int daysInMonth;
            double mins;
            int day = 0;

            // Let's get the date.
            // get the month
            do
            {
                month = Prompt("Enter month: ").ToUpper();
                month = month.Substring(0, 3);
                if (validMonths.Contains(month)) validInput = true; // if they put in a valid month then they'll be good as golden
                else validInput = false; // if they didn't put in the month they'll have to do it again
            }
            while (validInput == false);
            // get the int of the month that it is (0 - 11)
            monthThatItIs = Array.IndexOf(validMonths, month);
            monthThatItIs++; // this is necessary because the DateTime.DaysInMonth method starts from 1. That means that January is month 1. If we don't make monthThatItIs increment by 1, it will see the array index of January as 0, which crashes it. We don't want it to crash. We want it to work, in fact.
            // now for the year
            validInput = false;
            do
            {
                year = PromptInt("Enter year: ");
                if (year > 0 && year < 9999) validInput = true; // the year needs to be between 0 and 9999 or else DateTime.DaysInMonth() won't work
                else validInput = false;
            }
            while (validInput == false);
            // now to find the days in the month. this will affect february the most due to its leap years
            daysInMonth = DateTime.DaysInMonth(year, monthThatItIs);
            // apply the dates to the array for dates
            for (int i = 0; i < dates.Length; i++)
            {
                dates[i] = Convert.ToString(day + i + 1) + "" + month + "" + Convert.ToString(year);
            }
            // now we gotta enter in the minutes

            while (count < daysInMonth)
            {
                mins = PromptDouble($"Enter minutes for day {count + 1} (0 if the day hasn't happened yet): ");
                minutes[count] = mins; // assign minutes to the array
                count++;
            }
            Console.WriteLine("Maximum entries reached for the month. Returning now...");
            return count;
        }

        /// <summary>
        /// Load values from file into memory.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="dates"></param>
        /// <param name="values"></param>
        /// <returns>Count, how many entries there are</returns>
        static int LoadFromFile(string filename, string[] dates, double[] values)
        {
            int count = 0;
            StreamReader reader = new StreamReader(filename);
            if (File.Exists(filename))
            {
                try
                {
                    while (reader.EndOfStream == false)
                    {
                        string[] lines = reader.ReadLine().Split(',');
                        dates[count] = lines[0];
                        values[count] = Convert.ToDouble(lines[1]);
                        count++;
                    }
                }
                catch
                {
                    Console.WriteLine("Error occurred!");
                }
            }
            reader.Close();
            return count;
        }

        /// <summary>
        /// Take data from memory and save to file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="dates"></param>
        /// <param name="values"></param>
        /// <param name="count"></param>
        static void SaveToFile(string filename, string[] dates, double[] values, int count)
        {
            StreamWriter writer = new StreamWriter(filename);
            for (int i = 0; i < count; i++) // write dates to file
            {
                writer.WriteLine(dates[i] + "," + values[i]);
            }
            writer.Close();
        }

            // TODO: create the DisplayEntries method
            // take data from arrays and display them in console
            /// <summary>
            /// Displays values currently loaded in memory
            /// </summary>
            /// <param name="dates"></param>
            /// <param name="values"></param>
            /// <param name="count"></param>
        static void DisplayEntries(string[] dates, double[] values, int count)
        {
            int[] lineNumbers = new int[count];
            for (int i = 0; i < count; i++) // Go through the arrays and print what's in them
            {
                lineNumbers[i] = i;
                Console.WriteLine(lineNumbers[i] + ": " + dates[i] + ": " + values[i]);
            }
        }

        // ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Allows user to edit their their entries for a file or what they currently have loaded
        /// </summary>
        /// <param name="dates"></param>
        /// <param name="values"></param>
        /// <param name="count"></param>
        static void EditEntries(string[] dates, double[] values, int count)
        {
            string userIsEditing = "N"; // I set it to 'N' so that when the time comes to ask the user if they want to continue editing 'N' while be the default option
            string seeValues;
            int[] lineNumbers = new int[count];
            int userLineNumber;
            double userValue;
            bool validInput = false;
            for (int i = 0; i < count; i++) // generating line numbers for editing
            {
                lineNumbers[i] = i;
            }
            do
            {
                seeValues = Prompt("Do you want to see the loaded values? (Y/N): ").ToUpper();
                while (validInput == false)
                {
                    if (seeValues == "Y")
                    {
                        DisplayEntries(dates, values, count);
                        seeValues = "N";
                        validInput = true;
                    }
                    else if (seeValues == "N")
                    {
                        Console.WriteLine("Okay.");
                        validInput = true;
                    }
                    else
                    {
                        Prompt("Invalid input. Enter (Y/N) to see loaded values: ").ToUpper();
                        validInput = false;
                    }
                }
                userLineNumber = PromptInt("Enter line number: ");
                userValue = PromptDouble("Enter value for line: ");
                values[userLineNumber] = userValue;
                userIsEditing = Prompt("Do you want to continue editing? (y/N): ");
                if (userIsEditing == null) userIsEditing = "N";
            }
            while (userIsEditing == "Y");

        }


        // ++++++++++++++++++++++++++++++++++++ Difficulty 4 ++++++++++++++++++++++++++++++++++++

        // TODO: create the DisplayChart method



        // ********************************* Helper methods *********************************

            /// <summary>
            /// Displays the Program intro.
            /// </summary>
            static void DisplayProgramIntro()
        {
            Console.WriteLine("****************************************\n" +
                "*                                      *\n" +
                "*          Monthly  Game Time          *\n" +
                "*                                      *\n" +
                "****************************************\n");
        }

        /// <summary>
        /// Displays the Program outro.
        /// </summary>
        static void DisplayProgramOutro()
        {
            Console.Write("Program terminated. Press ENTER to exit program...");
            Console.ReadLine();
        }

        /// <summary>
        /// Displays a disclaimer for NEW entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptNewEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.\n" +
                "Hint: Select EDIT from the main menu instead, to change individual days.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for SAVE entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptSaveEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: saving to an EXISTING file will overwrite data currently on that file.\n" +
                "Hint: Files will be saved to this program's directory by default.\n" +
                "Hint: If the file does not yet exist, it will be created.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for EDIT entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptEditEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: editing will overwrite unsaved values.\n" +
                "Hint: Save to a file before editing.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for LOAD entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptLoadEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.\n" +
                "Hint: If you entered New Daily entries, save them first!\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays prompt for a filename, and returns a valid filename. 
        /// Includes exception handling.
        /// </summary>
        /// <returns>User-entered string, representing valid filename (.txt or .csv)</returns>
        static string PromptForFilename()
        {
            string filename = "";
            bool isValidFilename = true;
            const string CSV_FILE_EXTENSION = ".csv";
            const string TXT_FILE_EXTENSION = ".txt";

            do
            {
                filename = Prompt("Enter name of .csv or .txt file to save to (e.g. JAN-2025-data.csv): ");
                if (filename == "")
                {
                    isValidFilename = false;
                    Console.WriteLine("Please try again. The filename cannot be blank or just spaces.");
                }
                else
                {
                    if (!filename.EndsWith(CSV_FILE_EXTENSION) && !filename.EndsWith(TXT_FILE_EXTENSION)) //if filename does not end with .txt or .csv.
                    {
                        filename = filename + CSV_FILE_EXTENSION; //append .csv to filename
                        Console.WriteLine("It looks like your filename does not end in .csv or .txt, so it will be treated as a .csv file.");
                        isValidFilename = true;
                    }
                    else
                    {
                        Console.WriteLine("It looks like your filename ends in .csv or .txt, which is good!");
                        isValidFilename = true;
                    }
                }
            } while (!isValidFilename);
            return filename;
        }

    }
}
/// <summary>
/// Desc: This program is 
/// Name: Ganymede
/// Date: First Half of November, 2025
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
            // create a double array named minutes, using the max size constant you created above to specify the physical size of the array
            double[] minutes = new double[MAX_MONTH_SIZE];
            // TODO:
            // create a variable to represent the logical size of the array
            int logicalDateSize = 0;
            int logicalMinuteSize = 0;
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
                            // TODO: call EnterDailyValues & assign its return value
                            Console.WriteLine($"\nEntries completed. {count} records in temporary memory.\n");
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
                            // TODO: call SaveToFile()
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
                            //TODO: call EditEntries()
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
                            // TODO: call LoadFromFile() and assign its return value
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
                            // TODO: call DisplayEntries()
                        }
                        break;
                    case "M": //[M]onthly Statistics
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before ANALYSIS.");
                        }
                        else
                        {
                            RunAnalysisMenu(dates, minutes, count);
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
            string year = dates[0].Substring(0, 4),
                month = dates[0].Substring(5, 3);

            while (runAnalysis)
            {
                string analysisMenuChoice;

                // TODO: call DisplayAnalysisMenu()

                analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
                Console.WriteLine();

                switch (analysisMenuChoice)
                {
                    case "A": //[A]verage 
                        // TODO: uncomment the next 2 lines & call CalculateMean();
                        //double mean = ;
                        //Console.WriteLine($"The mean value for {month} {year} is: {mean:N2}.\n");
                        break;
                    case "H": //[H]ighest 
                        // TODO: uncomment the next 2 lines & call CalculateLargest();
                        //double largest = ;
                        //Console.WriteLine($"The largest value for {month} {year} is: {largest:N2}.\n");
                        break;
                    case "L": //[L]owest 
                        //TODO: uncomment the next 2 lines & call CalculateSmallest();
                        //double smallest = ;
                        //Console.WriteLine($"The smallest value for {month} {year} is: {smallest:N2}.\n");
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

        // TODO: create the DisplayMainMenu() method
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

        // TODO: create the DisplayAnalysisMenu() method

        static void DisplayAnalysisMenu()
        {
            Console.WriteLine("[A]verage \n" +
                "[H]ighest \n" +
                "[L]owest \n" +
                "[G]raph \n" +
                "[R]eturn to Main Menu");
            Console.Write("Enter Analysis Sub-Menu Option: ");
        }

        // TODO: create the Prompt method

        static string Prompt(string msgLabel)
        {
            Console.Write(msgLabel);
            return Console.ReadLine();
        }

        // TODO: create the PromptDouble() method
        /// <summary>
        /// Reads a double and checks if it is a valid number
        /// </summary>
        /// <returns>Input or error message</returns>
        static double PromptDouble()
        {
            double bubble = 0.00;
            if (double.TryParse(Console.ReadLine(), out bubble))
            {
                return bubble;
            }
            else
            {
                Console.Write("Please enter an actual number: ");
                return 0.00;
            }
        }

        // optional TODO: create the PromptInt() method



        // TODO: create the CalculateLargest() method



        // TODO: create the CalculateSmallest() method



        // TODO: create the CalculateMean() method



        // ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++

        // TODO: create the EnterDailyValues method



        // TODO: create the LoadFromFile method



        // TODO: create the SaveToFile method



        // TODO: create the DisplayEntries method



        // ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

        // TODO: create the EditEntries method



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
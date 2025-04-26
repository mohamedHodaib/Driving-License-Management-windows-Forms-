using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;


namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;

        public static void RememberUsernameAndPassword(string Username, string Password, bool IsRememberClicked)
        {

            try
            {
                string RememberedUserName = "";
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";
                if (File.Exists(filePath))
                {
                    using (StreamReader Reader = new StreamReader(filePath))
                    {
                        string RememberedUser = Reader.ReadLine();
                        RememberedUserName = RememberedUser.Substring(0, RememberedUser.IndexOf('#'));
                    }
                }

                if (IsRememberClicked)
                {
                    // Create a StreamWriter to write to the file
                    if (Username != RememberedUserName)
                    {
                        // concatonate username and passwrod withe seperator.
                        string dataToSave = Username + "#//#" + Password;
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            // Write the data to the file
                            writer.WriteLine(dataToSave);
                        }
                    }
                }
                else
                {
                    if (Username == RememberedUserName)
                    {
                        File.WriteAllText(filePath, string.Empty);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }


        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath  = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;   
            }

        }
    }
}

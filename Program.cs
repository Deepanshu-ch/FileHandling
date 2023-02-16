// Create a Folder in a drive using DirectoryInfo class.
// Ask file name and create a file with that name and store in created folder.
// Ask for person details (Name, Mobile no) and save information in that file.
// Print following option on console screen. View Saved File

using System;
using System.IO;
using System.Text;

namespace FileHandling
{
    static class Program
    {
        static void Main(string[] args)
        {

            string directoryPath = @"D:\Step2Gen\Practice\C#\User Details";  // Verbatim Literal
            string fileName;
            string name, phoneNumber;

            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            directory.Create();   // this will create folder name MyDirectory at specified path

            Console.Write("Enter a file name:");
            fileName = Console.ReadLine();

            string filePath = directoryPath + "\\" + fileName + ".txt";  // Setting path to create file with user defined name

            start:    // this is to start the program if file didn't exist already.

            // Check if file exist or not

            if (File.Exists(filePath))
            {
                FileStream fout;

                try  // this step can also be ignored file can directly be opened with help of stream writer class.
                {
                    fout = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                }
                catch (IOException exc)
                {
                    Console.WriteLine(exc.Message);
                    return;
                }

                // Now, lets open Stream writer to write user details in our program

                StreamWriter file = null;

                try
                {
                    file = new StreamWriter(fout);

                    again:
                   
                    // int userCount = 1;

                    Console.Write("Enter your Name:");
                    name = Console.ReadLine();
                    Console.Write("Enter your Phone Number:");
                    phoneNumber = Console.ReadLine();

                    file.WriteLine();

                    //file.WriteLine($"User {userCount}");
                    file.WriteLine($"Name : {name}");
                    file.WriteLine($"Phone Number : {phoneNumber}");
                    
                    //userCount++;

                    Console.Write("Do you want to add more details:");
                    if (Console.ReadLine() == "yes")
                        goto again;

                }
                catch (IOException exc)
                {
                    Console.WriteLine(exc.Message);
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }

                // Now lets read the content of the file
                // We can directly open a file with stream Reader
                // or like we did above first open it with fileStream and
                // then with stream reader both are correct way of witting a code.

                using (StreamReader readFile = new StreamReader(filePath))
                {
                    string userDetails = readFile.ReadToEnd();
                    Console.WriteLine(userDetails);
                }// here using will automatically close the file.

            }
            else
            {
                // Here I'm just creating a file this can also be done without this Else Block
                // But I'm doing this for sake of Practice Only 

                using (FileStream fin = new FileStream(filePath, FileMode.Create, FileAccess.Write))  // using statement helps in auto closing of file
                {
                    string data = "\nUser Details:\n";
                    byte[] write_data = Encoding.UTF8.GetBytes(data);  // One way of writing data in file
                    fin.Write(write_data, 0, data.Length);
                } // here file will close automatically no need to use fin.close

                // here we just don't want to create a file but also write in it so with use of goto let's restart the code
               goto start;
            }

        }
    }
}
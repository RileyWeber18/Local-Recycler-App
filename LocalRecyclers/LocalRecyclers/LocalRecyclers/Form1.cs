/*
    
Filename:   LocalRecyclers	
Author:     Aaron Pidd
Date:       09 May 2022
Version:    1.0
Notes:      Application thats reads and writes recyclers.csv
            recyclerList = new List<Recycler>();
            ReadRecylerData();
            recyclerList.Sort();
            DisplayRecyclerData();
            DisplayCurrentRecord();
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalRecyclers
{
    /// <summary>
    /// partial class localRecyclers_Form : Form
    /// recyclerList of recycler data
    /// stored in external file - recyclers.csv
    /// private List<Recycler> recyclerList;
    /// current selected record (index value)
    /// private int currentRecord;
    /// </summary>
    public partial class localRecyclers_Form : Form
    {
        
        // recyclerList of recycler data
        // stored in external file - recyclers.csv
        private List<Recycler> recyclerList;

        // current selected record (index value)
        private int currentRecord;
        private List<string> recycledFilter;
        // constuctor method
        /// <summary>
        /// public localRecyclers_Form() Constuctor Method
        /// instantiate the recyclerList
        /// recyclerList = new List<Recycler>();
        /// Contains Methods:
        /// ReadRecylerData() Read external file data and set up recyclerList 
        /// recyclerList.Sort() Sort the recyclerList
        /// DisplayRecyclerData() Display recycler data in recycler textbox
        /// set current record
        /// currentRecord = 0;
        /// DisplayCurrentRecord() Display recycler data for business as per currentRecord selected
        /// </summary>
        public localRecyclers_Form()
        {
            

            InitializeComponent();

            // instantiate the recyclerList
            recyclerList = new List<Recycler>();
            recycledFilter = new List<string>();

            // read external file data and set up recyclerList
            ReadRecylerData();

            // sort the recyclerList
            recyclerList.Sort();

            // display recycler data in recycler textbox
            DisplayRecyclerData();

            // set current record
            currentRecord = 0;
            // display recycler data for business as per currentRecord selected
            DisplayCurrentRecord();
            
            filter_ComboBox.Items.Clear();
            for (int i = 0; i < recycledFilter.Count; i++)
            {
                filter_ComboBox.Items.Add(recycledFilter[i]);
            }

        } // END constructor method

        // ReadRecylerData() 
        /// <summary>
        /// ReadRecylerData method: Read external file data and set up recyclerList 
        /// Get external file source 
        /// Read external file
        /// StreamReader Object
        /// Split the line into a string array
        /// Assign array values to string variables
        /// Create Recycler instance
        /// Add instance to recyclerList
        /// file.Close()
        /// </summary>
        public void ReadRecylerData()
        {
            // get external file source 
            string filePath = @"recyclers.csv";

            // read external file
            try
            {
                if (File.Exists(filePath))
                {
                    
                    using (StreamReader file = new StreamReader(filePath))
                    {
                        
                        string line;
                        // int lineCount = 0;
                        
                        while ((line = file.ReadLine()) != null)
                        {
                            /*lineCount++;
                            //if (lineCount == 1)
                            {
                                continue;
                            }
                            */
                            
                            
                            //split the line into a string array
                            string[] lineArray = line.Split(',');
                            // assign array values to string variables
                            string name = lineArray[0];
                            string address = lineArray[1];
                            string phone = lineArray[2];
                            string website = lineArray[3];
                            string recycles = lineArray[4];
                            
                            // create Recycler instance
                            Recycler recycler = new Recycler(name, address, phone, website, recycles);
                            // add instance to recyclerList
                            recyclerList.Add(recycler);

                            if (! recycledFilter.Contains(recycles))
                            {
                                recycledFilter.Add(recycles);
                            }

                        } // end while loop
                        file.Close();
                    } // end using StreamReader file
                } // end if
                else
                {
                    Console.WriteLine("ERROR: No external file exists for: " + filePath);
                }
            } // end try block
                catch (IOException ioe)
                {
                    Console.WriteLine("ERROR: Problem in reading the external file: " + filePath);
                    Console.WriteLine(ioe.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Problem with external file: " + filePath);
                    Console.WriteLine(e.Message);
                }
        } // END ReadRecyclerData() method

        // DisplayRecyclerData () method
        /// <summary>
        /// DisplayRecyclerData method
        /// Displays Busness Name, Phone and Website in table
        /// </summary>
        public void DisplayRecyclerData()
        {
            
            String displayText = "Name\t\t\t\tPhone\t\t\t\tWebsite\t\t\r\n";
            displayText += "----------------------------------------------------------------------------------------------------------------------------------------------------\r\n";
            for (int i = 0; i < recyclerList.Count(); i++)
            {
                displayText += recyclerList[i].Name + "\t\t" + recyclerList[i].Phone + "\t" + recyclerList[i].Website + "\r\n";
            }
            recyclerListing_TextBox.Text = displayText;
        } // END DisplayRecyclerData() method

        // DisplayCurrentRecord() method
        /// <summary>
        /// DisplayCurrentRecord method
        /// Displays data in textboxes
        /// </summary>
        public void DisplayCurrentRecord()
        {
            
            businessName_TextBox.Text = recyclerList[currentRecord].Name;
            address_TextBox.Text = recyclerList[currentRecord].Address;
            phone_TextBox.Text = recyclerList[currentRecord].Phone;
            website_TextBox.Text = recyclerList[currentRecord].Website;
            recycles_TextBox.Text = recyclerList[currentRecord].Recycles;
            
        } // END DisplayCurrentRecord()

        /// <summary>
        ///  clear_Button Event Handler
        ///  Clears textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_Button_Click(object sender, EventArgs e)
        {
            businessName_TextBox.Text = "";
            address_TextBox.Text = "";
            phone_TextBox.Text = "";
            website_TextBox.Text = "";
            recycles_TextBox.Text = "";
        } // END clear_Button_Click event handler

        // validate inputs
        /// <summary>
        /// IsRecyclerValid Method
        /// Validates inputs for textboxes
        /// Declare Boolean, name it “recyclerStatus” assign it a value of “true”
        /// Check name
        /// if (String.IsNullOrEmpty(businessName_TextBox.Text))
        /// {
        /// recyclerStatus = false;
        /// errorMessage += "The Business name is required\n";
        /// }
        /// Repeat for
        /// Check address
        /// Check phone
        /// Check website
        /// Check recycles
        /// Display error message if there are errors encountered
        /// </summary>
        /// <returns> recyclerStatus </returns>
        public bool IsRecyclerValid()
        {
            //Declare Boolean, name it “recyclerStatus” assign it a value of “true”
            bool recyclerStatus = true;
            string errorMessage = "ERROR(S) encountered\n";
            // 1. check name
            if (String.IsNullOrEmpty(businessName_TextBox.Text))
            {
                recyclerStatus = false;
                errorMessage += "The Business name is required\n";
                
            }
            // 2. check address
            if (String.IsNullOrEmpty(address_TextBox.Text))
            {
                recyclerStatus = false;
                errorMessage += "Address is required\n";
                
            }
            // 3. check phone
            if (String.IsNullOrEmpty(phone_TextBox.Text))
            {
                recyclerStatus = false;
                errorMessage += "Phone number is required\n";
                
            }
            // 4. check website
            if (String.IsNullOrEmpty(website_TextBox.Text))
            {
                recyclerStatus = false;
                errorMessage += "Website is required\n";
                
            }
            // 5. check recycles
            if (String.IsNullOrEmpty(recycles_TextBox.Text))
            {
                recyclerStatus = false;
                errorMessage += "What the Business Recycles is required\n";
                
            }
            // display error message if there are errors encountered
            if (recyclerStatus == false)
            {
                MessageBox.Show(errorMessage, "ERRORS!");
            }
            return recyclerStatus;

        } // END IsRecyclerValid() method


        // Add new record button
        /// <summary>
        /// Add New Record Button Event Handler
        /// Adds new record to recycler csv file
        /// prompt the user to proceed with the save
        /// if Yes button clicked, then proceed
        /// Get all 5 values for the new recycler
        /// Create new recycler object
        /// Add to the recyclerList
        /// re-sort the recyclerList
        /// display newly sorted recycler list
        /// get index number of new recycler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNew_Button_Click(object sender, EventArgs e)
        {
            if (IsRecyclerValid() == false)
            {
                return;
            }
            else
            {
                // prompt the user to proceed with the save
                DialogResult dialogResult = MessageBox.Show("Do you wish to proceed in adding this new Recycler record?", "NEW RECYCLER RECORD", MessageBoxButtons.YesNo);
                // if Yes button clicked, then proceed
                if (dialogResult == DialogResult.Yes)
                {
                    // proceed
                    // get all 5 values for the new recycler
                    string name = businessName_TextBox.Text;
                    string address = address_TextBox.Text;
                    string phone = phone_TextBox.Text;
                    string website = website_TextBox.Text;
                    string recycles = recycles_TextBox.Text;
                    // create new recycler object
                    Recycler newRecycler = new Recycler(name, address, phone, website, recycles);
                    // add to the recyclerList
                    recyclerList.Add(newRecycler);
                    // re-sort the recyclerList
                    recyclerList.Sort();
                    // get index number of new recycler
                    currentRecord = recyclerList.IndexOf(newRecycler);
                    // display newly sorted recycler list
                    DisplayRecyclerData();
                }
            }
        
        } // END addNew_Button_Click event handler

        /// <summary>
        /// update Button Event Handler
        /// Updates record of current business
        /// Prompt the user to proceed with the save
        /// if Yes button clicked, then proceed
        /// Get all 5 values for the new recycler
        /// Create new recycler object
        /// Change current record recyclerList
        /// re-sort the recyclerList
        /// Display newly sorted recycler list
        /// Get index number of new recycler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_Button_Click(object sender, EventArgs e)
        {
            if (IsRecyclerValid() == false)
            {
                return;
            }
            else
            {
                string originalName = recyclerList[currentRecord].Name;
                // prompt the user to proceed with the save
                DialogResult dialogResult = MessageBox.Show("Do you wish to proceed in updating the record for " + originalName + "?", "UPDATE EXISTING ENROLMENT RECORD", MessageBoxButtons.YesNo);
                // if Yes button clicked, then proceed
                if (dialogResult == DialogResult.Yes)
                {
                    // proceed
                    // get all 5 values for the new recycler
                    string name = businessName_TextBox.Text;
                    string address = address_TextBox.Text;
                    string phone = phone_TextBox.Text;
                    string website = website_TextBox.Text;
                    string recycles = recycles_TextBox.Text;
                    // create new recycler object
                    Recycler updatedRecycler = new Recycler(name, address, phone, website, recycles);
                    // change current record recyclerList
                    recyclerList[currentRecord] = updatedRecycler;
                    // re-sort the recyclerList
                    recyclerList.Sort();
                    // get index number of new recycler
                    currentRecord = recyclerList.IndexOf(updatedRecycler);
                    // display newly sorted recycler list
                    DisplayRecyclerData();
                    
                }
            }

        } // END UPDATE BUTTON EVENT HANDLER

        /// <summary>
        /// Previous Button Event Handler
        /// Go to previous record in recycler list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previous_Button_Click(object sender, EventArgs e)
        {
            // go to the previous record in the recyclerList
            currentRecord--;
            // check if currentRecord not less than zero
            // if so, then assign to last element index
            // (or Count – 1) of the recyclerList
            // this being the last record
            if (currentRecord < 0)
            {
                currentRecord = recyclerList.Count - 1;
            }
            // display this record in the recycler listing
            DisplayCurrentRecord();

        }// END previos button event handler

        /// <summary>
        /// next Button Event Handler
        /// Go to next record in recycler list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Button_Click(object sender, EventArgs e)
        {
            // go to the next record in the recyclerList
            currentRecord++;
            // check if currentRecord not greater than the list length - 1
            // if so, then assign to first element index [0]
            // of the enrolmentList
            // this being the first record
            if (currentRecord == recyclerList.Count)
            {
                currentRecord = 0;
            }
            // display this record in the recycler listing
            DisplayCurrentRecord();
        } // END next button event handler

        /// <summary>
        /// Save and Exit Button Event Handler
        /// Save all records from the list
        /// Save to external file
        /// StreamWriter object
        /// Write out headings
        /// Loop through each instance
        /// Write a line for each instance
        /// Close the file
        /// Exists application, WinForms app, Console app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You are to exit the application - do you wish to SAVE changes to all records?", "SAVE!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // save all records from the list
                if (recyclerList.Count > 0)
                {
                    // save to external file
                    try
                    {
                        string filePath = @"recyclers.csv";
                        // StreamWriter object
                        StreamWriter sw = new StreamWriter(filePath);
                        // Write out headings
                        sw.WriteLine("Name,Address,Phone,Website,Recycles");
                        // loop through each instance
                        // and write a line for each instance
                        for (int i = 0; i < recyclerList.Count; i++)
                        {
                            sw.WriteLine(recyclerList[i].ToCSVString());
                        }
                        //Close the file
                        sw.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: Problem in writing to external file!", "ERROR!");
                    }
                }
            }
            // exit the application
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }

        } // END EXIT button event handler


        // BinarySearch() method searches a string value in a List with Enrolment objects
        // if the method returns -1, then the string is NOT found
        // otherwise if it returns 0 or more, then the string is found
        /// <summary>
        /// BinarySearch Method
        /// Loop while foundStatus is false AND first is less than or equal to last
        /// Get mid index value
        /// Check if string to search is less than the value positioned in the middle of the sorted list
        /// if it is, then change the last position to that of the middle less 1
        /// Check if string to search is greater than the value positioned in the middle of the sorted list
        /// if it is, then change the first position to that of the middle plus 1
        /// Otherwise, the value has been found then break out of the loop 
        /// </summary>
        /// <param name="recyclerList"></param>
        /// <param name="businessNameToSearch"></param>
        /// <returns> foundIndex </returns>
        static int BinarySearch(List<Recycler> recyclerList, string businessNameToSearch)
        {
            int foundIndex = -1;
            int first = 0;
            int last = recyclerList.Count - 1;
            int mid;

            // loop while foundStatus is false AND first is less than or equal to last
            while (first <= last)
            {
                // get mid index value
                mid = (first + last) / 2;

                // check if string to search is less than the value positioned in the middle of the sorted list
                // if it is, then change the last position to that of the middle less 1
                // this way, last becomes the last value in the sorted upper half of the array
                if (businessNameToSearch.CompareTo(recyclerList[mid].Name) < 0)
                {
                    last = mid - 1;
                }
                // check if string to search is greater than the value positioned in the middle of the sorted list
                // if it is, then change the first position to that of the middle plus 1
                // this way, first becomes the first value in the sorted lower half of the array
                else if (businessNameToSearch.CompareTo(recyclerList[mid].Name) > 0)
                {
                    first = mid + 1;
                }
                // otherwise, the value has been found
                // if found, then break out of the loop
                else
                {
                    foundIndex = mid;
                    break;
                }
            }

            return foundIndex;
        }

        /// <summary>
        /// Binary Search Button Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void binarySearch_Button_Click(object sender, EventArgs e)
        {
            currentRecord = BinarySearch(recyclerList, find_TextBox.Text);
            if (currentRecord < 0)
            {
                MessageBox.Show("Not Present");
            }
            else
            {
                DisplayCurrentRecord();
            }
        }

        /// <summary>
        /// First Botton Event Handler
        /// Goes to first listing of currentrecord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void first_Button_Click(object sender, EventArgs e)
        {
            currentRecord = 0;
            DisplayCurrentRecord();
        }

        /// <summary>
        /// Last Button Event Handler
        /// Goes to last listing of currentrecord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            currentRecord = recyclerList.Count -1 ;
            DisplayCurrentRecord();
        }

        /// <summary>
        /// delete Button Event Handler
        /// Deletes current record from recycler csv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_Button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you wish to proceed in deleting a record ", "DELETE", MessageBoxButtons.YesNo);
            // if Yes button clicked, then proceed
            if (dialogResult == DialogResult.Yes)
            {
                if (recyclerList.Count > 1)
                {
                    // remove selected item 
                    recyclerList.Remove(recyclerList[currentRecord]);
                    // re sort the list 
                    recyclerList.Sort();

                    DisplayRecyclerData();
                }
            }
        } // END Delete Button event handler

        private void filter_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filter_ComboBox.SelectedIndex >= 0)
            {
                string filter = filter_ComboBox.SelectedItem.ToString();

                String displayText = "Name\t\t\t\tPhone\t\t\t\tWebsite\t\t\r\n";
                displayText += "----------------------------------------------------------------------------------------------------------------------------------------------------\r\n";
                for (int i = 0; i < recyclerList.Count(); i++)
                {
                    if (filter.Equals(recyclerList[i].Recycles))
                        displayText += recyclerList[i].Name + "\t\t" + recyclerList[i].Phone + "\t" + recyclerList[i].Website + "\r\n";
                }
                recyclerListing_TextBox.Text = displayText;
            }

        }

        private void businessName_TextBox_TextChanged(object sender, EventArgs e)
        {

        }
    } // END class localRecyclers_Form : Form

} // END namespace LocalRecyclers

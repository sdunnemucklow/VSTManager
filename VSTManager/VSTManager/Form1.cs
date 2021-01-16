// Original work Copyright (c) 2017 Samuel Dunne-Mucklow

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace VSTManager
{
    public partial class Form1 : Form
    {
        // URL for the store the user is currently browsing
        public string CurrentStoreUrl = "https://getdunne.net/Krakli/";

        // The list of VSTs available from the current store. Each VST is itself represented by a list of attributes, so it's a nested list
        public List<List<string>> StoreVstList = new List<List<string>>();

        // The list of VSTs already installed by the user in the current VST directory, also a nested list
        public List<List<string>> LocalVstList = new List<List<string>>();

        // The user's currently selected VST directory
        public string LocalPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("VST Manager {0}", Application.ProductVersion);
            
            // Read or create the CSV files containing lists of past stores visited and VST directories
            if (!File.Exists("past_stores.csv"))
            {
                File.Create("past_stores.csv").Close();
            }
            if (!File.Exists("locations.csv"))
            {
                File.Create("locations.csv").Close();
            }

            // read the CSV files and populate the combo boxes
            List<string> storesList = new List<string>();
            ReadCsvToSimpleList("past_stores.csv", storesList);
            PopulateComboBox(storesList, storesBox);
            List<string> locationList = new List<string>();
            ReadCsvToSimpleList("locations.csv", locationList);
            PopulateComboBox(locationList, locationBox);

            // create local.csv in all user-chosen directories if they don't already exist
            foreach (string s in locationList)
            {
                if (!File.Exists(Path.Combine(s, "local.csv")))
                {
                    File.Create(Path.Combine(s, "local.csv")).Close();
                }
            }
        }

        private void ReadCsvToNestedList(string csvPath, List<List<string>> csvList)
        {
            csvList.Clear();

            try
            {
                using (var reader = new StreamReader(csvPath))
                {
                    bool hasTitles = false;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line.StartsWith("name,"))
                        {
                            hasTitles = true;
                        }
                        List<string> smallList = new List<string>();
                        foreach (string s in line.Split(','))
                        {
                            smallList.Add(s);
                        }
                        csvList.Add(smallList);
                    }
                    if (hasTitles)
                    {
                        csvList.RemoveAt(0);
                    }
                }
            }
            catch
            {
                MessageBox.Show("The CSV could not be read.");
            }
        }

        private void ReadCsvToSimpleList(string csvPath, List<string> csvList)
        {
            csvList.Clear();

            try
            {
                using (var reader = new StreamReader(csvPath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        csvList.Add(line);
                    }
                }
            }
            catch
            {
                MessageBox.Show("The CSV could not be read.");
            }
        }

        // Add a line to a CSV, unless an identical line is already in the file
        private void AddLineToCsvFile(string csvPath, string lineToAdd)
        {
            bool alreadyExists = false;
            if (File.Exists(csvPath))
            {
                try
                {
                    using (var reader = new StreamReader(csvPath))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();

                            if (line == lineToAdd)
                            {
                                alreadyExists = true;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("The CSV could not be read.");
                }
            }

            if (!alreadyExists)
            {
                File.AppendAllText(csvPath, lineToAdd + Environment.NewLine);
            }
        }

        public void DeleteLineFromCsvFile(string csvPath, string lineToDelete)
        {
            // Rewrite the CSV without the specific line to delete
            string[] values = File.ReadAllLines(csvPath);
            StreamWriter Writer = new StreamWriter(csvPath, false);

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != lineToDelete)
                {
                    Writer.WriteLine(values[i]);
                }
            }

            Writer.Close();
        }

        private int DownloadAndExtractZipFile(string FileName, string DownloadURL)
        {
            string FullPath = Path.Combine(LocalPath, FileName);
            // remove the zip file if it was left there
            if (File.Exists(FullPath + ".zip"))
            {
                File.Delete(FullPath + ".zip");
            }

            // download the file
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(new Uri(DownloadURL), FullPath + ".zip");
                }
                catch
                {
                    return 1;
                }
            }

            // if it's already there, move it somewhere else temporarily, but move it back if the installation fails
            if (Directory.Exists(FullPath))
            {
                Directory.Move(FullPath, FullPath + "_old");
            }
            try
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(FullPath + ".zip", FullPath);
            }
            catch
            {
                if (File.Exists(FullPath + ".zip"))
                {
                    File.Delete(FullPath + ".zip");
                }
                if (Directory.Exists(FullPath + "_old"))
                {
                    Directory.Move(FullPath + "_old", FullPath);
                }
                return 1;
            }
            if (File.Exists(FullPath + ".zip"))
            {
                File.Delete(FullPath + ".zip");
            }
            if (Directory.Exists(FullPath + "_old"))
            {
                Directory.Delete(FullPath + "_old", true);
            }
            return 0;
        }

        private void PopulateCheckedBox(List<List<string>> vstList, CheckedListBox checkedBox)
        {
            checkedBox.Items.Clear();

            // add all of the first elements (name) of each list to the checked box
            foreach (List<string> l in vstList)
            {
                checkedBox.Items.Add(l[0]);
            }
        }

        private void PopulateComboBox(List<string> storeList, ComboBox comboBox)
        {
            comboBox.Items.Clear();

            // add all of the elements in the list to the combo box
            foreach (string s in storeList)
            {
                comboBox.Items.Add(s);
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (storesBox.Text == "")
            {
                MessageBox.Show("Please enter the VST Store URL.");
                return;
            }

            //set CurrentStoreUrl based on storesBox.Text
            CurrentStoreUrl = storesBox.Text;

            if (CurrentStoreUrl.Length < 7 || CurrentStoreUrl.Substring(0, 4) != "http")
            {
                CurrentStoreUrl = "http://" + CurrentStoreUrl;
            }
            if (CurrentStoreUrl[CurrentStoreUrl.Length - 1] != '/')
            {
                CurrentStoreUrl = CurrentStoreUrl + "/";
            }

            //download the store CSV from CurrentStoreUrl to store.csv
            if (File.Exists("store.csv"))
            {
                File.Delete("store.csv");
            }
            try
            {
                using (var client = new WebClient())
                {
                    // See https://stackoverflow.com/questions/39307684/webclient-error-when-downloading-file-from-https-url
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    client.DownloadFile(new Uri(CurrentStoreUrl + "store.csv"), "store.csv");
                }
            }
            catch
            {
                MessageBox.Show(CurrentStoreUrl + " is not a valid store.");
                return;
            }
            //read store.csv into StoreVstList
            ReadCsvToNestedList("store.csv", StoreVstList);
            //populate storeVstBox based on StoreVstList
            PopulateCheckedBox(StoreVstList, storeVstBox);
            storesBox.Items.Add(CurrentStoreUrl);
            //add the store to past_stores.csv
            AddLineToCsvFile("past_stores.csv", CurrentStoreUrl);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            //remove the store from the list then delete its entry from past_stores.csv
            string urlToDelete = storesBox.Text;
            if (urlToDelete.Length < 7 || urlToDelete.Substring(0, 7) != "http://")
            {
                urlToDelete = "http://" + urlToDelete;
            }
            if (urlToDelete[urlToDelete.Length - 1] != '/')
            {
                urlToDelete = urlToDelete + "/";
            }
            DeleteLineFromCsvFile("past_stores.csv", urlToDelete);
            List<string> storesList = new List<string>();
            ReadCsvToSimpleList("past_stores.csv", storesList);
            PopulateComboBox(storesList, storesBox);
            storesBox.Text = "";
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            if (LocalPath == null)
            {
                MessageBox.Show("Please select a destination folder first in the Local tab.");
                return;
            }

            //using StoreVstList, download and extract all of the VSTs
            //for each new vst, add to LocalVstList
            bool someFailed = false;
            foreach (int i in storeVstBox.CheckedIndices)
            {
                if (new FileInfo("locations.csv").Length == 0)
                {
                    LocalPath = @"C:\VST\";
                }
                installLabel.Text = "Installing " + StoreVstList[i][0];
                installLabel.Refresh();
                int result = DownloadAndExtractZipFile(StoreVstList[i][2], StoreVstList[i][3]);
                if (result == 1)
                {
                    MessageBox.Show("An error occurred while installing " + StoreVstList[i][0] + ".");
                    someFailed = true;
                    continue;
                }
                string csvLine = StoreVstList[i].Aggregate((x, y) => x + "," + y);
                AddLineToCsvFile(Path.Combine(LocalPath, "local.csv"), csvLine);
                storeVstBox.SetItemChecked(i, false);
            }
            ReadCsvToNestedList(Path.Combine(LocalPath, "local.csv"), LocalVstList);
            //populate localVstBox based on LocalVstList
            PopulateCheckedBox(LocalVstList, localVstBox);
            if (!someFailed)
            {
                installLabel.Text = "All installations complete.";
            } else
            {
                installLabel.Text = "One or more installations could not be completed.";
            }
            
        }

        private void reinstallButton_Click(object sender, EventArgs e)
        {
            if (LocalPath == null)
            {
                return;
            }

            //using localVstBox.CheckedIndices and LocalVstList, populate a list of the selected VSTs
            List<List<string>> vstList = new List<List<string>>();
            foreach (int i in localVstBox.CheckedIndices)
            {
                vstList.Add(LocalVstList[i]);
            }
            //for each VST in the list, delete the file tree then download and extract based on the URL in the list
            bool someFailed = false;
            foreach (List<string> l in vstList)
            {
                installLabel.Text = "Installing " + l[0];
                installLabel.Refresh();
                int result = DownloadAndExtractZipFile(l[2], l[3]);
                if (result == 1)
                {
                    MessageBox.Show("An error occurred while reinstalling " + l[0] + ".");
                    someFailed = true;
                    continue;
                }
            }
            if (!someFailed)
            {
                installLabel.Text = "All installations complete.";
            } else
            {
                installLabel.Text = "One or more installations could not be completed.";
            }
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            if (LocalPath == null)
            {
                return;
            }

            //using localVstBox.CheckedIndices and LocalVstList, populate a list of the selected VSTs
            List<List<string>> vstList = new List<List<string>>();
            foreach (int i in localVstBox.CheckedIndices)
            {
                vstList.Add(LocalVstList[i]);
            }
            //for each VST in the list, delete the file tree then delete its entry from local.csv
            foreach (List<string> l in vstList)
            {
                try
                {
                    Directory.Delete(Path.Combine(LocalPath, l[2]), true);
                }
                catch
                {
                    MessageBox.Show("The folder for " + l[0] + " could not be found. Please delete it manually.");
                }
                string csvLine = l.Aggregate((x, y) => x + "," + y);
                DeleteLineFromCsvFile(Path.Combine(LocalPath, "local.csv"), csvLine);
                ReadCsvToNestedList(Path.Combine(LocalPath, "local.csv"), LocalVstList);
                PopulateCheckedBox(LocalVstList, localVstBox);
            }
        }

        private void storeSelectAllButton_Click(object sender, EventArgs e)
        {
            // select all items in the store checked box
            for (int i = 0; i < storeVstBox.Items.Count; i++)
            {
                storeVstBox.SetItemChecked(i, true);
            }
        }

        private void storeDeselectAllButton_Click(object sender, EventArgs e)
        {
            // deselect all items in the store checked box
            for (int i = 0; i < storeVstBox.Items.Count; i++)
            {
                storeVstBox.SetItemChecked(i, false);
            }
        }

        private void localSelectAllButton_Click(object sender, EventArgs e)
        {
            // select all items in the local checked box
            for (int i = 0; i < localVstBox.Items.Count; i++)
            {
                localVstBox.SetItemChecked(i, true);
            }
        }

        private void localDeselectAllButton_Click(object sender, EventArgs e)
        {
            // deselect all items in the local checked box
            for (int i = 0; i < localVstBox.Items.Count; i++)
            {
                localVstBox.SetItemChecked(i, false);
            }
        }

        private void locationSelectButton_Click(object sender, EventArgs e)
        {
            if (locationBox.Text == "")
            {
                MessageBox.Show("Please enter or browse for the VST folder.");
                return;
            }

            // set the new install path
            string oldPath = LocalPath;
            LocalPath = locationBox.Text;

            // remove the backlash at the end if there is one
            if (LocalPath[LocalPath.Length - 1] == '\\')
            {
                LocalPath = LocalPath.Substring(0, LocalPath.Length - 1);
            }

            // ask the user to create the directory (also creates the local CSV file) if it doesn't exist
            if (!Directory.Exists(LocalPath))
            {
                DialogResult dr = MessageBox.Show(LocalPath + " does not exist. Create it?",
                      "Create new directory?", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(LocalPath);
                    }
                    catch
                    {
                        MessageBox.Show("The directory could not be created.");
                        LocalPath = oldPath;
                        return;
                    }
                    File.Create(Path.Combine(LocalPath, "local.csv")).Close();
                }
                else if (dr == DialogResult.No)
                {
                    LocalPath = oldPath;
                    return;
                }
            }

            if (!File.Exists(Path.Combine(LocalPath, "local.csv")))
            {
                File.Create(Path.Combine(LocalPath, "local.csv")).Close();
            }

            // add the new install path to locations.csv
            List<string> locationList = new List<string>();
            AddLineToCsvFile("locations.csv", LocalPath);
            ReadCsvToSimpleList("locations.csv", locationList);
            PopulateComboBox(locationList, locationBox);

            // read the local CSV file to get the VSTs, and list them in the local checked box
            ReadCsvToNestedList(Path.Combine(LocalPath, "local.csv"), LocalVstList);
            PopulateCheckedBox(LocalVstList, localVstBox);
        }

        private void locationRemoveButton_Click(object sender, EventArgs e)
        {
            // confirm to the user that the VSTs in the folder will not be deleted
            DialogResult dr = MessageBox.Show("VSTs in " + locationBox.Text + " will no longer appear in VST Manager, but will not be deleted. Continue?",
                     "Remove directory from list?", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return;
            }

            // remove the location from locations.csv
            string pathToDelete = locationBox.Text;
            if (pathToDelete[pathToDelete.Length - 1] != '\\')
            {
                pathToDelete = pathToDelete + @"\";
            }
            DeleteLineFromCsvFile("locations.csv", pathToDelete);
            List<string> locationList = new List<string>();
            ReadCsvToSimpleList("locations.csv", locationList);
            PopulateComboBox(locationList, locationBox);

            // reset the local path and checked box
            locationBox.Text = "";
            localVstBox.Items.Clear();
            LocalPath = null;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            // open a file browser to let the user choose the location, then set the combo box's text
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    locationBox.Text = fbd.SelectedPath;
                }
            }
        }
    }
}

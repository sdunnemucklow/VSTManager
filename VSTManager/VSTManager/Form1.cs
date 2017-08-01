// Original work Copyright (c) 2017 Samuel Dunne-Mucklow

using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.Linq;
using System.Net;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using System.IO.Compression;

namespace VSTManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Display product name and version in title bar
            this.Text = String.Format("VST Manager {0}", Application.ProductVersion);
            
            // create necessary CSV files
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
            ReadCsvToList("past_stores.csv", storesList);
            PopulateComboBox(storesList, storesBox);
            List<string> locationList = new List<string>();
            ReadCsvToList("locations.csv", locationList);
            PopulateComboBox(locationList, locationBox);

            // create local.csv in all user-chosen directories if they don't already exist
            foreach (string s in locationList)
            {
                if (!File.Exists(s + "local.csv"))
                {
                    File.Create(s + "local.csv").Close();
                }
            }
        }

        static class G
        {
            // global variables

            private static string _currentStoreUrl = "http://getdunne.net/Krakli/";
            public static string CurrentStoreUrl
            {
                get { return _currentStoreUrl; }
                set { _currentStoreUrl = value; }
            }

            private static List<List<string>> _storeVstList = new List<List<string>>();
            public static List<List<string>> StoreVstList
            {
                get { return _storeVstList; }
                set { _storeVstList = value; }
            }

            private static List<List<string>> _localVstList = new List<List<string>>();
            public static List<List<string>> LocalVstList
            {
                get { return _localVstList; }
                set { _localVstList = value; }
            }

            private static string _localPath;
            public static string LocalPath
            {
                get { return _localPath; }
                set { _localPath = value; }
            }
        }

        private void ReadCsvToList(string csvPath, List<List<string>> csvList)
        {
            csvList.Clear();

            // read the CSV into a list of lists
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
        private void ReadCsvToList(string csvPath, List<string> csvList)
        {
            csvList.Clear();

            // read the CSV into a list of strings
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

        private void AddToCsv(string csvPath, string lineToAdd)
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

        public void DeleteFromCsv(string csvPath, string lineToDelete)
        {
            // rewrite the CSV without the specific line to delete
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

        private int DownloadExtract(string FileName, string DownloadURL)
        {
            // remove the zip file if it was left there
            if (File.Exists(G.LocalPath + FileName + ".zip"))
            {
                File.Delete(G.LocalPath + FileName + ".zip");
            }

            // download the file
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(new Uri(DownloadURL), G.LocalPath + FileName + ".zip");
                }
                catch
                {
                    return 1;
                }
            }

            // if it's already there, move it somewhere else temporarily, but move it back if the installation fails
            if (Directory.Exists(G.LocalPath + FileName))
            {
                Directory.Move(G.LocalPath + FileName, G.LocalPath + FileName + "_old");
            }
            try
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(G.LocalPath + FileName + ".zip", G.LocalPath);
            }
            catch
            {
                if (File.Exists(G.LocalPath + FileName + ".zip"))
                {
                    File.Delete(G.LocalPath + FileName + ".zip");
                }
                if (Directory.Exists(G.LocalPath + FileName + "_old"))
                {
                    Directory.Move(G.LocalPath + FileName + "_old", G.LocalPath + FileName);
                }
                return 1;
            }
            if (File.Exists(G.LocalPath + FileName + ".zip"))
            {
                File.Delete(G.LocalPath + FileName + ".zip");
            }
            if (Directory.Exists(G.LocalPath + FileName + "_old"))
            {
                Directory.Delete(G.LocalPath + FileName + "_old", true);
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

            //set G.CurrentStoreUrl based on storesBox.Text
            G.CurrentStoreUrl = storesBox.Text;

            if (G.CurrentStoreUrl.Length < 7 || G.CurrentStoreUrl.Substring(0, 4) != "http")
            {
                G.CurrentStoreUrl = "http://" + G.CurrentStoreUrl;
            }
            if (G.CurrentStoreUrl[G.CurrentStoreUrl.Length - 1] != '/')
            {
                G.CurrentStoreUrl = G.CurrentStoreUrl + "/";
            }

            //download the store CSV from G.CurrentStoreUrl to store.csv
            if (File.Exists("store.csv"))
            {
                File.Delete("store.csv");
            }
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(new Uri(G.CurrentStoreUrl + "store.csv"), "store.csv");
                }
            }
            catch
            {
                MessageBox.Show(G.CurrentStoreUrl + " is not a valid store.");
                return;
            }
            //read store.csv into G.StoreVstList
            ReadCsvToList("store.csv", G.StoreVstList);
            //populate storeVstBox based on G.StoreVstList
            PopulateCheckedBox(G.StoreVstList, storeVstBox);
            storesBox.Items.Add(G.CurrentStoreUrl);
            //add the store to past_stores.csv
            AddToCsv("past_stores.csv", G.CurrentStoreUrl);
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
            DeleteFromCsv("past_stores.csv", urlToDelete);
            List<string> storesList = new List<string>();
            ReadCsvToList("past_stores.csv", storesList);
            PopulateComboBox(storesList, storesBox);
            storesBox.Text = "";
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            if (G.LocalPath == null)
            {
                MessageBox.Show("Please select a destination folder first in the Local tab.");
                return;
            }

            //using G.StoreVstList, download and extract all of the VSTs
            //for each new vst, add to G.LocalVstList
            bool someFailed = false;
            foreach (int i in storeVstBox.CheckedIndices)
            {
                if (new FileInfo("locations.csv").Length == 0)
                {
                    G.LocalPath = @"C:\VST\";
                }
                installLabel.Text = "Installing " + G.StoreVstList[i][0];
                installLabel.Refresh();
                int result = DownloadExtract(G.StoreVstList[i][2], G.StoreVstList[i][3]);
                if (result == 1)
                {
                    MessageBox.Show("An error occurred while installing " + G.StoreVstList[i][0] + ".");
                    someFailed = true;
                    continue;
                }
                string csvLine = G.StoreVstList[i].Aggregate((x, y) => x + "," + y);
                AddToCsv(G.LocalPath + "local.csv", csvLine);
                storeVstBox.SetItemChecked(i, false);
            }
            ReadCsvToList(G.LocalPath + "local.csv", G.LocalVstList);
            //populate localVstBox based on G.LocalVstList
            PopulateCheckedBox(G.LocalVstList, localVstBox);
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
            if (G.LocalPath == null)
            {
                return;
            }

            //using localVstBox.CheckedIndices and G.LocalVstList, populate a list of the selected VSTs
            List<List<string>> vstList = new List<List<string>>();
            foreach (int i in localVstBox.CheckedIndices)
            {
                vstList.Add(G.LocalVstList[i]);
            }
            //for each VST in the list, delete the file tree then download and extract based on the URL in the list
            bool someFailed = false;
            foreach (List<string> l in vstList)
            {
                installLabel.Text = "Installing " + l[0];
                installLabel.Refresh();
                int result = DownloadExtract(l[2], l[3]);
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
            if (G.LocalPath == null)
            {
                return;
            }

            //using localVstBox.CheckedIndices and G.LocalVstList, populate a list of the selected VSTs
            List<List<string>> vstList = new List<List<string>>();
            foreach (int i in localVstBox.CheckedIndices)
            {
                vstList.Add(G.LocalVstList[i]);
            }
            //for each VST in the list, delete the file tree then delete its entry from local.csv
            foreach (List<string> l in vstList)
            {
                try
                {
                    Directory.Delete(G.LocalPath + l[2], true);
                }
                catch
                {
                    MessageBox.Show("The folder for " + l[0] + " could not be found. Please delete it manually.");
                }
                string csvLine = l.Aggregate((x, y) => x + "," + y);
                DeleteFromCsv(G.LocalPath + "local.csv", csvLine);
                ReadCsvToList(G.LocalPath + "local.csv", G.LocalVstList);
                PopulateCheckedBox(G.LocalVstList, localVstBox);
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
            string oldPath = G.LocalPath;
            G.LocalPath = locationBox.Text;

            // add a backlash onto the end if there isn't one
            if (G.LocalPath[G.LocalPath.Length - 1] != '\\')
            {
                G.LocalPath = G.LocalPath + @"\";
            }

            // ask the user to create the directory (also creates the local CSV file) if it doesn't exist
            if (!Directory.Exists(G.LocalPath))
            {
                DialogResult dr = MessageBox.Show(G.LocalPath + " does not exist. Create it?",
                      "Create new directory?", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(G.LocalPath);
                    }
                    catch
                    {
                        MessageBox.Show("The directory could not be created.");
                        G.LocalPath = oldPath;
                        return;
                    }
                    File.Create(G.LocalPath + "local.csv").Close();
                }
                else if (dr == DialogResult.No)
                {
                    G.LocalPath = oldPath;
                    return;
                }
            }

            if (!File.Exists(G.LocalPath + "local.csv"))
            {
                File.Create(G.LocalPath + "local.csv").Close();
            }

            // add the new install path to locations.csv
            List<string> locationList = new List<string>();
            AddToCsv("locations.csv", G.LocalPath);
            ReadCsvToList("locations.csv", locationList);
            PopulateComboBox(locationList, locationBox);

            // read the local CSV file to get the VSTs, and list them in the local checked box
            ReadCsvToList(G.LocalPath + "local.csv", G.LocalVstList);
            PopulateCheckedBox(G.LocalVstList, localVstBox);
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
            DeleteFromCsv("locations.csv", pathToDelete);
            List<string> locationList = new List<string>();
            ReadCsvToList("locations.csv", locationList);
            PopulateComboBox(locationList, locationBox);

            // reset the local path and checked box
            locationBox.Text = "";
            localVstBox.Items.Clear();
            G.LocalPath = null;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            // open a file browser to let the user choose the location, then set the combo box's text
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    locationBox.Text = fbd.SelectedPath + @"\";
                }
            }
        }
    }
}

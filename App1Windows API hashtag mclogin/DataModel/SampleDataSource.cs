using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace App1Windows_API_hashtag_mclogin.Data
{
    public class doWhateverConsumerAPI
    {
        private int uniqnNum = 1; //unique id sample data item created
        public async void doAClassThing()
        {
            OnBase sds = new OnBase();
            var gruops = sds.Groups;
            foreach (var item in gruops)
            {
                //item.ImagePath = 
            }

            //string file new stuff
            var filele = await StorageFile.GetFileFromPathAsync(@"C:\Users\bryan\Documents\Visual Studio 14\Projects\App1Windows API hashtag mclogin\App1Windows API hashtag mclogin\new stuff.txt");
            string newFileStuff = await FileIO.ReadTextAsync(filele);

            string[] splitslashN = newFileStuff.Split('\n');

            foreach (var strrrr in splitslashN)
            {
                SampleItemsData.Add(new SampleDataItem(uniqnNum++.ToString(), "title" + uniqnNum, "sub-title" + uniqnNum, strrrr, "descriptonle" + uniqnNum, "thecontent" + uniqnNum));
            }

        }
        public ObservableCollection<SampleDataItem> SampleItemsData = new ObservableCollection<SampleDataItem>();
    }
    /// <summary>
    /// Generic item data model.
    /// </summary>

    public class SampleDataItem : INotifyPropertyChanged
    {
        public SampleDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
            this.ListTags = new List<string>();
        }

        public void AddTag(string tagName)
        {
            if (ListTags == null)
            {
                ListTags = new List<string>();
            }
            ListTags.Add(tagName);
        }

        //organize groups by tag
        private string _uniq;

        public string UniqueId
        {
            get { return _uniq; }
            set { _uniq = value; OnPropertyChanged(); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }


        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }
        public List<string> ListTags { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string callerNamed = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(callerNamed));

            }
        }
        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup
    {
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; } //group title set by tag
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<SampleDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class OnBase
    {
        /*

            base
            unsorted
            1
            2
            3
            123%
            [folder 1]
            [folder 2]
            [folder 3]
            123%11
            123%12
            123$%%1234$321

        */
        private static OnBase _sampleDataSource = new OnBase();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<SampleDataItem> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            //todo binar search? iiiidddkkkkk
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private Uri _uri;

        public Uri Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        //public void OrganizeGroups by tag? 

        private async Task GetSampleDataAsync() //imgur api
        {
            if (this._groups.Count != 0)
                return;

            //FileOpenPicker openPicker = new FileOpenPicker();
            //openPicker.ViewMode = PickerViewMode.List;
            //openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            //openPicker.FileTypeFilter.Add("*");
            //IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
            //SampleDataGroup ggg = new SampleDataGroup("1", "uncharter group", "subtitle unchartered group. unsortrte", "ms-appx:///Assets/9.png", "a discription");
            //if (files.Count > 0)
            //{
            //    StringBuilder output = new StringBuilder("Picked files:\n");
            //    // Application now has read/write access to the picked file(s) 
            //    foreach (StorageFile file1 in files)
            //    {
            //        var dddd = await file1.CopyAsync(ApplicationData.Current.LocalFolder);
            //        var ee = await file1.GetBasicPropertiesAsync();
            //        var ee1 = file1.Attributes;
            //        var ee2 = await file1.Properties.GetDocumentPropertiesAsync();
            //        var ee4 = await file1.Properties.GetImagePropertiesAsync();
            //        //var ee3 = await file1.Properties.RetrievePropertiesAsync();
            //        SampleDataItem it = new SampleDataItem(file1.FolderRelativeId, file1.Name, file1.FileType, dddd.Path, file1.Properties.ToString(), file1.Attributes.ToString());
            //        ggg.Items.Add(it);
            //        output.Append(file1.Name + "\n");
            //    }
            //    //OutputTextBlock.Text = output.ToString();
            //}
            //else
            //{
            //    //OutputTextBlock.Text = "Operation cancelled.";
            //}

            //this.Groups.Add(ggg);

            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                SampleDataGroup group = new SampleDataGroup(groupObject["UniqueId"].GetString(),
                                                            groupObject["Title"].GetString(),
                                                            groupObject["Subtitle"].GetString(),
                                                            groupObject["ImagePath"].GetString(),
                                                            groupObject["Description"].GetString());

                foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                {
                    JsonObject itemObject = itemValue.GetObject();
                    group.Items.Add(new SampleDataItem(itemObject["UniqueId"].GetString(),
                                                       itemObject["Title"].GetString(),
                                                       itemObject["Subtitle"].GetString(),
                                                       itemObject["ImagePath"].GetString(),
                                                       itemObject["Description"].GetString(),
                                                       itemObject["Content"].GetString()));
                }
                this.Groups.Add(group);
            }
        }

        public async void ReStructureTagGroups()
        {
            //get items
            //for each item tag start group. collect group items together and create gruops based on these tags
            //await GetSampleDataAsync();
            ObservableCollection<SampleDataGroup> newGroupsCopy = new ObservableCollection<SampleDataGroup>();
            foreach (var group in this.Groups)
            {
                foreach (SampleDataItem item in group.Items)
                {
                    foreach (var tagNm in item.ListTags)
                    {
                        if (newGroupsCopy.Any((x) => x.Title.Contains(tagNm))) //if grup exists with title = list tag name then
                        {
                            (newGroupsCopy.First(x => x.Title.Contains(tagNm))).Items.Add(item); //add the item to the group
                        }
                        else //create a new group for the item with a unique id for whatever
                        {
                            SampleDataGroup newGroup = new SampleDataGroup(Guid.NewGuid().ToString(), tagNm, "subtitle 1", "ms-appx:///Assets/9.png", "sampel description");
                        }
                    }
                }
            }
        }

    }
}
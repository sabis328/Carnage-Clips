using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carnage_Clips
{
    public partial class SearchForm : Form
    {
        Bnet_Client SearchClient;
        MainForm _Parent;

        public bool LoadingAccount { get; set; }
        public bool LoadingDetails { get; set; }
        public SearchForm(MainForm _parentForm)
        {
            _Parent = _parentForm;
            LoadingAccount = false;
            LoadingDetails = false;
            SearchClient = new Bnet_Client();
            SearchClient.API_key = "9efe9b8eba3042afb081121d447fd981";
            SearchClient.Client_Event += SearchClient_Client_Event;
            InitializeComponent();
        }

        private void SearchClient_Client_Event(object sender, Bnet_Client.BNet_Client_Event_Type e)
        {
            switch(e)
            {
                case Bnet_Client.BNet_Client_Event_Type.Search_Complete:
                    LoadingAccount = false;
                    LoadingDetails = false;
                    DisplayResultsList((List<BNet_Profile>)sender);
                    break;
                case Bnet_Client.BNet_Client_Event_Type.Search_No_Results:
                    LoadingAccount = false;
                    LoadingDetails = false;
                    DisplayError();
                    break;
                case Bnet_Client.BNet_Client_Event_Type.Search_Fail:
                    LoadingAccount = false;
                    LoadingDetails = false;
                    DisplayError();
                    break;
                case Bnet_Client.BNet_Client_Event_Type.Details_Loaded:
                    BNet_Profile loadedAccount = (BNet_Profile)sender;
                    Task.Run(() => SearchClient.Load_Character_Entries(loadedAccount));
                    break;
                case Bnet_Client.BNet_Client_Event_Type.Details_Failed:
                    LoadingAccount = false;
                    LoadingDetails = false;
                    DisplayError();
                    break;
                case Bnet_Client.BNet_Client_Event_Type.Characters_Loaded:
                    LoadingAccount = false;
                    LoadingDetails = false;
                    DisplayDetailed((BNet_Profile)sender);
                    break;
            }
        }
        
        /// <summary>
        /// Displays detailed account information for the selected user and updates the selected user of the main form 
        /// for carnage report loading purposes
        /// </summary>
        /// <param name="DisplayAccount"></param>
        private void DisplayDetailed(BNet_Profile DisplayAccount)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    DisplayDetailed(DisplayAccount);
                });
                return;
            }

            treeDetailView.Nodes.Clear();

            TreeNode ProfileNode = new TreeNode(DisplayAccount.GlobalName());
            ProfileNode.Nodes.Add("BNet ID : "  + DisplayAccount.BungieNetId);
            ProfileNode.Nodes.Add("Crosssave code : " + DisplayAccount.CrossSaveCode);
            if (DisplayAccount.CrossSaveCode == "0")
            {
                foreach (Destiny_Membership PlatformAccount in DisplayAccount.DestinyMemberships)
                {
                    if (PlatformAccount.PlatformCharacters != null)
                    {
                        TreeNode PlatformNode = new TreeNode(PlatformAccount.PlatformType());
                        PlatformNode.Nodes.Add("Account id : " + PlatformAccount.MembershipId);
                        PlatformNode.Nodes.Add("Platform Name : " + PlatformAccount.platformDisplayName);

                        foreach (Destiny_Character platformChar in PlatformAccount.PlatformCharacters)
                        {
                            TreeNode CharNode = new TreeNode(platformChar.characterId);
                            CharNode.Nodes.Add(platformChar.light);
                            CharNode.Nodes.Add(platformChar.classHash);

                            PlatformNode.Nodes.Add(CharNode);
                        }

                        ProfileNode.Nodes.Add(PlatformNode);
                    }
                }
            }
            else
            {
                TreeNode PlatformNode = new TreeNode("Cross Save Override : " + DisplayAccount.SelectedMemebership.PlatformType());

                PlatformNode.Nodes.Add("Account id : " + DisplayAccount.SelectedMemebership.MembershipId);
                PlatformNode.Nodes.Add("Platform Name : " + DisplayAccount.SelectedMemebership.platformDisplayName);

                foreach (Destiny_Character platformChar in DisplayAccount.SelectedMemebership.PlatformCharacters)
                {
                    TreeNode CharNode = new TreeNode(platformChar.characterId);
                    CharNode.Nodes.Add(platformChar.light);
                    CharNode.Nodes.Add(platformChar.classHash);

                    PlatformNode.Nodes.Add(CharNode);
                }

                ProfileNode.Nodes.Add(PlatformNode);
            }
            treeDetailView.Nodes.Add(ProfileNode);
            ProfileNode.Expand();

            _Parent.SetSelectedUser(DisplayAccount);

            lblDetailedUser.Text = DisplayAccount.GlobalName() + " bnet information";
            lblStatus.Text = "Idle";
        }
        /// <summary>
        /// Loads all users found matching that name into the listview so that a user can double click to see
        /// detailed information for the selected user
        /// </summary>
        /// <param name="SearchResults"></param>
        private void DisplayResultsList(List<BNet_Profile> SearchResults)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    DisplayResultsList(SearchResults);
                });
                return;
            }
            listResutlsView.Items.Clear();

            foreach(BNet_Profile BNet in SearchResults)
            {
                ListViewItem ProfileItem = new ListViewItem(BNet.GlobalName());
                ProfileItem.Tag = BNet;

                listResutlsView.Items.Add(ProfileItem);
            }
            lblStatus.Text = listResutlsView.Items.Count.ToString() + " result(s) for " + txtUserSearch.Text;
        }

        private void DisplayError()
        {
            if (InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    DisplayError();
                });
                return;
            }
            listResutlsView.Items.Clear();
            lblStatus.Text = "No results found";
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!LoadingAccount && !LoadingDetails)
            {
                LoadingAccount = true;
                LoadingDetails = true;
                lblDetailedUser.Text = "No user selected";
                lblStatus.Text = "Searching for user " + txtUserSearch.Text;
                SearchClient.CancelAll = false;
                Task.Run(() => SearchClient.Search_User_Accounts(txtUserSearch.Text));
            }
        }
        
        
        private void listResutlsView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listResutlsView.SelectedItems != null)
            {
                if (!LoadingAccount && !LoadingDetails)
                {
                    LoadingAccount = true;
                    LoadingDetails = true;
                    lblStatus.Text = "Loading detailed information for " + listResutlsView.SelectedItems[0].Text;
                    BNet_Profile SelectedAccount = (BNet_Profile)listResutlsView.SelectedItems[0].Tag;
                    Task.Run(() => SearchClient.Load_Detailed_Bnet(SelectedAccount));
                }
            }
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {

        }
    }
}

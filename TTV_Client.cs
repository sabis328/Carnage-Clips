using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Globalization;


namespace Carnage_Clips
{
    public class TTV_Client
    {

        public string Twitch_API_Key { get; set; }
        public string Twitch_API_Secret { get; set; }
        public string Twitch_API_Token { get; set; }

        public enum Twitch_Client_Event
        {
            Validate_Suceess,
            Validate_Failed,
            Search_Success,
            Search_None_Found,
            Search_Fail,
            Video_Load_Success,
            Video_None_Found,
            Video_Load_Fail,
            Vod_Check_Success,
            Vod_Check_Fail,
            Vod_Check_None_Found,
            Vod_Check_No_videos
        }

        public bool isValidated { get; set; }
        private string twitch_validation_base = "https://id.twitch.tv/oauth2/token?client_id=";
        private string twitch_validation_mid = "&client_secret=";
        private string twitch_validation_end = "&grant_type=client_credentials";

        public event EventHandler<Twitch_Client_Event> Client_Event;
        public void Validate_Client()
        {
            isValidated = false;
            string twitch_val_link = twitch_validation_base + Twitch_API_Key + twitch_validation_mid + Twitch_API_Secret + twitch_validation_end;

            string ValidationBody = "";
            try
            {
                HttpWebRequest query = (HttpWebRequest)WebRequest.Create(twitch_val_link);
                query.Method = "POST";
                query.KeepAlive = true;

                HttpWebResponse res = (HttpWebResponse)query.GetResponse();

                ValidationBody = new StreamReader(res.GetResponseStream()).ReadToEnd();
                System.Diagnostics.Debug.WriteLine(ValidationBody);

                if (ValidationBody.Contains("access_token"))
                {
                    dynamic validationJson = JsonConvert.DeserializeObject<dynamic>(ValidationBody);
                    Twitch_API_Token = validationJson.access_token.ToString();

                    isValidated = true;

                    Client_Event?.Invoke(this, Twitch_Client_Event.Validate_Suceess);
                }
                else
                {
                    isValidated = false;
                    Client_Event?.Invoke(this, Twitch_Client_Event.Validate_Failed);
                }
            }
            catch
            {
                isValidated = false;
                Client_Event?.Invoke(this, Twitch_Client_Event.Validate_Failed);
            }
        }

        public void Search_Channels(string inputName)
        {
            ServicePointManager.DefaultConnectionLimit = 15;

            List<TwitchBroadcaster> FoundChannels = new List<TwitchBroadcaster>();

            string queryBase = "https://api.twitch.tv/helix/search/channels?query=";
            string responseBody;

            try
            {
                HttpWebRequest query = (HttpWebRequest)WebRequest.Create(queryBase + inputName);
                query.Method = "GET";
                System.Diagnostics.Debug.Print(query.RequestUri.ToString());
                query.Headers.Add("Authorization", string.Format("Bearer {0}", Twitch_API_Token));
                query.Headers.Add("Client-Id", string.Format("{0}", Twitch_API_Key));

                HttpWebResponse res = (HttpWebResponse)query.GetResponse();
                responseBody = new StreamReader(res.GetResponseStream()).ReadToEnd();


                if (responseBody.Contains("broadcaster_language"))
                {
                    dynamic channelsFound = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    string channelsJson = channelsFound["data"].ToString();
                    dynamic channelsSerialized = JsonConvert.DeserializeObject<dynamic>(channelsJson);

                    foreach (var channelData in channelsSerialized)
                    {
                        TwitchBroadcaster Broadcaster = new TwitchBroadcaster();
                        Broadcaster.Broadcaster_DisplayName = channelData.display_name;
                        Broadcaster.Broadcaster_ID = channelData.id;
                        Broadcaster.Broadcaster_Lang = channelData.broadcaster_language;
                        Broadcaster.Broadcaster_Thumbnail_URL = channelData.thumbnail_url;

                        Broadcaster.SetChannelIcon();
                        FoundChannels.Add(Broadcaster);
                    }
                    if (FoundChannels.Count > 0)
                    {
                        Client_Event?.Invoke(FoundChannels, Twitch_Client_Event.Search_Success);
                    }
                    else
                    {
                        Client_Event?.Invoke(FoundChannels, Twitch_Client_Event.Search_None_Found);
                    }
                }
                else
                {
                    Client_Event(FoundChannels, Twitch_Client_Event.Search_Fail);
                }
            }
            catch
            {
                Client_Event(FoundChannels, Twitch_Client_Event.Search_Fail);
            }
        }
        public void Load_Channel_Videos(TwitchBroadcaster inputChannel)
        {
            ServicePointManager.DefaultConnectionLimit = 15;
            string requestBase = "https://api.twitch.tv/helix/videos?user_id=";
            string requestBody;
            inputChannel.ChannelVideos = new List<Twitch_Vod>();
            try
            {
                HttpWebRequest query = (HttpWebRequest)WebRequest.Create(requestBase + inputChannel.Broadcaster_ID);
                query.Method = "GET";

                query.Headers.Add("Authorization", string.Format("Bearer {0}", Twitch_API_Token));
                query.Headers.Add("Client-Id", string.Format("{0}", Twitch_API_Key));


                HttpWebResponse res = (HttpWebResponse)query.GetResponse();
                requestBody = new StreamReader(res.GetResponseStream()).ReadToEnd();

                if (requestBody.Contains("data"))
                {
                    dynamic videosFound = JsonConvert.DeserializeObject<dynamic>(requestBody);
                    string videosJson = videosFound["data"].ToString();
                    dynamic videosSerialized = JsonConvert.DeserializeObject<dynamic>(videosJson);

                    foreach (var vodData in videosSerialized)
                    {
                        Twitch_Vod vod = new Twitch_Vod();
                        vod.Video_ID = vodData.id;
                        vod.Video_Title = vodData.title;
                        vod.Video_Url = vodData.url;
                        vod.Video_View_Count = vodData.view_count;
                        vod.Video_Created_String = vodData.created_at;
                        vod.Video_Published_String = vodData.published_at;
                        vod.Video_Duration = vodData.duration;
                        vod.Muted_segments = vodData.muted_segments;
                        vod.Viewable = vodData.viewable;

                        inputChannel.ChannelVideos.Add(vod);
                    }
                }
                else
                {
                    inputChannel.ChannelVideos = null;

                }
            }
            catch
            {
                inputChannel.ChannelVideos = null;
            }
        }

        public TwitchBroadcaster Current_Channel { get; set; }
        public void Check_and_match_Vods(string inputName)
        {
            ServicePointManager.DefaultConnectionLimit = 15;
            Current_Channel = null;
            List<TwitchBroadcaster> FoundChannels = new List<TwitchBroadcaster>();

            string queryBase = "https://api.twitch.tv/helix/search/channels?query=";
            string responseBody;

            try
            {
                HttpWebRequest query = (HttpWebRequest)WebRequest.Create(queryBase + inputName);
                query.Method = "GET";
                System.Diagnostics.Debug.Print(query.RequestUri.ToString());
                query.Headers.Add("Authorization", string.Format("Bearer {0}", Twitch_API_Token));
                query.Headers.Add("Client-Id", string.Format("{0}", Twitch_API_Key));

                HttpWebResponse res = (HttpWebResponse)query.GetResponse();
                responseBody = new StreamReader(res.GetResponseStream()).ReadToEnd();


                if (responseBody.Contains("broadcaster_language"))
                {
                    dynamic channelsFound = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    string channelsJson = channelsFound["data"].ToString();
                    dynamic channelsSerialized = JsonConvert.DeserializeObject<dynamic>(channelsJson);

                    foreach (var channelData in channelsSerialized)
                    {
                        TwitchBroadcaster Broadcaster = new TwitchBroadcaster();
                        Broadcaster.Broadcaster_DisplayName = channelData.display_name;
                        Broadcaster.Broadcaster_ID = channelData.id;
                        Broadcaster.Broadcaster_Lang = channelData.broadcaster_language;
                        Broadcaster.Broadcaster_Thumbnail_URL = channelData.thumbnail_url;
                        FoundChannels.Add(Broadcaster);
                    }
                    if (FoundChannels.Count > 0)
                    {
                        foreach (TwitchBroadcaster broadcaster in FoundChannels)
                        {
                            if (broadcaster.Broadcaster_DisplayName.ToLower().Trim() == inputName.ToLower().Trim())
                            {
                                Load_Channel_Videos(broadcaster);
                                Current_Channel = broadcaster;
                                Client_Event?.Invoke(broadcaster, Twitch_Client_Event.Vod_Check_Success);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Client_Event?.Invoke(FoundChannels, Twitch_Client_Event.Vod_Check_None_Found);
                    }
                }
                else
                {
                    Client_Event(FoundChannels, Twitch_Client_Event.Vod_Check_Fail);
                }
            }
            catch
            {
                Client_Event(FoundChannels, Twitch_Client_Event.Vod_Check_Fail);
            }
        }
    }

    public class TwitchBroadcaster
    {

        public string Broadcaster_Lang { get; set; }
        public string Broadcaster_DisplayName { get; set; }
        public string Broadcaster_Login { get; set; }
        public bool Is_Live { get; set; }

        public string Broadcaster_ID { get; set; }

        public string Broadcaster_Thumbnail_URL { get; set; }

        public Image Channel_Icon { get; set; }

        public List<Twitch_Vod> ChannelVideos { get; set; }

        public void SetChannelIcon()
        {
            try
            {
                HttpWebRequest iconReq = (HttpWebRequest)WebRequest.Create(Broadcaster_Thumbnail_URL);
                iconReq.AllowWriteStreamBuffering = true;
                iconReq.Timeout = 5000;

                using (HttpWebResponse iconRes = (HttpWebResponse)iconReq.GetResponse())
                {
                    Stream iconStream = iconRes.GetResponseStream();
                    Channel_Icon = Image.FromStream(iconStream);
                    iconRes.Close();
                }
            }
            catch
            {
                Channel_Icon = null;
            }
        }
    }

    public class Twitch_Vod
    {
        public string Video_ID { get; set; }
        public string Video_Title { get; set; }
        public string Video_Created_String { get; set; }
        public string Video_Published_String { get; set; }
        public string Video_Url { get; set; }
        public string Video_Thumbnail_URL { get; set; }

        public string Viewable { get; set; }
        public string Video_View_Count { get; set; }

        public string Video_Duration { get; set; }

        public string Muted_segments { get; set; }

        public Image Video_Thumb { get; set; }

        public DateTime returnVodTime()
        {
            return DateTime.Parse(Video_Created_String);
        }

        public TimeSpan returnVodDuration()
        {
            var m = Regex.Match(Video_Duration, @"^((?<hours>\d+)h)?((?<minutes>\d+)m)?((?<seconds>\d+)s)?$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.RightToLeft);

            int hs = m.Groups["hours"].Success ? int.Parse(m.Groups["hours"].Value) : 0;
            int ms = m.Groups["minutes"].Success ? int.Parse(m.Groups["minutes"].Value) : 0;
            int ss = m.Groups["seconds"].Success ? int.Parse(m.Groups["seconds"].Value) : 0;

            return  TimeSpan.FromSeconds(hs * 60 * 60 + ms * 60 + ss);
        }
        public void SetVODThumb()
        {
            try
            {
                if (Video_Thumbnail_URL.Length > 0)
                {
                    HttpWebRequest iconReq = (HttpWebRequest)WebRequest.Create(Video_Thumbnail_URL);
                    iconReq.AllowWriteStreamBuffering = true;
                    iconReq.Timeout = 5000;

                    using (HttpWebResponse iconRes = (HttpWebResponse)iconReq.GetResponse())
                    {
                        Stream iconStream = iconRes.GetResponseStream();
                        Video_Thumb = Image.FromStream(iconStream);
                        iconRes.Close();
                    }
                }
            }
            catch
            {
                Video_Thumb = null;
            }
        }
    }
}

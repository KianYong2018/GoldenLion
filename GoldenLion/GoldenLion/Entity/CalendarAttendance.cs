using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GoldenLion.Entity
{
    public class CalendarAttendance
    {
        string id;
        String dateTime;
        string userAccountID;
        bool deleted;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "DateTime")]
        public String DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        [JsonProperty(PropertyName = "UserAccount_id")]
        public string UserAccountID
        {
            get { return userAccountID; }
            set { userAccountID = value; }
        }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
    }
}

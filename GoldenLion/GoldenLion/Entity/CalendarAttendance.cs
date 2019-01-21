using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GoldenLion.Entity
{
    public class CalendarAttendance
    {
        int id;
        DateTime dateTime;
        string userAccountID; 

        [JsonProperty(PropertyName = "id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "DateTime")]
        public DateTime DateTime
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
    }
}

using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace GoldenLion.Entity
{
    public class UserAccount
    {
        //Need more in-depth understanding
        string id;
        string name;
        string username;
        string password;
        string role;
        bool deleted;

        [JsonProperty(PropertyName = "id")] //This represent the columns in table 'UserAccount'
        public string IdUserAccount
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty(PropertyName = "Username")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [JsonProperty(PropertyName = "Password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [JsonProperty(PropertyName = "Role")]
        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
    }
}

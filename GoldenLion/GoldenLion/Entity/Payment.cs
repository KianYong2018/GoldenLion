﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLion.Entity
{
    public class Payment
    {
        string id;
        DateTime paymentDate;
        string amount;
        string userAccountID;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "PaymentDate")]
        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }

        [JsonProperty(PropertyName = "Amount")]
        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [JsonProperty(PropertyName = "UserAccount_id")]
        public string UserAccountID
        {
            get { return userAccountID; }
            set { userAccountID = value; }
        }
    }
}

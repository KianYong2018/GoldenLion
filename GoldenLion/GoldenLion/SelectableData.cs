using GoldenLion.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLion
{
    public class SelectableData<UserAccount>
    {
        public UserAccount Data { get; set; }
        public bool Selected { get; set; }

    }
}

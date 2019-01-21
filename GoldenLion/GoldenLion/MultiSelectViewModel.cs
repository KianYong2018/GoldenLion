using GoldenLion.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLion
{
    public class MultiSelectViewModel
    {
        public MultiSelectViewModel(List<SelectableData<UserAccount>> data)
        {
            DataList = data;
        }

        public List<SelectableData<UserAccount>> DataList { get; set; }

        public List<SelectableData<UserAccount>> GetNewData()
        {
            var list = new List<SelectableData<UserAccount>>();

            foreach(var data in DataList)
            {
                list.Add(new SelectableData<UserAccount>() { Data = data.Data, Selected = data.Selected });
            }
            return list;
        }


    }
}

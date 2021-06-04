using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Core.Domain
{
    public sealed class SortMainCategories
    {
        public bool? DecendingOrder { get; set; }
        private static readonly SortMainCategories instance = new ();
        static SortMainCategories() { }
        public static SortMainCategories Instance
        {
            get { return instance; }
        }

        public void SetOrder(bool? decending)
        {
            DecendingOrder = decending;
        }
    }
}

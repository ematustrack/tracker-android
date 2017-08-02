using System;
using System.Collections.Generic;

namespace test2
{
	public class ST
	{
        private string v;

        public ST(string v)
        {
            this.v = v;
        }

        public string st { get; set; }
		public string location { get; set; }
		public string note { get; set; }
		public int pk { get; set; }
        public string folio { get; set; }
		public List<Folio> folios { get; set; }
		public override string ToString()
		{
            return "ST: " + st;
		}
	}
}

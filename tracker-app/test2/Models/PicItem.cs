using System;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace test2
{
    public class PicItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CreatedOn { get; set; }
        public string FileName { get; set; }
        public bool Sent { get; set; }
        public string Color { get; set; }
        public DateTimeOffset DateSent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Folio { get; set; }

		public string Folio_string { get; set; }
		public int ST { get; set; }
		
		public string ST_string { get; set; }
		public string Note { get; set; }
        public string Notes { get; set; }


		public override string ToString()
		{
			return string.Format("[PicItem: Id={0}, CreatedOn={1}, FileName={2}]", Id, CreatedOn, FileName);
		}
	}
}

using System;
namespace test2
{
	public class Folio
	{
		public Folio(string number)
		{
			this.number = number;
		}
		public int pk { get; set; }
		public string number { get; set; }
	}
}

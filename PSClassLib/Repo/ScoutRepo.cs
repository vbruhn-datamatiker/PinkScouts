using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
	public class ScoutRepo
	{
		#region instance fields
		private List<Scout> _scouts;
		#endregion

		#region Properties
		public List<Scout> Scouts
		{
			get { return _scouts; }
			set { _scouts = value; }
		}
		#endregion

		#region Constructors
		public ScoutRepo()
		{
			_scouts = new List<Scout>();
		}
		public ScoutRepo(List<Scout> scouts)
		{
			_scouts = scouts;
		}
		#endregion

		#region Methods
		#endregion


	}
}

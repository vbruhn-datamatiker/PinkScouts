using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
	public class SkillRepo
	{
		#region instance fields
		private List<Skill> _skills;
		#endregion

		#region Properties
		public List<Skill> Skills
		{
		get { return _skills; }
		set { _skills = value; }
		}
		#endregion

		#region Constructors
		#endregion
		public SkillRepo()
		{
			_skills = new List<Skill>();
		}
		public SkillRepo(List<Skill> skills)
		{
			_skills = skills;
		}
		#region Methods
		#endregion


	}
}

using PSClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class Skill : IHasId
    {
        #region instance fields
        private int _id;
        private string _skillName;
		private int _skillMastery;
		#endregion

		#region Properties
		#endregion

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public string SkillName
		{
			get { return _skillName; }
			set { _skillName = value; }
		}
		public int SkillMastery
		{
			get { return _skillMastery; }
			set { _skillMastery = value; }
		}
		#region Constructors
		#endregion

		public Skill()
		{
			_id = 0;
			_skillName = "";
			_skillMastery = 0;
		}

		public Skill(int skillId, string skillName, int skillMastery)
		{
			_id = skillId;
			_skillName = skillName;
			_skillMastery = skillMastery;
		}
		#region methods

		public override string ToString()
		{
			return $"Id : {_id}, navn : {_skillName}, Færdighedsniveau : {_skillMastery}";
		}
		#endregion

	}
}

using PSClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class Skill : IHasId
    {
        #region instance fields
        private int _skillId;
        private string _skillName;
		private int _skillMastery;
		#endregion

		#region Properties
		#endregion

		public int SkillId
		{
			get { return _skillId; }
			set { _skillId = value; }
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
			_skillId = 0;
			_skillName = "";
			_skillMastery = 0;
		}

		public Skill(int skillId, string skillName, int skillMastery)
		{
			_skillId = skillId;
			_skillName = skillName;
			_skillMastery = skillMastery;
		}
		#region methods

		public override string ToString()
		{
			return $"Id : {_skillId}, navn : {_skillName}, Færdighedsniveau : {_skillMastery}";
		}
		#endregion

	}
}

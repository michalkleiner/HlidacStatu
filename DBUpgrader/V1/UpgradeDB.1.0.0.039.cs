﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseUpgrader;
using System.Configuration;


namespace HlidacSmluv.DBUpgrades
{
	public static partial class DBUpgrader
	{

		private partial class UpgradeDB
		{

			[DatabaseUpgradeMethod("1.0.0.39")]
			public static void Init_1_0_0_39(IDatabaseUpgrader du)
			{

                string sql = @"";

                du.AddColumnToTable("SpecificContact", "nvarchar(100)", "watchdog", true);

                //add new bank account

                

			}

	


		}

	}
}
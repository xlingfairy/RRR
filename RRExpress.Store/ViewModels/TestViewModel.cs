using System;
using System.Collections.Generic;
using System.Linq;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;

namespace RRExpress.Store
{
	[Regist(InstanceMode.Singleton)]
	public class TestViewModel : StoreBaseVM
	{
		public override char Icon
		{
			get
			{
				return (char)0xf015;
			}
		}

		public override string Title
		{
			get
			{
				return "商城";
			}
		}


		public List<string> Datas { get; } = System.Linq.Enumerable.Range(0, 100).Select(i => i.ToString()).ToList();



	}
}

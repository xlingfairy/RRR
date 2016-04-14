﻿using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class EditMyInfoViewModel : BaseVM {
        public override string Title {
            get {
                return "个人信息修改";
            }
        }


        public List<string> Sexs {
            get;
        }

        public string Sex { get; set; }

        public EditMyInfoViewModel() {
            this.Sexs = new List<string>() {
                "男","女","保密"
            };
        }
    }
}

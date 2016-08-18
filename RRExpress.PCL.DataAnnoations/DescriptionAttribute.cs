using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel {
    public class DescriptionAttribute : Attribute {

        public string Description { get; }

        public DescriptionAttribute(string description) {
            this.Description = description;
        }

    }
}

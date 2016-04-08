using RRExpress.Common.Extends;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Moq.Auth {

    /// <summary>
    /// 
    /// </summary>
    [Obsolete("只用于模拟, 请不要使用")]
    public class UsersConfig : ConfigurationSection {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public Users Users {
            get {
                return (Users)this[""];
            }
        }
    }

    [Obsolete("只用于模拟, 请不要使用")]
    public class Users : ConfigurationElementCollection {

        protected override ConfigurationElement CreateNewElement() {
            return new User();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((User)element).ID;
        }

        public User Get(int id) {
            var item = (User)this.BaseGet(id);
            return item;
        }

        public User Get(string userName) {
            return this.Cast<User>().FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

    }

    [Obsolete("只用于模拟, 请不要使用")]
    public class User : ConfigurationElement {
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value) {
            return true;
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader) {
            return true;
        }


        /// <summary>
        /// Key
        /// </summary>
        [ConfigurationProperty("id", IsRequired = true)]
        public int ID {
            get {
                return this["id"].ToString().ToInt();
            }
            set {
                this["id"] = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName {
            get {
                return this["userName"].ToString();
            }
            set {
                this["userName"] = value;
            }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password {
            get {
                return this["password"].ToString();
            }
            set {
                this["password"] = value;
            }
        }
    }
}

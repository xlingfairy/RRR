using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace RRExpress.Common {

    [DataContract]
    public class Token {

        [JsonProperty("access_token")]
        public string AccessToken {
            get;
            set;
        }

        [JsonProperty("token_type")]
        public string TokenType {
            get;
            set;
        }


        [JsonProperty("expires_in")]
        public int ExpressIn {
            get;
            set;
        }

        [JsonProperty("userId")]
        public int UserID {
            get;
            set;
        }

        [JsonProperty("userName")]
        public string Account {
            get;
            set;
        }


        [JsonProperty("LoginedOn")]
        public DateTime? LoginedOn {
            get;
            set;
        }


        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid {
            get {
                return
                    !string.IsNullOrWhiteSpace(this.AccessToken)
                    && this.UserID >= 0
                    && !string.IsNullOrWhiteSpace(this.Account)
                    && this.LoginedOn.HasValue
                    && this.LoginedOn.Value.AddSeconds(this.ExpressIn) > DateTime.Now;
            }
        }
    }
}

using RRExpress.AppCommon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RRExpress.AppCommon.Services {

    /// <summary>
    /// 通讯录服务
    /// </summary>
    public interface IAddressBook {

        /// <summary>
        /// 获取通讯录中的联系人
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contacter>> GetContactors();

    }
}

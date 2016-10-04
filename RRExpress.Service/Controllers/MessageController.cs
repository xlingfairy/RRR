using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RRExpress.Service.Controllers {
    public class MessageController : ApiController {


        [HttpGet]
        public Message Get(long id) {
            return new Message() {
                MessageID = id,
                Subject = $"消息{id}",
                Ctx = "消息内容消息内容消息内容消息内容消息内容消息内容消息内容消息内容消息内容"
            };
        }


        [HttpGet]
        [Route("api/Message/GetByPage")]
        public IEnumerable<Message> GetByPage(int page = 0, int pageSize = 10) {
            if (page < 3) {
                var rnd = new Random();
                return Enumerable.Range(pageSize * page, pageSize)
                    .Select(i => {
                        return new Message {
                            MessageID = i,
                            CreatedOn = DateTime.Now.AddDays(-i * page),
                            Subject = $"消息{i}",
                            IsReaded = rnd.Next() % 2 == 0
                        };
                    });
            } else
                return Enumerable.Empty<Message>();
        }
    }
}

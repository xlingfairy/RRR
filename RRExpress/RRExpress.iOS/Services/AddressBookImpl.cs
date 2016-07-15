using RRExpress.Services;
using System;
using System.Collections.Generic;
using System.Text;
using RRExpress.Models;
using System.Threading.Tasks;
using System.Linq;
using UIKit;
using System.IO;
using Foundation;
using RRExpress.iOS.Services;
using Xamarin.Forms;
using System.Runtime.InteropServices;

[assembly: Dependency(typeof(AddressBookImpl))]
namespace RRExpress.iOS.Services {

  
    public class AddressBookImpl : IAddressBook {

        public async Task<IEnumerable<Contacter>> GetContactors() {

            var book = new Xamarin.Contacts.AddressBook();

            var datas = new List<Contacter>();

            await book.RequestPermission().ContinueWith(t => {
                if (t.Result) {

                    //foreach (Contact contact in book.OrderBy(c => c.LastName)) {
                    var cs = book.ToList()
                                .OrderBy(c => c.DisplayName);

                    foreach (var c in cs) {

                        foreach (var p in c.Phones) {
                            datas.Add(new Contacter() {
                                Phone = p.Number,
                                PhoneType = p.Label,
                                Name = c.DisplayName,
                                Img = this.GetBytes(c.GetThumbnail())
                            });
                        }
                    }
                }
            });

            return datas;
        }


        /// <summary>
        /// http://stackoverflow.com/questions/17112314/converting-uiimage-to-byte-array
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        private byte[] GetBytes(UIImage bmp) {
            if (bmp == null)
                return null;

            using (NSData imageData = bmp.AsPNG()) {
                var bytes = new Byte[imageData.Length];
                Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
                return bytes;
            }
        }
    }
}

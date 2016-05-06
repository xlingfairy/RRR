using Android.Graphics;
using RRExpress.Droid.Services;
using RRExpress.Models;
using RRExpress.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Contacts;
using Xamarin.Forms;

[assembly: Dependency(typeof(AddressBookImpl))]
namespace RRExpress.Droid.Services {

    //https://components.xamarin.com/gettingstarted/xamarin.mobile
    public class AddressBookImpl : IAddressBook {

        #region
        ////https://developer.xamarin.com/recipes/android/data/contentproviders/read_contacts/
        //public async Task<IEnumerable<Contacter>> GetContactors() {

        //    var datas = new List<Contacter>();

        //    await Task.Run(() => {
        //        var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;

        //        string[] projection = {
        //            ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Id,
        //            ContactsContract.CommonDataKinds.Phone.InterfaceConsts.DisplayName,
        //            ContactsContract.CommonDataKinds.Phone.Number,
        //            ContactsContract.CommonDataKinds.Phone.InterfaceConsts.PhotoId,
        //            ContactsContract.CommonDataKinds.Phone.InterfaceConsts.PhotoThumbnailUri
        //    };


        //        try {
        //            using (var cursor = Forms.Context.ContentResolver.Query(uri, projection, null, null, null)) {

        //                if (cursor.MoveToFirst()) {
        //                    do {
        //                        var name = cursor.GetString(1);
        //                        var phone = cursor.GetString(2);
        //                        var thumb = cursor.GetString(4);

        //                        //Uri uri = ContentUris.withAppendedId(
        //                        //                    ContactsContract.Contacts.CONTENT_URI,
        //                        //                    contact.getContactId());
        //                        //InputStream input = ContactsContract.Contacts
        //                        //        .openContactPhotoInputStream(ctx.getContentResolver(), uri);
        //                        //Bitmap contactPhoto = BitmapFactory.decodeStream(input);
        //                        //holder.quickContactBadge.setImageBitmap(contactPhoto);

        //                        ContentUris.WithAppendedId(ContactsContract.Contacts.ContentUri,)


        //                        datas.Add(new Contacter() {
        //                            Name = name,
        //                            Phone = phone,
        //                            Img = thumb
        //                        });

        //                    } while (cursor.MoveToNext());
        //                }
        //            }
        //        } catch (SecurityException) {

        //        } catch (Java.Lang.Exception) {

        //        }
        //    });

        //    return datas;
        //}
        #endregion

        #region
        public async Task<IEnumerable<Contacter>> GetContactors() {
            var book = new AddressBook(Forms.Context);

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
        #endregion
        private byte[] GetBytes(Bitmap bmp) {
            if (bmp == null)
                return null;

            using (var msm = new MemoryStream()) {
                bmp.Compress(Bitmap.CompressFormat.Png, 100, msm);
                return msm.GetBuffer();
            }
        }
    }
}
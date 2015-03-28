using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

// http://go.microsoft.com/fwlink/?LinkId=290986&clcid=0x409

namespace App1Windows_API_hashtag_mclogin
{
    internal class bryanwesleystumpPush
    {
        public async static void UploadChannel()
        {

            try
            {
            var channel = await Windows.Networking.PushNotifications.PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                await App.bryanwesleystumpClient.GetPush().RegisterNativeAsync(channel.Uri);    
                await App.bryanwesleystumpClient.InvokeApiAsync("notifyAllUsers",
                    new JObject(new JProperty("toast", "Sample Toast")));
            }
            catch (Exception exception)
            {
                HandleRegisterException(exception);
            }
        }

        private static void HandleRegisterException(Exception exception)
        {
//            //cross-refrences references are people. network display. tools. --//--//--//--//--//--//--//--//--//--//--//--//--///---/---/-/--/---/-/-///////-/-//-/-///-/-//-/-/
//            ////miami-caniseeall-//those//signs//in//the//airr//miami//right//now//--//--//--//--//-------///----////-----;;;;;;;;/////////////////;;;;;====-------;llllp;;l;;lkkkl;;;;;;pppppp[
//            //[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[]]]]]]]]]]]]]]][[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[\]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]
//            //l//l//ll/l//l/ll//l/l/l/l/l/l/l/lll//l/l/ll/l//l/l/l/l/l/l/l/l/l/l/ll//ll//l/l/ll//l/l/ll//l/ll//l/ll//l/l/l/l/ll//ll/l//ll//l/l/ll/l/l//l/ll//l/ll//l/l/l
//            ////1/1/1//1/1/11//1/1/1/111/1//1/1/1//1//1//1///1///1//1//1///1//1///1//1//1//1//1/1///1//1//1//1//1//1//1/1/1//1/1//11//1
//            /1/1/1//1/1//1/1//1//1//1/1/1/1/1/1/1/1/1/1/1/1/1/1/1/1/1/1//1//1//1/1/1//1/1//1/1/1/1//1//1/1//1/1//1//1/1//1/1//1/1//1////1//1
//            /1/1/1////1///1///1///1/1//1/1///1//1//1//1//1/1//1/1/1/1//1/1////1///1//1/////1///1/////1////1/////1//////1//////1////
//            //1/1//////1///////1///////1//1/1/1/1/1/1/1/11//////1///1///1///1//1/1/1///1//1//1///1///1///1/1///1//1/
//            1///1//1//1//1//1///1///1///1/1//////////1////1////1///1///1/////1//////1//1////1//1///1///1///1//1/1///1///1//1/1/1//1////1////1/1/1///1/1/1/1/1//1//1/1/1/1/1/1
////1//1//1//1/1//1//11//1//1/1/1/1/
////1//1/1//1/1/1/1/1//1/1/1/1/1/1/1
////1/1/1/1/1/1/1/1//1/1//1/1/1//1/11
////1/1/1/1/1/1/1/1/1//11/1/1/1//1/1/1/1/1/1
////1/1/1/1/1/1//1
////1/1/1/1/1/1/1/
//'/1//1//1//1/1//1
//                //1/1/1/1/1/1/1/1//1/1/1/1//1//1//1/1//1/1//1///1//1//1//1/1//1/1/111//1/1//1//1/1////1/1///1//1/1/1/1/1/1/1/11/1/1/1/1/1/1/1/1/1/1/111/1/11/1111
//                ///1/11/1/11/1/1/11/111/1/111/1/1/1111/1///1/1111/1/1/1/1/11/1/1/1/1/1/1//1/11/1/1/1/1/11/1/1/1/1//1/1//1/1/`/`/`/`//11/lock/lock/lock/lock/l
        }
    }
}

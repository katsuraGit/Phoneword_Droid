using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace Phoneword_Droid
{
    [Activity(Label = "Phoneword_Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button> (Resource.Id.CallButton);

            // "Call" を Disable にします

            callButton.Enabled = false;

            // 番号を変換するコードを追加します。

            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>

            {

                // ユーザーのアルファベットの電話番号を電話番号に変換します。

                translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);

                if (String.IsNullOrWhiteSpace(translatedNumber))

                {

                    callButton.Text = "Call";

                    callButton.Enabled = false;

                }

                else

                {

                    callButton.Text = "Call " + translatedNumber;

                    callButton.Enabled = true;

                }

            };



            callButton.Click += (object sender, EventArgs e) =>

            {

                Bundle args = new Bundle();

                args.PutString("translatedNumber", translatedNumber);

                // ShowDialog(DIALOG_ID_CALL, args);

            };

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
        }
    }
}


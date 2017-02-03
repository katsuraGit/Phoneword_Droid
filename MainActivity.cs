using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using System.Collections.Generic;



namespace Phoneword_Droid
{
    [Activity(Label = "電話アプリ", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly List<string> phoneNumbers = new List<string>(); 
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            Button callHitoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);

            // "Call" を Disable にします
            callButton.Enabled = false;

            // 番号を変換するコードを追加します。
            string translatedNumber = string.Empty;

            // 「translate」ボタンがクリックされた時に発火するイベント
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

            // 「Call」ボタンがクリックされた時に発火するイベント
            callButton.Click += (object sender, EventArgs e) =>
            {
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call" + translatedNumber + "?");
                callDialog.SetNeutralButton("Call", delegate
                {
                    //履歴に番号追加
                    phoneNumbers.Add(translatedNumber);
                    //CallHistory ボタンを有効化
                    callHitoryButton.Enabled = true;
                    // 電話へのintent策債
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                    StartActivity(callIntent);

                });
                callDialog.SetNegativeButton("Cancel", delegate { });

                callDialog.Show();
                  


            };

            // Call History ボタン
            callHitoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CallHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
         
          
            };
        }
    }
}


﻿using Pechkin;  //引用它
using System.Drawing.Printing;  //設定A4那邊要用的

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
	有其他的方法請再查找，以下是找其中一個
	參考網圵：https://blog.darkthread.net/blog/pechkin/

	1. NuGet裝上 Pechkin
	2. 將專案目標平台切成x86 (如此仍說     無法加載 DLL"wkhtmltox0.dll"的話 將packages --> Pechkic 版本號的資料夾 ---> content 中的5 
		個檔案複製貼上到專案的bin資料夾，即可解決)

	3.開始寫
	 
*/

namespace HtmlToPdf
{
	class Program
	{
		static void Main(string[] args)
		{

			//初使設置(照著寫)
			var config = new GlobalConfig();

			//這裡可以自由設定   參考網圵 https://stackoverflow.com/questions/47021112/qt-could-not-initialize-ole-error-80010106-libwkhtmltox-dll-on-c-sharp 的中間處
			config.SetPaperSize(PaperKind.A4);    //這段你直接打會錯，但你就接受他的修改(接受xxx的參考……)的這個

			var pechkin = new SimplePechkin(config);
			ObjectConfig oc = new ObjectConfig();

			//目標網圵
			string goalURL = "https://www.google.com.tw/";

			//進行設定(轉成pdf時)
			oc.SetPrintBackground(true)
			    .SetLoadImages(true)
			    .SetPageUri(goalURL);

			//轉成二進制檔
			byte[] pdf = pechkin.Convert(oc);
			//存檔
			File.WriteAllBytes("google.pdf", pdf);

			//在執行這邊時，如果視窗出現 Qt: Could not initialize OLE (error 80010106) 這錯誤
			//那應該是跟此網站的js有關    他們的src = "/js/xxx.js" 應改成 => http開頭的
			//參考網圵 https://github.com/wkhtmltopdf/wkhtmltopdf/issues/2782
			//暫且視為此問題，以後會再研究


			//------------------------------------------------------------------------------------------------------------------------------------------------------------------

			//自己組的html語法
			string html = @"<html><head><style>
								body { background: #ccc; }
								div { 
										width: 200px; height: 100px; border: 1px solid red; border-radius: 5px;
										margin: 20px; padding: 4px; text-align: center;
									}
								</style></head>
								<body>
										<div>Hong</div>
										<script>document.write('<span>Generated by JavaScript</span>');</script>
								</body></html>";

			//轉換
			byte[] pdf2 = pechkin.Convert(oc, html);
			File.WriteAllBytes("myhtml.pdf", pdf2);

			Console.Read();
		}

	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Blog.App_Start
{
    public class CommanFile
    {
        public Boolean CopyFile(string fileName)
        {
            DirectoryInfo dinfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "articles");  //创建根目录对象
            if (dinfo.Exists)
            {
                FileInfo finfo = new FileInfo(dinfo.FullName + "/HtmlPage1.html"); //创建fileinfo文件对象
                if (finfo.Exists)
                {
                    finfo.CopyTo(dinfo.FullName + "/" + fileName + ".html", true);
                    dinfo.Refresh();
                    return true;
                }
            }

            return false;
        }
    }
}
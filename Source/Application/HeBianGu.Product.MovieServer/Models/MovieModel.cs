using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace HeBianGu.Product.MovieServer.Models
{
    public class MovieModel
    {
        private string _title;
        /// <summary> 说明 </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }


        private string _detial;
        /// <summary> 说明 </summary>
        public string Detial
        {
            get { return _detial; }
            set { _detial = value; }
        }


        private Image _image;
        /// <summary> 说明 </summary>
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private string  _fileFullPath;
        /// <summary> 说明 </summary>
        public string  FileFullPath
        {
            get { return _fileFullPath; }
            set { _fileFullPath = value; }
        }


    }
}
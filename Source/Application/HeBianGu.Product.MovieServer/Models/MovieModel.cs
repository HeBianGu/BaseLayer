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

        private string _imagePath;
        /// <summary> 说明 </summary>
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }


        private string _fileFullPath;
        /// <summary> 说明 </summary>
        public string FileFullPath
        {
            get { return _fileFullPath; }
            set { _fileFullPath = value; }
        }

        private MovieType _movieType;
        /// <summary> 说明 </summary>
        public MovieType MovieType
        {
            get { return _movieType; }
            set { _movieType = value; }
        }

    }


    /// <summary> 说明 </summary>
    public enum MovieType
    {
        DZP = 0, XJP, AQP, KHP, JQP, XYP, ZZP, KBP, ZNP, LXJ, DM
    }
}
using System;

namespace HebianGu.Product.WinHelper.ViewModel
{
    class Note
    {
        public Note()
        {

        }

        public Note(int id, string title, string content, DateTime createTime)
        {
            ID = id;
            Title = title;
            Content = content;
            CreateTime = createTime;
        }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }

    
}

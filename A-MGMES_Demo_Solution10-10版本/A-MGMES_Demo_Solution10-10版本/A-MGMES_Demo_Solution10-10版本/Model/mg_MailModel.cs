using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class mg_MailModel
    {
       public mg_MailModel() { }
       private int? _mail_id;
       private int? _ReceiptType;
       private string _ReceiptTypeName;
       private string _MailRecipient;
       private int? _RecipientType;
       private string _RecipientTypeName;
       private string _MailName;

       public int? mail_id
       {
           get { return _mail_id; }
           set { _mail_id = value; }
       }
       public int? ReceiptType
       {
           get { return _ReceiptType; }
           set { _ReceiptType = value; }
       }
       public string ReceiptTypeName
       {
           get { return _ReceiptTypeName; }
           set { _ReceiptTypeName = value; }
       }
       public string MailRecipient
       {
           get { return _MailRecipient; }
           set { _MailRecipient = value; }
       }

       public int? RecipientType
       {
           get { return _RecipientType; }
           set { _RecipientType = value; }
       }
       public string RecipientTypeName
       {
           get { return _RecipientTypeName; }
           set { _RecipientTypeName = value; }
       }
       public string MailName
       {
           get { return _MailName; }
           set { _MailName = value; }
       }
    }
}

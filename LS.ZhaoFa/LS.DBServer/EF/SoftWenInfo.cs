//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LS.DBServer.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class SoftWenInfo
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> SoftWenAdminId { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string SoftWenContent { get; set; }
        public string ImgRemarks { get; set; }
        public Nullable<int> ShowPositionId { get; set; }
        public Nullable<int> TopIndex { get; set; }
    }
}
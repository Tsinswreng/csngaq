using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngaq.Core.Model;

// public interface I_IdBlCtUt {
// 	// [Key]

// 	// [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
// 	public i64 id { get; set; }

// 	public str bl { get; set; }
// 	//註解不可被子類繼承
// 	//[DefaultValue("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))")]
// 	public i64 ct { get; set; }
// 	//[DefaultValue("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))")]
// 	public i64 ut { get; set; }
// }

//It is just difficult at the beginning

public interface I_RowBaseInfo
	:Shr.Model.IF.I_id<i64>
	,Shr.Model.IF.I_RowBaseInfo
{

}